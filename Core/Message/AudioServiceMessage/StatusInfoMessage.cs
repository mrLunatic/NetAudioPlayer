using System;
using System.Collections.Generic;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение о состоянии плеера
    /// </summary>
    public class StatusInfoMessage : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(StatusInfoMessage);

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"Error")]
        public Error Error { get; set; }

        #endregion

        /// <summary>
        /// Режим повтора
        /// </summary>
        [JsonProperty(@"Repeat", ItemConverterType = typeof(StringEnumConverter))]
        public RepeatMode Repeat { get; set; }

        /// <summary>
        /// Случайный порядок воспроизведения
        /// </summary>
        [JsonProperty(@"Shuffle")]
        public bool Shuffle { get; set; }

        /// <summary>
        /// Громкость воспроизведения. Задается в интервале [0.00 - 1.00]
        /// </summary>
        [JsonProperty(@"Volume")]
        public double Volume { get; set; }

        /// <summary>
        /// Текущее состояние плеера
        /// </summary>
        [JsonProperty(@"State", ItemConverterType = typeof(StringEnumConverter))]
        public PlayerState State { get; set; }

        /// <summary>
        /// Текущий воспроизводимый элемент
        /// </summary>
        [JsonProperty(@"CurrentItem")]
        public string CurrentItem { get; set; }

        /// <summary>
        /// Список воспроизведения
        /// </summary>
        [JsonProperty(@"Items")]
        public IEnumerable<string> Items { get; set; } 

        /// <summary>
        /// Длительность текущего элемента
        /// </summary>
        [JsonProperty(@"CurrentItemDuration")]
        public TimeSpan CurrentItemDuration { get; set; }

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [JsonProperty(@"CurrentPosition")]
        public TimeSpan CurrentPosition { get; set; }

    }
}
