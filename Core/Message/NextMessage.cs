using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Сообщение-комманда перехода к следующему элементу
    /// </summary>
    [Message]
    public class NextMessage : MessageBase
    {
    }
}
