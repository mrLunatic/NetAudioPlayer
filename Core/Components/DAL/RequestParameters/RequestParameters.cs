using System.Collections.Generic;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    /// <summary>
    /// Общие параметры запроса группы сущностей
    /// </summary>
    public abstract class RequestParameters
    {
        #region Constants

        public const string DefaultCondition = @"1 = 1";

        public const int DefaultLimit = 1000;

        public const int DefaultOffset = 0;

        #endregion

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
        public int? Limit { get; set; }

    }
}
