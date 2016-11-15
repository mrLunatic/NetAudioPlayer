using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class Artist : Item
    {
        public const string AlbumsCountField = @"albumsCount";

        public const string TracksCountField = @"tracksCountField";

        public const int DefaultId = 0;

        /// <summary>
        /// Количество альбомов
        /// </summary>
        [JsonProperty(AlbumsCountField)]
        public int AlbumsCount { get; set; }

        /// <summary>
        /// Колечество композиций
        /// </summary>
        [JsonProperty(TracksCountField)]
        public int TracksCount { get; set; }
    }
}
