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
        /// Идентификатор текущего трека
        /// </summary>
        [JsonProperty(@"trackId")]
        public int TrackId { get; set; }

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [JsonProperty(@"currentPosition")]
        public double CurrentPosition { get; set; }
    }
}
