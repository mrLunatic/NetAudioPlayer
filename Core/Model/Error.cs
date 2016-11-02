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
        [JsonProperty(@"code")]
        public ErrorCode Code { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [JsonProperty(@"message")]
        public string Message { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Error()
        {   
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Error(ErrorCode code, string message = null)
        {
            Code = code;
            Message = message;
        }
    }
}
