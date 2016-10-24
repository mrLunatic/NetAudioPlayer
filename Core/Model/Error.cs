using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Model
{
    /// <summary>
    /// Общий класс ошибки
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonProperty(@"Code")]
        public ErrorCode Code { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [JsonProperty(@"Message")]
        public string Message { get; set; }
    }
}
