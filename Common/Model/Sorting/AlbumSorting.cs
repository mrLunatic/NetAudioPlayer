using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.Common.Model.Sorting
{
    public enum AlbumSorting
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [EnumMember(Value = "id")]
        Id,
        /// <summary>
        /// Название альбома
        /// </summary>
        [EnumMember(Value = "name")]
        Name,
        /// <summary>
        /// Рейтинг альбома
        /// </summary>
        [EnumMember(Value = "rating")]
        Rating,
        /// <summary>
        /// Дополнительный тег
        /// </summary>
        [EnumMember(Value = "tag")]
        Tag,
        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [EnumMember(Value = "artistId")]
        ArtistId,
        /// <summary>
        /// Имя исполнителя 
        /// </summary>
        [EnumMember(Value = "artistName")]
        ArtistName,
        /// <summary>
        /// Идентификатор жанра
        /// </summary>
        [EnumMember(Value = "genreId")]
        GenreId,
        /// <summary>
        /// Название жанра
        /// </summary>
        [EnumMember(Value = "genreName")]
        GenreName,
        /// <summary>
        /// Год выпуска
        /// </summary>
        [EnumMember(Value = "year")]
        Year,
        /// <summary>
        /// Количество композиций
        /// </summary>
        [EnumMember(Value = "trackCount")]
        TracksCount
    }
}
