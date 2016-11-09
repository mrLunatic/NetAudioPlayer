using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.AudioPlayerServer.Components;
using NetAudioPlayer.Core.Components;
using NetAudioPlayer.Core.Components.Common;
using NetAudioPlayer.Core.Components.Communication;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Model;
using SimpleTCP;

namespace NetAudioPlayer.AudioPlayerServer.Components
{
    public sealed class SimpleTcpServer : ICommunicationServer
    {
        private ILocalizationService Localization => ServiceLocator.Current.GetInstance<ILocalizationService>();

        private readonly SimpleTCP.SimpleTcpServer _server = new SimpleTCP.SimpleTcpServer()
        {
            AutoTrimStrings = true,
            Delimiter = (byte)'|'
        };

        private bool _disposed;

        public SimpleTcpServer()
        {
            _server.DataReceived += ServerOnDataReceived;
        }

        private void ServerOnDataReceived(object sender, Message message)
        {
            if (string.IsNullOrEmpty(message.MessageString))
            {
                return;
            }

            var msg = MessageParser.Parse(message.MessageString);

            IMessage response;

            if (msg != null)
            {
                var args = new RequestRecievedEventArgs(msg);

                OnRequestRecieved(args);

                response = args.Response;
            }
            else
            {
                response = new UnknownMessage();
            }

            if (response == null)
            {
                return;
            }

            var str = MessageParser.Serialize(response);

            message.Reply(str);
        }

        #region IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                if (_server.IsStarted)
                {
                    _server.Stop();
                }
            }
        }

        #endregion

        #region ICommunicationServer

        public event EventHandler<RequestRecievedEventArgs> RequestRecieved;

        /// <summary>
        /// Запускает сервер с указанными параметрами
        /// </summary>
        /// <param name="host">Адрес хоста, на котором размещается сервер</param>
        /// <param name="serviceName">Имя службы (порт), на котором размещается сервер</param>
        public void Start(string host, string serviceName)
        {
            if (_server.IsStarted)
            {
                Stop();
            }

            int port;

            if (!int.TryParse(serviceName, out port))
            {
                throw new ArgumentException(@"Tcp server allow only port numbers", nameof(serviceName));
            }

            IPAddress ipAddress;
            if (!IPAddress.TryParse(host, out ipAddress))
            {
                throw new ArgumentException();
            }

            _server.Start(ipAddress, port);
        }

        /// <summary>
        /// Останавливает сервер
        /// </summary>
        public void Stop()
        {
            if (_server.IsStarted)
            {
                _server.Stop();
            }
        }

        /// <summary>
        /// Отправляет сообщение всем подключенным клиентам
        /// </summary>
        /// <param name="message"></param>
        public async void SendBroadcast(IMessage message)
        {
            await Task.Run(() => _server.Broadcast(MessageParser.Serialize(message)));
        }

        #endregion

        private void OnRequestRecieved(RequestRecievedEventArgs e)
        {            
            RequestRecieved?.Invoke(this, e);
        }
    }
}
