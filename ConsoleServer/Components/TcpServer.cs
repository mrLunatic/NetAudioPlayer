using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Components.Communication;

namespace NetAudioPlayer.ConsoleServer.Components
{
    public sealed class TcpServer : CommunicationServerBase
    {
        private CancellationTokenSource _cts;

        private TcpListener _listener;

        private Task _listenerTask; 

        private readonly IDictionary<TcpClient, Task>  _clients = new ConcurrentDictionary<TcpClient, Task>();


        public override void Start(string host, string port)
        {            


            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            _listener = new TcpListener(IPAddress.Parse(host), int.Parse(port));

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
