using System;
using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.Core.Components.Communication
{
    /// <summary>
    /// Компонент, отвечающий за сетевую коммуникацию
    /// </summary>
    public interface ICommunicationServer : IDisposable
    {
        /// <summary>
        /// Событие, генерируемое при получении нового запроса
        /// </summary>
        event EventHandler<RequestRecievedEventArgs> RequestRecieved; 

        /// <summary>
        /// Запускает сервер с указанными параметрами
        /// </summary>
        /// <param name="host">Адрес хоста, на котором размещается сервер</param>
        /// <param name="serviceName">Имя службы (порт), на котором размещается сервер</param>
        void Start(string host, string serviceName);

        /// <summary>
        /// Останавливает сервер
        /// </summary>
        void Stop();

        /// <summary>
        /// Отправляет сообщение всем подключенным клиентам
        /// </summary>
        /// <param name="message"></param>
        void SendBroadcast(IMessage message);
    }
}