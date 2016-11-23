using System.Collections.Generic;
using NetAudioPlayer.Common.Data;

namespace NetAudioPlayer.ServerCore.Components.DAL
{
    /// <summary>
    /// Общие параметры обновления сущности
    /// </summary>
    public abstract class CommonUpdateParameters
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

        public virtual IDictionary<string, object> GetParams()
        {
            var items = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Name))
                items.Add(Item.NameField, Name);

            if (Rating.HasValue)
                items.Add(Item.RatingField, Rating.Value);

            if (!string.IsNullOrEmpty(Tag))
                items.Add(Item.TagField, Tag);

            return items;
        }

        
             

    }
}