using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.Core.Components
{
    delegate void NewMessageRecievedHandler(ITcpClient client, IMessage message);

    interface ITcpServer
    {
        event NewMessageRecievedHandler MessageRecieved;

        void Start(string hostName, int port);

        void SendToAll(IMessage message);

        void Stop();        
    }
}
