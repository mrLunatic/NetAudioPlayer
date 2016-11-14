namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// ��������� �������� ������ 
    /// </summary>
    public sealed class TrackRequestParameters : CommonRequestParameters
    {
        /// <summary>
        /// ������������� ����������� ������������� ����������
        /// </summary>
        public int? ArtistId { get; set; }

        /// <summary>
        /// ������������� �������, ����������� ������������� ����������
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// ������������� ����� ������������� ����������
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// ����� ������������� ����������
        /// </summary>
        string Uri { get; set; }
    }
}