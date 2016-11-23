using System.Collections.Generic;
using NetAudioPlayer.Common.Data;

namespace NetAudioPlayer.ServerCore.Components.DAL
{
    public sealed class AlbumRequestParameters : CommonRequestParameters
    {
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

        #region Overrides of CommonRequestParameters

        protected override IList<string> GetWhereInternal()
        {
            var where = base.GetWhereInternal();

            if (ArtistId.HasValue)
                where.Add($@"{Album.ArtistIdField} = {ArtistId}");

            if (string.IsNullOrEmpty(ArtistName))
                where.Add($@"{Album.ArtistNameField} LIKE %{ArtistName}%");

            if (YearMin.HasValue)
                where.Add($@"{Album.YearField} >= {YearMin}");

            if (YearMax.HasValue)
                where.Add($@"{Album.YearField} <= {YearMax}");

            if (Year.HasValue)
                where.Add($@"{Album.YearField} = {Year}");

            return where;
        }

        #endregion
    }
}