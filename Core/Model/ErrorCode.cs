namespace NetAudioPlayer.Core.Model
{
    /// <summary>
    /// Перечисление типов ошибок
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Неуказанная ошибка
        /// </summary>
        Unspecified,
        /// <summary>
        /// Сообщение было принято, но не было обработано
        /// </summary>
        Unhandled,
        /// <summary>
        /// Неизвестный тип сообщения
        /// </summary>
        UnknownMessageType,
        /// <summary>
        /// Неподдерживаемый тип сообщения
        /// </summary>
        UnsupportedMessageType,
        /// <summary>
        /// В сообщении было передано неправильное значение
        /// </summary>
        WrongValue
        
    }
}