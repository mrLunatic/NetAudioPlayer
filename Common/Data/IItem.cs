using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    /// <summary>
    /// Общий тип сущности
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Уникальный идентификатор 
        /// </summary>
        [JsonProperty(DataContract.ItemIdField)]
        int Id { get; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty(DataContract.ItemNameField)]
        string Name { get; }

        /// <summary>
        /// Рейтинг альбома
        /// </summary>
        [JsonProperty(DataContract.ItemRatingField)]
        int? Rating { get; }

        /// <summary>
        /// Дополнительная метка
        /// </summary>
        [JsonProperty(DataContract.ItemTagField)]
        string Tag { get; }
    }
}