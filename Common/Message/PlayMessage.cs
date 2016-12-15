using System.Collections.Generic;
using Newtonsoft.Json;
using Spartan.Common.Attribute;
using Spartan.Common.Data;

namespace Spartan.Common.Message
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
        public IEnumerable<ITrack> Items { get; set; }

        /// <summary>
        /// Первый элемент для воспроизведения. 
        /// Если не указан, то используется первый элемент из списка
        /// </summary>
        [JsonProperty(@"item", NullValueHandling = NullValueHandling.Ignore)]
        public ITrack Item { get; set; }
    }
}
