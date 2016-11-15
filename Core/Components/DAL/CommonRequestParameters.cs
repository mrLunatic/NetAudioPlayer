using System.Collections.Generic;
using System.Text;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.Core.Components.DAL
{
    /// <summary>
    /// Общие параметры запроса группы сущностей
    /// </summary>
    public abstract class CommonRequestParameters
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


        public string GetWhere()
        {
            var where = GetWhereInternal();

            return string.Join(" AND ", where);
        }

        protected virtual IList<string> GetWhereInternal()
        {
            var where = new List<string>();

            if (Ids != null)
                where.Add($@"{Item.IdField} IN ({string.Join(",", Ids)})");

            if (!string.IsNullOrEmpty(Name))
                where.Add($@"{Item.NameField} LIKE '%{Name}%'");

            if (!string.IsNullOrEmpty(Tag))
                where.Add($@"{Item.TagField} LIKE '%{Tag}%'");

            if (RatingMin.HasValue)
                where.Add($@"{Item.RatingField} >= {RatingMin}");

            if (RatingMax.HasValue)
                where.Add($@"{Item.RatingField} <= {RatingMax}");

            if (Rating.HasValue)
                where.Add($@"{Item.RatingField} = {Rating}");

            if (Offset.HasValue)
                where.Add($@"{Item.IdField} > {Offset}");

            return where;
        }
    }
}
