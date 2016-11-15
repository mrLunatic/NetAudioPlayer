using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
{
    /// <summary>
    /// Общий тип сущности
    /// </summary>
    public class Item
    {
        #region Constants

        public const string IdField = @"id";

        public const string NameField = @"name";

        public const string RatingField = @"rating";

        public const string TagField = @"tag";

        #endregion

        /// <summary>
        /// Уникальный идентификатор 
        /// </summary>
        [JsonProperty(IdField)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty(NameField)]
        public string Name { get; set; }

        /// <summary>
        /// Рейтинг альбома
        /// </summary>
        [JsonProperty(RatingField)]
        public int? Rating { get; set; }

        /// <summary>
        /// Дополнительная метка
        /// </summary>
        [JsonProperty(TagField)]
        public string Tag { get; set; }

        #region Overrides of Object

        /// <summary>
        /// Возвращает строку, представляющую текущий объект.
        /// </summary>
        /// <returns>
        /// Строка, представляющая текущий объект.
        /// </returns>
        public override string ToString()
        {
            return $@"{Id} : {Name}. ({Tag})";
        }

        #endregion
    }
}