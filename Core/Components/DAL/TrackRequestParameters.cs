using System.Collections.Generic;
using NetAudioPlayer.Core.Data;

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
        /// Примерное имя исполнителя
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Идентификатор альбома, содержащего запрашиваемую композицию
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Примерное название альбома
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// Номер композиции в альбоме
        /// </summary>
        public int? AlbumNumber { get; set; }

        /// <summary>
        /// Идентификатор жанра запрашиваемой композиции
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// Примерное название жанра
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Адрес запрашиваемой композиции
        /// </summary>
        public string Uri { get; set; }

        #region Overrides of CommonRequestParameters

        protected override IList<string> GetWhereInternal()
        {
            var where = base.GetWhereInternal();

            if (ArtistId.HasValue)
                where.Add($@"{Track.ArtistIdField} = {ArtistId.Value}");

            if (!string.IsNullOrEmpty(ArtistName))
                where.Add($@"{Track.ArtistNameField} LIKE %{ArtistName}%");

            if (AlbumId.HasValue)
                where.Add($@"{Track.AlbumIdField} = {AlbumId.Value}");

            if (string.IsNullOrEmpty(AlbumName))
                where.Add($@"{Track.AlbumNameField} LIKE %{AlbumName}%");

            if (AlbumNumber.HasValue)
                where.Add($@"{Track.AlbumNumberField} = {AlbumNumber.Value}");

            if (GenreId.HasValue)
                where.Add($@"{Track.GenreIdField} = {GenreId}");

            if (string.IsNullOrEmpty(GenreName))
                where.Add($@"{Track.GenreNameField} LIKE %{GenreName}%");

            if (string.IsNullOrEmpty(Uri))
                where.Add($@"{Track.UriField} LIKE %{Uri}%");

            return where;

        }

        #endregion
    }
}