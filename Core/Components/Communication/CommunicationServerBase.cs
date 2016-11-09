using System;
using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.Core.Components.Communication
{
    public abstract class CommunicationServerBase : ICommunicationServer
    {
        #region Implementation of IDisposable

        /// <summary>
        /// ��������� ������������ ����������� ������, ��������� � �������������� ��� ������� ������������� ��������.
        /// </summary>
        public virtual void Dispose()
        {

        }

        #endregion

        #region Implementation of ICommunicationServer

        public event EventHandler<RequestRecievedEventArgs> RequestRecieved;

        /// <summary>
        /// ��������� ������ � ���������� �����������
        /// </summary>
        /// <param name="host">����� �����, �� ������� ����������� ������</param>
        /// <param name="serviceName">��� ������ (����), �� ������� ����������� ������</param>
        public abstract void Start(string host, string serviceName);

        /// <summary>
        /// ������������� ������
        /// </summary>
        public void Stop()
        {
            StopInternal();
        }

        /// <summary>
        /// ���������� ��������� ���� ������������ ��������
        /// </summary>
        /// <param name="message"></param>
        public void SendBroadcast(IMessage message)
        {
            if (message == null)
            {
                return;                
            }
            var str = MessageParser.Serialize(message);
            SendBroadcast(MessageParser.Serialize(message));
        }

        #endregion

        protected abstract void StopInternal();

        protected abstract void SendBroadcast(string data);

        protected string OnMessageRecieved(string message)
        {
            var msg = MessageParser.Parse(message);

            if (msg == null)
            {
                return null;
            }

            var response = OnRequestRecieved(msg);

            if (response == null)
            {
                return null;
            }

            return MessageParser.Serialize(response);
        }

        private IMessage OnRequestRecieved(IMessage request)
        {
            var args = new RequestRecievedEventArgs(request);

            RequestRecieved?.Invoke(this, args);

            return args.Response;
        }
    }
}