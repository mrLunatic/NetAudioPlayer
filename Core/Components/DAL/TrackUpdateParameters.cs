namespace NetAudioPlayer.Core.Components.DAL
{
    public sealed class TrackUpdateParameters : CommonUpdateParameters
    {
        public int? ArtistId { get; set; }

        public int? AlbumId { get; set; }

        public int? GenreId { get; set; }

        public int? Duration { get; set; }

        public string Uri { get; set; }
          
    }
}