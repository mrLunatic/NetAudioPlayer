namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// Параметры пагинации
    /// </summary>
    public class PageParameter
    {
        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// Номер запрашиваемой страницы
        /// </summary>
        public int Index { get; set; }
    }
}
