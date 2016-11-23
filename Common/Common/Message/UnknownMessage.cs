using NetAudioPlayer.Common.Attribute;
using NetAudioPlayer.Common.Model;

namespace NetAudioPlayer.Common.Message
{
    [Message]
    public sealed class UnknownMessage : MessageBase
    {
        public UnknownMessage()
        {
            Error = new Error(ErrorCode.UnknownMessageType);
        }
    }
}
