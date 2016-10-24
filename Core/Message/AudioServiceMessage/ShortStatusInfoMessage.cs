using System;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Короткое сообщение о текущем статусе плеера
    /// </summary>
    public class ShortStatusInfoMessage  : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(ShortStatusInfoMessage);

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"Error")]
        public Error Error { get; set; }

        #endregion

        /// <summary>
        /// Воспроизводимый элемент
        /// </summary>
        [JsonProperty(@"Item")]
        public string Item { get; set; }

        /// <summary>
        /// Длительность текущего элемента
        /// </summary>
        [JsonProperty(@"ItemDuration")]
        public TimeSpan ItemDuration { get; set; }

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [JsonProperty(@"CurrentPosition")]
        public TimeSpan CurrentPosition { get; set; }
    }
}
