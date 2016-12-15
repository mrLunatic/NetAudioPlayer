namespace Spartan.ServerCore.Components.DAL.UpdateParameters
{
    public sealed class AlbumUpdateParameters : CommonUpdateParameters
    {
        public int? ArtistId { get; set; }

        public int? Year { get; set; }
    }
}