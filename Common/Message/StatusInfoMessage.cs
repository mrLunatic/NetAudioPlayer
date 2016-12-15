using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spartan.Common.Attribute;
using Spartan.Common.Data;
using Spartan.Common.Model;

namespace Spartan.Common.Message
{
    /// <summary>
    /// Сообщение о состоянии плеера
    /// </summary>
    [Message]
    public class StatusInfoMessage : MessageBase
    {
        /// <summary>
        /// Режим повтора
        /// </summary>
        [JsonProperty(@"repeat", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RepeatMode? Repeat { get; set; }

        /// <summary>
        /// Случайный порядок воспроизведения
        /// </summary>
        [JsonProperty(@"shuffle", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Shuffle { get; set; }

        /// <summary>
        /// Громкость воспроизведения. Задается в интервале [0.00 - 1.00]
        /// </summary>
        [JsonProperty(@"volume", NullValueHandling = NullValueHandling.Ignore)]
        public double? Volume { get; set; }

        /// <summary>
        /// Текущее состояние плеера
        /// </summary>
        [JsonProperty(@"state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerState State { get; set; }

        /// <summary>
        /// Текущий воспроизводимый элемент
        /// </summary>
        [JsonProperty(@"currentItem")]
        public ITrack CurrentItem { get; set; }

        /// <summary>
        /// Список воспроизведения
        /// </summary>
        [JsonProperty(@"items")]
        public IEnumerable<ITrack> Items { get; set; } 

        /// <summary>
        /// Длительность текущего элемента
        /// </summary>
        [JsonProperty(@"currentItemDuration", NullValueHandling = NullValueHandling.Ignore)]
        public double? CurrentItemDuration { get; set; }

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [JsonProperty(@"currentPosition", NullValueHandling = NullValueHandling.Ignore)]
        public double? CurrentPosition { get; set; }

    }
}
