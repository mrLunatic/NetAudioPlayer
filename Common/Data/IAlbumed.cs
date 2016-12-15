using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    public interface IAlbumed
    {
        /// <summary>
        /// Идентификатор альбома
        /// </summary>
        [JsonProperty(DataContract.TrackAlbumIdField)]
        int AlbumId { get; }

        /// <summary>
        /// Название альбома
        /// </summary>
        [JsonProperty(DataContract.TrackAlbumNameField)]
        string AlbumName { get; }
    }
}
