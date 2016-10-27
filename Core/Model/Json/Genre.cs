using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Model.Json
{
    /// <summary>
    /// Жанр
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty(@"Id")]
        public int Id { get; private set; }

        /// <summary>
        /// Название жанра
        /// </summary>
        [JsonProperty(@"Name")]
        public string Name { get; set; }
    }
}
