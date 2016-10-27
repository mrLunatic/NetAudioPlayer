using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Model.Json
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty(@"Id")]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [JsonProperty(@"Name")]
        public string Name { get; set; }        

        /// <summary>
        /// Количество альбомов
        /// </summary>
        [JsonProperty(@"AlbumsCount")]
        public int AlbumsCount { get; set; }

        /// <summary>
        /// Колечество композиций
        /// </summary>
        [JsonProperty(@"TracksCount")]
        public int TracksCount { get; set; }
    }
}
