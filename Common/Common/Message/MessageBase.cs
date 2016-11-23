using NetAudioPlayer.Common.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Common.Message
{
    /// <summary>
    /// Базовый класс для сообщений
    /// </summary>
    public class MessageBase : IMessage
    {
        private static int _counter;

        #region IMessage

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        [JsonProperty(@"id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"type")]
        public string Type { get; private set; } 

        /// <summary>
        /// Локаль клиента
        /// </summary>
        [JsonProperty(@"lang", NullValueHandling = NullValueHandling.Ignore)]
        public string Lang { get; set; }

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"error", NullValueHandling = NullValueHandling.Ignore)]
        public Error Error { get; set; }

        #endregion

        public MessageBase()
        {
            Type = GetType().Name;
            Id = _counter++.ToString("D");
        }

        public MessageBase(string id)
        {
            Type = GetType().Name;
            Id = id;
        }
    }
}
