namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// ����� ��������� ���������� ��������
    /// </summary>
    public class CommonUpdateParameters
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// �������������� �����
        /// </summary>
        public string Tag { get; set; }
    }
}