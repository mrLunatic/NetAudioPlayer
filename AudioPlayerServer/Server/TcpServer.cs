using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleTCP;

namespace AudioPlayerServer.Server
{
    internal sealed class TcpClientProxy : ITcpClient
    {
        private readonly Message _message;

        public TcpClientProxy(Message message)
        {
            _message = message;
        }

        public void Send(IMessage message)
        {
            var str = JsonConvert.SerializeObject(message);

            _message.Reply(str);
        }
    }

    internal sealed class TcpServer : ITcpServer
    {
        private readonly List<TcpClient> _clients = new List<TcpClient>();

        private readonly SimpleTcpServer _server = new SimpleTcpServer();                


        public event NewMessageRecievedHandler MessageRecieved;


        public TcpServer()
        {
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.DataReceived += OnDataReceived;
        }

        private void OnDataReceived(object sender, Message message)
        {
            var revievedMsg = JsonConvert.DeserializeObject<IMessage>(message.MessageString);

            var msg = revievedMsg;

            switch (revievedMsg.MessageType)
            {
                case nameof(PlayMessage):
                    msg = JsonConvert.DeserializeObject<PlayMessage>(message.MessageString);
                    break;
                case nameof(PauseMessage):
                    msg = JsonConvert.DeserializeObject<PauseMessage>(message.MessageString);
                    break;
                case nameof(StopMessage):
                    msg = JsonConvert.DeserializeObject<StopMessage>(message.MessageString);
                    break;
                case nameof(SeekMessage):
                    msg = JsonConvert.DeserializeObject<SeekMessage>(message.MessageString);
                    break;
                case nameof(StatusMessage):
                    msg = JsonConvert.DeserializeObject<StatusMessage>(message.MessageString);
                    break;
                default:
                    break;
            }

            OnMessageRecieved(new TcpClientProxy(message), msg);
        }

        private void OnClientDisconnected(object sender, TcpClient tcpClient)
        {
            _clients.Remove(tcpClient);
        }

        private void OnClientConnected(object sender, TcpClient tcpClient)
        {
            _clients.Add(tcpClient);
            
        }


        public void Start(string hostName, int port)
        {            
            _server.Start(IPAddress.Parse(hostName), port);            
        }

        public void SendToAll(IMessage message)
        {
            var str = JsonConvert.SerializeObject(message);

            var data = Encoding.UTF8.GetBytes(str);

            foreach (var tcpClient in _clients)
                using (var stream = tcpClient.GetStream())
                    stream.Write(data, 0, data.Length);
        }

        public void Stop()
        {
            _server.Stop();
            _clients.Clear();            
        }


        private void OnMessageRecieved(ITcpClient client, IMessage message)
        {
            MessageRecieved?.Invoke(client, message);
        }
    }
}
