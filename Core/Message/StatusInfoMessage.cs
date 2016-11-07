using System;
using System.Collections.Generic;
using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Converters;
using NetAudioPlayer.Core.Model;
using NetAudioPlayer.Core.Model.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetAudioPlayer.Core.Message
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
        public Track CurrentItem { get; set; }

        /// <summary>
        /// Список воспроизведения
        /// </summary>
        [JsonProperty(@"items")]
        public IEnumerable<Track> Items { get; set; } 

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
