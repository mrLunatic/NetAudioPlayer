using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Сообщение-команда установки статуса сообщения
    /// </summary>
    [Message]
    public class StatusMessage : MessageBase
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
    }
}
