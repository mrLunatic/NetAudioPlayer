using System.Collections.Generic;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    public sealed class AlbumRequestParameters : RequestParameters
    {
        #region DefaultValues

        public static readonly IEnumerable<SortingItem<AlbumSorting>> DefaultOrderBy
            = new[]
            {
                new SortingItem<AlbumSorting>(AlbumSorting.Year),
                new SortingItem<AlbumSorting>(AlbumSorting.Name),
            };

        #endregion

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// Примерное имя исполнителя альбома
        /// </summary>
        public string ArtistName { get; set; }

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

        /// <summary>
        /// Поля, по которым будет отсортирован результат запроса
        /// </summary>
        public IEnumerable<SortingItem<AlbumSorting>> OrderBy { get; set; } 

    }
}