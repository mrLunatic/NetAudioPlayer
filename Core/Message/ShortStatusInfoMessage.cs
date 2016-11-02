using System;
using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Converters;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Короткое сообщение о текущем статусе плеера
    /// </summary>
    [Message]
    public class ShortStatusInfoMessage  : MessageBase
    {
        /// <summary>
        /// Воспроизводимый элемент
        /// </summary>
        [JsonProperty(@"item")]
        public string Item { get; set; }

        /// <summary>
        /// Длительность текущего элемента
        /// </summary>
        [JsonProperty(@"itemDuration"), JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan ItemDuration { get; set; }

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [JsonProperty(@"currentPosition"), JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan CurrentPosition { get; set; }
    }
}
