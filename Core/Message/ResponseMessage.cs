using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Простой ответ на запрос
    /// </summary>
    [Message]
    public class ResponseMessage : MessageBase
    {
        public ResponseMessage(Error error = null)
        {
            Error = error;
        }

        public ResponseMessage(ErrorCode errorCode, string message = null)
        {
            Error = new Error(errorCode, message);
        }       
    }
}
