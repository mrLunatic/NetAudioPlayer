using NetAudioPlayer.Common.Model;

namespace NetAudioPlayer.Common.Message
{
    /// <summary>
    /// Общее описание сообщения
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Локаль клиента
        /// </summary>
        string Lang { get; set; }

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        Error Error { get; set; }
    }
}
