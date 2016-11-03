using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Сообщение-комманда приостановки воспроизведения
    /// </summary>
    [Message]
    public class PauseMessage : MessageBase
    {
    }
}
