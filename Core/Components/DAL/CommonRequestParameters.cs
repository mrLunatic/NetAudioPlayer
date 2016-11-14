using System.Collections.Generic;

namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// Общие параметры запроса группы сущностей
    /// </summary>
    public class CommonRequestParameters
    {
        /// <summary>
        /// Список индентификаторов запрашиваемых элементов
        /// </summary>
        public IEnumerable<int> Ids { get; set; }

        /// <summary>
        /// Примерное имя элементов
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Примерное значение дополнительной метки
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Минимальный рейтинг элемента
        /// </summary>
        public int? RatingMin { get; set; }

        /// <summary>
        /// Максимальный рейтинг элемента
        /// </summary>
        public int? RatingMax { get; set; }

        /// <summary>
        /// Значение рейтинга элемента
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Начиная с указанного идентификатора
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Максимальное количество возвращаемых элементов
        /// </summary>
        public int? MaxCount { get; set; }
    }
}
