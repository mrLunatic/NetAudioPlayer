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
        /// ������������� �����������
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// ��������� ��� ����������� �������
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// ����������� ��� �������
        /// </summary>
        public int? YearMin { get; set; }

        /// <summary>
        /// ������������ ��� �������
        /// </summary>
        public int? YearMax { get; set; }

        /// <summary>
        /// ��� �������
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// ����, �� ������� ����� ������������ ��������� �������
        /// </summary>
        public IEnumerable<SortingItem<AlbumSorting>> OrderBy { get; set; } 

    }
}