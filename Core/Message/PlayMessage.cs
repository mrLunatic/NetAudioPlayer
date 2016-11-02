using System.Collections.Generic;
using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
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
        public IEnumerable<string> Items { get; set; }

        /// <summary>
        /// Первый элемент для воспроизведения. 
        /// Если не указан, то используется первый элемент из списка
        /// </summary>
        [JsonProperty(@"item")]
        public string Item { get; set; }
    }
}
