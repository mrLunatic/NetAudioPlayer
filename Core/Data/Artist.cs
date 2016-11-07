using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty(@"Id")]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [JsonProperty(@"Name")]
        public string Name { get; set; }        

        /// <summary>
        /// Количество альбомов
        /// </summary>
        [JsonProperty(@"AlbumsCount")]
        public int AlbumsCount { get; set; }

        /// <summary>
        /// Колечество композиций
        /// </summary>
        [JsonProperty(@"TracksCount")]
        public int TracksCount { get; set; }

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
