using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    /// <summary>
    /// Альбом
    /// </summary>
    public interface IAlbum : IItem, ITrackStore, IGenred, IArtisted
    {
        /// <summary>
        /// Год выпуска альбома
        /// </summary>
        [JsonProperty(DataContract.AlbumYearField)]
        int Year { get; }
    }
}
