using System;
using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.AudioPlayerServer.Components
{
    /// <summary>
    /// Данные события полученного запроса
    /// </summary>
    public class RequestRecievedEventArgs : EventArgs
    {
        /// <summary>
        /// Полученный запрос
        /// </summary>
        public IMessage Request { get; }

        /// <summary>
        /// Ответ на запрос
        /// </summary>
        public IMessage Response { get; set; }

        /// <summary>
        /// Создает новый экземпляр класса
        /// </summary>
        /// <param name="request">Полученный запрос</param>
        public RequestRecievedEventArgs(IMessage request)
        {
            Request = request;
        }
    }
}