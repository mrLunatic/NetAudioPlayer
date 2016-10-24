using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение-комманда перехода к следующему элементу
    /// </summary>
    public class NextMessage : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(NextMessage);

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"Error")]
        public Error Error { get; set; }

        #endregion
    }
}
