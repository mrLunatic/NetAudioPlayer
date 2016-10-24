using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение-команда установки статуса сообщения
    /// </summary>
    public class StatusMessage : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(StatusMessage);

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
        public RepeatMode? Repeat { get; set; }

        /// <summary>
        /// Случайный порядок воспроизведения
        /// </summary>
        [JsonProperty(@"Shuffle")]
        public bool? Shuffle { get; set; }

        /// <summary>
        /// Громкость воспроизведения. Задается в интервале [0.00 - 1.00]
        /// </summary>
        [JsonProperty(@"Volume")]
        public double? Volume { get; set; }
    }
}
