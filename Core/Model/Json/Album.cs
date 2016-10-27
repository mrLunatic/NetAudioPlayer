using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Model.Json
{
    /// <summary>
    /// Альбом
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Уникальный идентификатор 
        /// </summary>
        [JsonProperty(@"Id")]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [JsonProperty(@"Artist")]
        public Artist Artist { get; set; }

        /// <summary>
        /// Год выпуска альбома
        /// </summary>
        [JsonProperty(@"Year")]
        public int Year { get; set; }

        /// <summary>
        /// Количество песен в альбоме
        /// </summary>
        [JsonProperty(@"TrackCount")]
        public int TrackCount { get; set; }
    }
}
