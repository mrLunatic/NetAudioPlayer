namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// Параметры запроста списка 
    /// </summary>
    public sealed class TrackRequestParameters : CommonRequestParameters
    {
        /// <summary>
        /// Идентификатор исполнителя запрашиваемой композиции
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Идентификатор альбома, содержащего запрашиваемую композицию
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Идентификатор жанра запрашиваемой композиции
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// Адрес запрашиваемой композиции
        /// </summary>
        string Uri { get; set; }
    }
}