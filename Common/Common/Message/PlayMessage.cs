using System.Collections.Generic;
using NetAudioPlayer.Common.Attribute;
using NetAudioPlayer.Common.Data;
using Newtonsoft.Json;

namespace NetAudioPlayer.Common.Message
{
    /// <summary>
    /// Сообщение-комманда воспроизведения списка элементов
    /// </summary>
    [Message]
    public class PlayMessage : MessageBase
    {
        /// <summary>
        /// Список элементов для воспроизведения
        /// </summary>
        [JsonProperty(@"items")]
        public IEnumerable<Track> Items { get; set; }

        /// <summary>
        /// Первый элемент для воспроизведения. 
        /// Если не указан, то используется первый элемент из списка
        /// </summary>
        [JsonProperty(@"item", NullValueHandling = NullValueHandling.Ignore)]
        public Track Item { get; set; }
    }
}
