using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public interface IArtist : IItem, ITrackStore
    {
        /// <summary>
        /// Количество альбомов
        /// </summary>
        [JsonProperty(DataContract.ArtistAlbumsCountField)]
        int AlbumsCount { get; }
    }
}
