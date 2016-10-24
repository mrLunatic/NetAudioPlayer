using System;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetAudioPlayer.Core.Message.AudioServiceMessage
{
    /// <summary>
    /// Сообщение-комманда перемотки
    /// </summary>
    public class SeekMessage : IMessage
    {
        #region IMessage

        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonProperty(@"Type")]
        public string Type => nameof(SeekMessage);

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        [JsonProperty(@"Error")]
        public Error Error { get; set; }

        #endregion

        /// <summary>
        /// Позиция, на которую необходимо перемотать
        /// </summary>
        [JsonProperty(@"Position")]
        public TimeSpan Position { get; set; }
    }
}
