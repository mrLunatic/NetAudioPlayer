namespace NetAudioPlayer.ServerCore.Components.DAL
{
    public sealed class AlbumUpdateParameters : CommonUpdateParameters
    {
        public int ArtistId { get; set; }

        public int Year { get; set; }
    }
}