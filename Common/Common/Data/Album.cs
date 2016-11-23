using Newtonsoft.Json;

namespace NetAudioPlayer.Common.Data
{
    /// <summary>
    /// Альбом
    /// </summary>
    public class Album : Item
    {
        public const string ArtistIdField = @"artistId";

        public const string ArtistNameField = @"artistName";

        public const string YearField = @"year";

        public const string TracksCountField = @"tracksCount";

        public const int DefaultId = 0;

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [JsonProperty(ArtistIdField)]
        public int ArtistId { get; set; }

        /// <summary>
        /// Имя исполнителя альбома
        /// </summary>
        [JsonProperty(ArtistNameField)]
        public string ArtistName { get; set; }

        /// <summary>
        /// Год выпуска альбома
        /// </summary>
        [JsonProperty(YearField)]
        public int Year { get; set; }

        /// <summary>
        /// Количество песен в альбоме
        /// </summary>
        [JsonProperty(TracksCountField)]
        public int TracksCount { get; set; }
    }
}
