using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NetAudioPlayer.Common.Data;

namespace NetAudioPlayer.ServerCore.Components.DAL
{
    public abstract class DalBase : IDal
    {
        #region Consts

        protected virtual string ArtistTable { get; } = @"artist";

        protected virtual string AlbumTable { get; } = @"album";

        protected virtual string GenreTable { get; } = @"genre";

        protected virtual string TrackTable { get; } = @"track";

        #endregion

        #region Fields

        private readonly string _dbFileName;

        private static readonly string[] TrackFields =
        {
            Track.IdField,
            Track.NameField,
            Track.ArtistIdField,
            Track.ArtistNameField,
            Track.AlbumIdField,
            Track.AlbumNameField,
            Track.AlbumNumberField,
            Track.GenreIdField,
            Track.GenreNameField,
            Track.DurationField,
            Track.UriField,
            Track.RatingField,
            Track.TagField
        };

        private static readonly string[] ArtistFields =
        {
            Artist.IdField,
            Artist.NameField,
            Artist.AlbumsCountField,
            Artist.TracksCountField,
            Artist.RatingField,
            Artist.TagField
        };

        private static readonly string[] AlbumFields =
        {
            Album.IdField,
            Album.NameField,
            Album.ArtistIdField,
            Album.ArtistNameField,
            Album.YearField,
            Album.TracksCountField
        };

        private static readonly string[] GenreFields =
        {
            Genre.IdField,
            Genre.NameField,
            Genre.RatingField,
            Genre.TagField
        };

        #endregion

        
        #region Implementation of IDal

        #region Track

        public int CreateTrack(string name, int artistId, int albumId, int albumNumber, int genreId, int duration, string uri, string tag)
        {
            return CreateItem(
                TrackTable,
                new Dictionary<string, object>
                {
                    { Track.NameField, name },
                    { Track.ArtistIdField, artistId },
                    { Track.AlbumIdField, albumId },
                    { Track.AlbumNumberField, albumNumber },
                    { Track.GenreIdField, genreId },
                    { Track.DurationField, duration },
                    { Track.UriField, uri },
                    { Track.TagField, tag }
                });
        }

        public Track GetTrack(int id)
        {
            return GetItem(TrackTable, id, TrackFields).GetTrack();
        }

        public IEnumerable<Track> GetTracks(TrackRequestParameters parameters)
        {
            return GetItems(TrackTable, TrackFields, parameters.GetWhere()).Select(p => p.GetTrack());
        }

        public bool UpdateTrack(int id, TrackUpdateParameters parameters)
        {
            return UpdateItem(TrackTable, id, parameters.GetParams());
        }

        public bool DeleteTrack(int id)
        {
            return DeleteItem(TrackTable, id);
        }

        #endregion

        #region Artist

        public int CreateArtist(string name, string tag)
        {
            return CreateItem(
                ArtistTable,
                new Dictionary<string, object>()
                {
                    { Artist.NameField, name},
                    { Artist.TagField, tag}
                });
        }

        public Artist GetArtist(int id)
        {
            return GetItem(ArtistTable, id, ArtistFields).GetArtist();
        }

        public IEnumerable<Artist> GetArtists(ArtistRequestParameters parameters)
        {
            return GetItems(ArtistTable, ArtistFields, parameters.GetWhere()).Select(p => p.GetArtist());
        }

        public bool UpdateArtist(int id, ArtistUpdateParameters parameters)
        {
            return UpdateItem(ArtistTable, id, parameters.GetParams());
        }

        public bool DeleteArtist(int id)
        {
            return DeleteItem(ArtistTable, id);
        }

        #endregion

        #region Album

        public int CreateAlbum(string name, int artistId, int year, string tag)
        {
            return CreateItem(
                AlbumTable,
                new Dictionary<string, object>()
                {
                    { Album.NameField, name},
                    { Album.ArtistIdField, artistId},
                    { Album.YearField, year},
                    { Album.TagField, tag}
                });
        }

        public Album GetAlbum(int id)
        {
            return GetItem(AlbumTable, id, AlbumFields).GetAlbum();
        }

        public IEnumerable<Album> GetAlbums(AlbumRequestParameters parameters)
        {
            return GetItems(AlbumTable, AlbumFields, parameters.GetWhere()).Select(p => p.GetAlbum());
        }

        public bool UpdateAlbum(int id, AlbumUpdateParameters parameters)
        {
            return UpdateItem(AlbumTable, id, parameters.GetParams());
        }

        public bool DeleteAlbum(int id)
        {
            return DeleteItem(AlbumTable, id);
        }

        #endregion

        #region Genre

        public int CreateGenre(string name, string tag)
        {
            return CreateItem(
                GenreTable, 
                new Dictionary<string, object>
                {
                    { Genre.NameField,  name },
                    { Genre.TagField,   tag ?? string.Empty }
                });
        }

        public Genre GetGenre(int id)
        {
            return GetItem(GenreTable, id, GenreFields).GetGenre();
        }

        public IEnumerable<Genre> GetGenres(GenreRequestParameters parameters)
        {
            return GetItems(GenreTable, GenreFields, parameters.GetWhere()).Select(p => p.GetGenre());
        }
                         
        public bool UpdateGenre(int id, GenreUpdateParameters parameters)
        {
            return UpdateItem(GenreTable, id, parameters.GetParams());
        }

        public bool DeleteGenre(int id)
        {
            return DeleteItem(GenreTable, id);
        }

        #endregion

        #endregion

        #region Private methods





        protected abstract bool Exists(string tableName, int id);

        protected abstract int CreateItem(string tableName, IDictionary<string, object> items);

        protected abstract IDictionary<string, object> GetItem(string tableName, int id, IEnumerable<string> fields);

        protected abstract IList<IDictionary<string, object>> GetItems(string tableName, IEnumerable<string> fields,
            string where);

        protected abstract bool UpdateItem(string tableName, int id, IDictionary<string, object> values);

        protected abstract bool DeleteItem(string tableName, int id);


        protected abstract void CreateDb();



        #endregion
    }
}
