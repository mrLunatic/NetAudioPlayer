using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.Core.Message
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
