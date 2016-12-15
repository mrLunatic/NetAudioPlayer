using System;
using Spartan.Common.Attribute;
using Spartan.Common.Model;

namespace Spartan.Common.Message
{
    /// <summary>
    /// Простой ответ на запрос
    /// </summary>
    [Message]
    public class ResponseMessage : MessageBase
    {

        public ResponseMessage()
        {
            
        }

        public ResponseMessage(IMessage request, Error error = null) : base(request.Id)
        {
            Error = error;
        }

        public ResponseMessage(IMessage request, ErrorCode errorCode, string message = null) : base(request.Id)
        {
            Error = new Error(errorCode, message);
        }

        public ResponseMessage(IMessage request, Exception e) : base(request.Id)
        {
            Error = new Error(ErrorCode.Unspecified, $@"{e.GetType().Name}: {e.Message}");
        }
    }
}
