using System;
using System.Net;
using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.AudioPlayerServer.Components
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
        void Start(IPAddress host, string serviceName);

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