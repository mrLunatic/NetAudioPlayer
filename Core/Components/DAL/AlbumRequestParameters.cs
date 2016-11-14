namespace NetAudioPlayer.Core.Components.DAL
{
    public sealed class AlbumRequestParameters : CommonRequestParameters
    {
        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Минимальный год альбома
        /// </summary>
        public int? YearMin { get; set; }

        /// <summary>
        /// Максимальный год альбома
        /// </summary>
        public int? YearMax { get; set; }

        /// <summary>
        /// Год альбома
        /// </summary>
        public int? Year { get; set; }
    }
}