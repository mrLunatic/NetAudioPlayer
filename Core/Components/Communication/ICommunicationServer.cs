using System;
using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.Core.Components.Communication
{
    /// <summary>
    /// ���������, ���������� �� ������� ������������
    /// </summary>
    public interface ICommunicationServer : IDisposable
    {
        /// <summary>
        /// �������, ������������ ��� ��������� ������ �������
        /// </summary>
        event EventHandler<RequestRecievedEventArgs> RequestRecieved; 

        /// <summary>
        /// ��������� ������ � ���������� �����������
        /// </summary>
        /// <param name="host">����� �����, �� ������� ����������� ������</param>
        /// <param name="serviceName">��� ������ (����), �� ������� ����������� ������</param>
        void Start(string host, string serviceName);

        /// <summary>
        /// ������������� ������
        /// </summary>
        void Stop();

        /// <summary>
        /// ���������� ��������� ���� ������������ ��������
        /// </summary>
        /// <param name="message"></param>
        void SendBroadcast(IMessage message);
    }
}