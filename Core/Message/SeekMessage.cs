using System;
using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Converters;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Сообщение-комманда перемотки
    /// </summary>
    [Message]
    public class SeekMessage : MessageBase
    {
        /// <summary>
        /// Позиция, на которую необходимо перемотать
        /// </summary>
        [JsonProperty(@"position")]
        public double Position { get; set; }
    }
}
