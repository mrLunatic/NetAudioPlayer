using System;
using System.IO;
using NetAudioPlayer.AudioPlayerServer.Model;

namespace NetAudioPlayer.AudioPlayerServer.Components
{
    /// <summary>
    /// ��������� ������
    /// </summary>
    public interface IItemLoader : IDisposable
    {
        /// <summary>
        /// ��������� ��������� �������
        /// </summary>
        /// <param name="item">���� � ����������</param>
        /// <returns>����� ������� ���������, ��������� ��� ��������� ����������</returns>
        Stream LoadItem(string item);

        /// <summary>
        /// ������� �� ������ ��� ���������� ����������
        /// </summary>
        void Reset();
    }
}