using System;
using System.Collections.Generic;
using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Converters;
using NetAudioPlayer.Core.Model;
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
        [JsonProperty(@"repeat"), JsonConverter(typeof(StringEnumConverter))]
        public RepeatMode Repeat { get; set; }

        /// <summary>
        /// Случайный порядок воспроизведения
        /// </summary>
        [JsonProperty(@"shuffle")]
        public bool Shuffle { get; set; }

        /// <summary>
        /// Громкость воспроизведения. Задается в интервале [0.00 - 1.00]
        /// </summary>
        [JsonProperty(@"volume")]
        public double Volume { get; set; }

        /// <summary>
        /// Текущее состояние плеера
        /// </summary>
        [JsonProperty(@"state"), JsonConverter(typeof(StringEnumConverter))]
        public PlayerState State { get; set; }

        /// <summary>
        /// Текущий воспроизводимый элемент
        /// </summary>
        [JsonProperty(@"currentItem")]
        public string CurrentItem { get; set; }

        /// <summary>
        /// Список воспроизведения
        /// </summary>
        [JsonProperty(@"items")]
        public IEnumerable<string> Items { get; set; } 

        /// <summary>
        /// Длительность текущего элемента
        /// </summary>
        [JsonProperty(@"currentItemDuration"), JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan CurrentItemDuration { get; set; }

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [JsonProperty(@"currentPosition"), JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan CurrentPosition { get; set; }

    }
}
