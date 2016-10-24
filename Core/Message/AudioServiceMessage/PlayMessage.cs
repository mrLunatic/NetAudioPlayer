using System.Collections.Generic;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение-комманда воспроизведения списка элементов
    /// </summary>
    public class PlayMessage : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(PlayMessage);

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"Error")]
        public Error Error { get; set; }

        #endregion

        /// <summary>
        /// Список элементов для воспроизведения
        /// </summary>
        [JsonProperty(@"Items")]
        public IEnumerable<string> Items { get; set; }

        /// <summary>
        /// Первый элемент для воспроизведения. 
        /// Если не указан, то используется первый элемент из списка
        /// </summary>
        [JsonProperty(@"Item")]
        public string Item { get; set; }
    }
}
