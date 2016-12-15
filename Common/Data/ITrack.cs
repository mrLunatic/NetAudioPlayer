using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    /// <summary>
    /// Композиция
    /// </summary>
    public interface ITrack : IItem, IGenred, IArtisted, IAlbumed
    {
        /// <summary>
        /// Номер композиции в альбоме
        /// </summary>
        [JsonProperty(DataContract.TrackAlbumNumberField)]
        int AlbumNumber { get;}

        /// <summary>
        /// Длительность композиции
        /// </summary>
        [JsonProperty(DataContract.TrackDurationField)]
        int Duration { get; }

        /// <summary>
        /// Путь к медиафайлу
        /// </summary>
        [JsonProperty(DataContract.TrackUriField)]
        string Uri { get; }
    }
}
