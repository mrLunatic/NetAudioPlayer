using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    public interface IGenred
    {
        /// <summary>
        /// Идентификатор жанра
        /// </summary>
        [JsonProperty(DataContract.AlbumGenreIdField)]
        int GenreId { get; }

        /// <summary>
        /// Название жанра
        /// </summary>
        [JsonProperty(DataContract.AlbumGenreNameField)]
        string GenreName { get; }
    }
}
