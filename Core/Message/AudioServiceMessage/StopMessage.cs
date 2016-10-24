using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение-комманда остановки воспроизведения
    /// </summary>
    public class StopMessage : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(StopMessage);

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"Error")]
        public Error Error { get; set; }

        #endregion
    }
}
