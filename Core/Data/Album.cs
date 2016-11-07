using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
{
    /// <summary>
    /// Альбом
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Уникальный идентификатор 
        /// </summary>
        [JsonProperty(@"id")]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [JsonProperty(@"artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Год выпуска альбома
        /// </summary>
        [JsonProperty(@"year")]
        public int Year { get; set; }

        /// <summary>
        /// Количество песен в альбоме
        /// </summary>
        [JsonProperty(@"trackCount")]
        public int TrackCount { get; set; }

        /// <summary>
        /// Рейтинг альбома
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
