using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Базовый класс для сообщений
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"type")]
        public string Type => GetType().Name;

        /// <summary>
        /// Локаль клиента
        /// </summary>
        [JsonProperty(@"lang")]
        public string Lang { get; set; }

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"error")]
        public Error Error { get; set; }

        #endregion
    }
}
