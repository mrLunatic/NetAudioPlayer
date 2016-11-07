﻿using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Data
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
        public double Duration { get; set; }

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
        /// Номер композиции в альбоме
        /// </summary>
        public int AlbumNumber { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        public string AlbumName { get; set; }      
        
        /// <summary>
        /// Путь к медиафайлу
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Рейтинг композиции
        /// </summary>
        [JsonProperty(@"rating")]
        public int? Rating { get; set; }

        /// <summary>
        /// Дополнительная метка
        /// </summary>
        [JsonProperty(@"tag")]
        public string Tag { get; set; }
    }
}