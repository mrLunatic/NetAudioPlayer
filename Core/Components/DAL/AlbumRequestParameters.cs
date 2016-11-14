namespace NetAudioPlayer.Core.Components.DAL
{
    public sealed class AlbumRequestParameters : CommonRequestParameters
    {
        /// <summary>
        /// ������������� �����������
        /// </summary>
        public int? ArtistId { get; set; }

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
    }
}