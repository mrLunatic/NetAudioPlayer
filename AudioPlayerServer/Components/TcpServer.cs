using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.AudioPlayerServer.Components
{
    public abstract class CommunicationServerBase : ICommunicationServer
    {
        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public virtual void Dispose()
        {

        }

        #endregion

        #region Implementation of ICommunicationServer

        public event EventHandler<RequestRecievedEventArgs> RequestRecieved;

        /// <summary>
        /// Запускает сервер с указанными параметрами
        /// </summary>
        /// <param name="host">Адрес хоста, на котором размещается сервер</param>
        /// <param name="serviceName">Имя службы (порт), на котором размещается сервер</param>
        public void Start(IPAddress host, string serviceName)
        {
            int port;
            if (!int.TryParse(serviceName, out port))
            {
                throw new ArgumentException();
            }

            StartInternal(host, port);
        }

        /// <summary>
        /// Останавливает сервер
        /// </summary>
        public void Stop()
        {
            StopInternal();
        }

        /// <summary>
        /// Отправляет сообщение всем подключенным клиентам
        /// </summary>
        /// <param name="message"></param>
        public void SendBroadcast(IMessage message)
        {
            if (message == null)
            {
                return;                
            }
            var str = MessageParser.Serialize(message);
            SendBroadcast(MessageParser.Serialize(message));
        }

        #endregion

        protected abstract void StartInternal(IPAddress host, int port);

        protected abstract void StopInternal();

        protected abstract void SendBroadcast(string data);

        protected string OnMessageRecieved(string message)
        {
            var msg = MessageParser.Parse(message);

            if (msg == null)
            {
                return null;
            }

            var response = OnRequestRecieved(msg);

            if (response == null)
            {
                return null;
            }

            return MessageParser.Serialize(response);
        }

        private IMessage OnRequestRecieved(IMessage request)
        {
            var args = new RequestRecievedEventArgs(request);

            RequestRecieved?.Invoke(this, args);

            return args.Response;
        }
    }

    public sealed class TcpServer : CommunicationServerBase
    {
        private CancellationTokenSource _cts;

        private TcpListener _listener;

        private Task _listenerTask; 

        private readonly IDictionary<TcpClient, Task>  _clients = new ConcurrentDictionary<TcpClient, Task>();


        protected override void StartInternal(IPAddress host, int port)
        {            
            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            _listener = new TcpListener(host, port);

            _listener.Start();

            _listenerTask = Task.Run( async () => { await Listen(_cts.Token); }, _cts.Token);

        }

        protected override void StopInternal()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        protected override async void SendBroadcast(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);

            await Task.Run(
                async () =>
                {
                    foreach (var tcpClient in _clients.Keys)
                    {
                        await tcpClient.GetStream().WriteAsync(data, 0, data.Length);
                    }
                });
        }

        private async Task Listen(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync();

                var task = new Task(() => ProcessClient(client, token));

                _clients.Add(client, task);
                task.Start();
            }
        }

        private async void ProcessClient(TcpClient client, CancellationToken token)
        {
            var inputBuffer = new byte[client.ReceiveBufferSize];

            using (var stream = client.GetStream())
            {
                int recievedCount;

                while ((recievedCount = await stream.ReadAsync(inputBuffer, 0, inputBuffer.Length, token)) > 0)
                {
                    var data = Encoding.UTF8.GetString(inputBuffer, 0, recievedCount);

                    var response = OnMessageRecieved(data);

                    if (response != null)
                    {
                        var output = Encoding.UTF8.GetBytes(response);

                        await stream.WriteAsync(output, 0, output.Length, token);
                    }
                }
            }

            _clients.Remove(client);
            client.Close();
        }


    }
}
