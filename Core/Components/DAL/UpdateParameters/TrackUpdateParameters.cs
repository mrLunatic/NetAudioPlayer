namespace Spartan.ServerCore.Components.DAL.UpdateParameters
{
    public sealed class TrackUpdateParameters : CommonUpdateParameters
    {
        public int? ArtistId { get; set; }

        public int? AlbumId { get; set; }

        public int? AlbumNumber { get; set; }

        public int? GenreId { get; set; }

        public int? Duration { get; set; }

        public string Uri { get; set; }
          
    }
}