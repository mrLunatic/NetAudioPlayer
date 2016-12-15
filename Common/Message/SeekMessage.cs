using Newtonsoft.Json;
using Spartan.Common.Attribute;

namespace Spartan.Common.Message
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
