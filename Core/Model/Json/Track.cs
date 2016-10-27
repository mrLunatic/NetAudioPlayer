using System;

namespace NetAudioPlayer.Core.Model.Json
{
    /// <summary>
    /// Композиция
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название композиции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Длительность композиции
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Название исполнителя
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Идентификатор альбома
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        public string AlbumName { get; set; }        
    }
}
