using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
{
    /// <summary>
    /// Жанр
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty(@"Id")]
        public int Id { get; private set; }

        /// <summary>
        /// Название жанра
        /// </summary>
        [JsonProperty(@"Name")]
        public string Name { get; set; }

        /// <summary>
        /// Рейтинг исполнителя
        /// </summary>
        [JsonProperty(@"rating")]
        public int? Rating { get; set; }

        /// <summary>
        /// Дополнительная метка
        /// </summary>
        [JsonProperty(@"tag")]
        public string Tag { get; set; }
    }
}
