using Newtonsoft.Json;

namespace NetAudioPlayer.Common.Data
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class Artist : Item
    {
        public const string AlbumsCountField = @"albumsCount";

        public const string TracksCountField = @"tracksCount";

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
