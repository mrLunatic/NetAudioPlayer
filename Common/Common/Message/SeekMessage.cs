using NetAudioPlayer.Common.Attribute;
using Newtonsoft.Json;

namespace NetAudioPlayer.Common.Message
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
