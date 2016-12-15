using System.Collections.Generic;
using Spartan.Common.Data;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    /// <summary>
    /// Параметры запроста списка 
    /// </summary>    
    public sealed class TrackRequestParameters : RequestParameters
    {
        public static readonly IEnumerable<SortingItem<TrackSorting>> DefaultOrderBy
            = new[]
            {
                        new SortingItem<TrackSorting>(TrackSorting.Name),
            };

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

        public IEnumerable<SortingItem<TrackSorting>> OrderBy { get; set; }

    }
}