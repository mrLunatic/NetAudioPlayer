using System.Collections.Generic;
using Spartan.Common.Data;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    /// <summary>
    /// ��������� �������� ������ 
    /// </summary>    
    public sealed class TrackRequestParameters : RequestParameters
    {
        public static readonly IEnumerable<SortingItem<TrackSorting>> DefaultOrderBy
            = new[]
            {
                        new SortingItem<TrackSorting>(TrackSorting.Name),
            };

        /// <summary>
        /// ������������� ����������� ������������� ����������
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// ��������� ��� �����������
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// ������������� �������, ����������� ������������� ����������
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// ��������� �������� �������
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// ����� ���������� � �������
        /// </summary>
        public int? AlbumNumber { get; set; }

        /// <summary>
        /// ������������� ����� ������������� ����������
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// ����� ������������� ����������
        /// </summary>
        public string Uri { get; set; }

        public IEnumerable<SortingItem<TrackSorting>> OrderBy { get; set; }

    }
}