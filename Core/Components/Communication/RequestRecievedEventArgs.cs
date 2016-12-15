using System;
using Spartan.Common.Message;

namespace Spartan.ServerCore.Components.Communication
{
    /// <summary>
    /// ������ ������� ����������� �������
    /// </summary>
    public class RequestRecievedEventArgs : EventArgs
    {
        /// <summary>
        /// ���������� ������
        /// </summary>
        public IMessage Request { get; }

        /// <summary>
        /// ����� �� ������
        /// </summary>
        public IMessage Response { get; set; }

        /// <summary>
        /// ������� ����� ��������� ������
        /// </summary>
        /// <param name="request">���������� ������</param>
        public RequestRecievedEventArgs(IMessage request)
        {
            Request = request;
        }
    }
}