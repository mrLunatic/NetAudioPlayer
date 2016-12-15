using Spartan.Common.Attribute;
using Spartan.Common.Model;

namespace Spartan.Common.Message
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
