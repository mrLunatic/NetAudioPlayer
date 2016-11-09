using System.Collections.Generic;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.Core.Components.DAL
{
    public interface IDal
    {
        #region Track

        #region Create

        Track CreateTrack(string name, int artistId, int albumId, double duration, string uri);

        #endregion

        #region Read

        Track GetTrack(int id);

        IEnumerable<Track> GetTracks(
            IEnumerable<int> ids = null,
            string name = null,
            int? artistId = null,
            int? albumId = null,
            string uri = null,
            RatingParameter rating = null,
            string tag = null,
            PageParameter page = null);

        #endregion

        #region Update

        Track UpdateTrack(Track track,
            string name = null,
            double? duration = null,
            int? artistId = null,
            int? albumId = null,
            string uri = null,
            int? rating = null,
            string tag = null);

        #endregion

        #region Delete

        int DeleteTrack(Track track);

        #endregion

        #endregion

        #region Artist

        #region Create

        Artist CreateArtist(string name);

        #endregion

        #region Read

        Artist GetArtist(int id);

        IEnumerable<Artist> GetArtists(
            IEnumerable<int> ids = null,
            string name = null,
            PageParameter page = null,
            RatingParameter rating = null,
            string tag = null);

        #endregion

        #region Update

        Artist UpdateArtist(Artist artist, string name = null);

        #endregion

        #region Delete

        int DeleteArtist(Artist artist);

        #endregion

        #endregion

        #region Album

        #region Create

        Album CreateAlbum(string name, int artistId, int? year, string tag);

        #endregion

        #region Read

        Album GetAlbum(int id);

        IEnumerable<Album> GetAlbums(
            IEnumerable<int> ids = null,
            int? artistId = null,
            IEnumerable<int> artistIds = null,
            int? yearMin = null,
            int? yearMax = null,
            int? year = null,
            PageParameter page = null,
            RatingParameter rating = null,            
            string tag = null);

        #endregion

        #region Update

        Album UpdateAlbum(Album album,
            string name = null,
            int? artistId = null,
            int? year = null,
            int? rating = null,
            string tag = null);

        #endregion

        #region Delete

        int DeleteAlbum(Album album);

        #endregion

        #endregion

        #region Genres

        #region Create

        Genre CreateGenre(string name, string tag = null);

        #endregion

        #region Read

        Genre GetGenre(int id);

        IEnumerable<Genre> GetGenres(IEnumerable<int> ids = null, RatingParameter rating = null, PageParameter page = null, string tag = null);

        #endregion

        #region Update

        Genre UpdateGenre(Genre genre, string name = null, int? rating = null, string tag = null);

        #endregion

        #region Delete

        int DeleteGenre(Genre genre);

        #endregion

        #endregion

        #region Strings

        string GetLocalizedString(string name, string lang);


        #endregion
    }
}
