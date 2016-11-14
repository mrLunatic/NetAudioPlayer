namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// Общие параметры обновления сущности
    /// </summary>
    public class CommonUpdateParameters
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Дополнительная метка
        /// </summary>
        public string Tag { get; set; }
    }
}