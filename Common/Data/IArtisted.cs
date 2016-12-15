using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    public interface IArtisted
    {
        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [JsonProperty(DataContract.AlbumArtistIdField)]
        int ArtistId { get; }

        /// <summary>
        /// Имя исполнителя альбома
        /// </summary>
        [JsonProperty(DataContract.AlbumArtistNameField)]
        string ArtistName { get; }
    }
}
