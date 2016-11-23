using System;
using System.IO;

namespace NetAudioPlayer.ServerCore.Components.Player
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