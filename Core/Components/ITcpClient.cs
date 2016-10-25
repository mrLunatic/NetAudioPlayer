using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.Core.Components
{
    interface ITcpClient
    {
        void Connect(string hostName, int port);

        void Send(IMessage message);
    }
}