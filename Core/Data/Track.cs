using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
{
    /// <summary>
    /// Композиция
    /// </summary>
    public class Track : Item
    {
        public const string DurationField = @"duration";

        public const string ArtistIdField = @"aristId";

        public const string ArtistNameField = @"artistName";

        public const string AlbumIdField = @"albumId";

        public const string AlbumNumberField = @"albumNumber";

        public const string AlbumNameField = @"albumName";

        public const string GenreIdField = @"genreId";

        public const string GenreNameField = @"genreName";

        public const string UriField = @"uriField";

        public const int DefaultId = 0;

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [JsonProperty(ArtistIdField)]
        public int ArtistId { get; set; }

        /// <summary>
        /// Название исполнителя
        /// </summary>
        [JsonProperty(ArtistNameField)]
        public string ArtistName { get; set; }

        /// <summary>
        /// Идентификатор альбома
        /// </summary>
        [JsonProperty(AlbumIdField)]
        public int AlbumId { get; set; }

        /// <summary>
        /// Номер композиции в альбоме
        /// </summary>
        [JsonProperty(AlbumNumberField)]
        public int AlbumNumber { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        [JsonProperty(AlbumNameField)]
        public string AlbumName { get; set; }

        /// <summary>
        /// Жанр композиции
        /// </summary>
        [JsonProperty(GenreIdField)]
        public int GenreId { get; set; }

        /// <summary>
        /// Название жанра
        /// </summary>
        [JsonProperty(GenreNameField)]
        public string GenreName { get; set; }

        /// <summary>
        /// Длительность композиции
        /// </summary>
        [JsonProperty(DurationField)]
        public int Duration { get; set; }

        /// <summary>
        /// Путь к медиафайлу
        /// </summary>
        [JsonProperty(UriField)]
        public string Uri { get; set; }
    }
}
