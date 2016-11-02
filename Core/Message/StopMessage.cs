using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение-комманда остановки воспроизведения
    /// </summary>
    [Message]
    public class StopMessage : MessageBase
    {
    }
}
