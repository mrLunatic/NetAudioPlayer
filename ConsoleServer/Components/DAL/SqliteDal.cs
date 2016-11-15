using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using NetAudioPlayer.Core.Components.DAL;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.ConsoleServer.Components.DAL
{
    public sealed class SqliteDal : IDal
    {
        #region Consts

        private const string ArtistTable = @"artist";

        private const string AlbumTable = @"album";

        private const string GenreTable = @"genre";

        private const string TrackTable = @"track";

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


        public SqliteDal(string dbFileName)
        {
            _dbFileName = dbFileName;

            if (!File.Exists(_dbFileName))
            {
                SQLiteConnection.CreateFile(_dbFileName);
                CreateDb();
            }
        }
        
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





        private bool Exists(string tableName, int id)
        {
            var req = $@"SELECT EXISTS (SELECT {Item.IdField} FROM {tableName} WHERE {Item.IdField} = {id} LIMIT 1);";

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = req.ToString();

                connection.Open();

                var exist = Convert.ToInt32(command.ExecuteScalar()) > 0;

                connection.Close();

                return exist;
            }
        }

        private int CreateItem(string tableName, IDictionary<string, object> items)
        {
            var cmd = new StringBuilder();
            cmd.AppendLine($@"INSERT INTO {tableName} ({string.Join(",", items.Keys)})");
            cmd.AppendLine($@"VALUES ({string.Join(",", items.Values.Select(p => p.AsSqlString()))});");
            cmd.AppendLine($@"SELECT last_insert_rowid();");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();

                connection.Open();

                var id = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return id;
            }
        }

        private IDictionary<string, object> GetItem(string tableName, int id, IEnumerable<string> fields)
        {
            var cmd = new StringBuilder();
            cmd.AppendLine($@"SELECT {string.Join(",", fields)}");
            cmd.AppendLine($@"FROM {tableName}");
            cmd.AppendLine($@"WHERE {Item.IdField} = {id}");
            cmd.AppendLine($@"LIMIT 1;");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();
                connection.Open();
                var reader = command.ExecuteReader();
                var values = new Dictionary<string, object>();

                if (reader.Read())
                {
                    foreach (var key in fields)
                    {
                        values.Add(key, reader[key]);
                    }
                }

                connection.Close();
                return values;
            }
        }

        private IList<IDictionary<string, object>> GetItems(string tableName, IEnumerable<string> fields, string where)
        {
            var cmd = new StringBuilder();
            cmd.AppendLine($@"SELECT {string.Join(",", fields)}");
            cmd.AppendLine($@"FROM {tableName}");
            cmd.AppendLine($@"WHERE {where};");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();
                connection.Open();

                var reader = command.ExecuteReader();
                var items = new List<IDictionary<string, object>>();

                while (reader.Read())
                {
                    var values = new Dictionary<string, object>();

                    foreach (var key in fields)
                    {
                        values.Add(key, reader[key]);
                    }

                    items.Add(values);
                }

                connection.Close();
                return items;
            }
        }

        private bool UpdateItem(string tableName, int id, IDictionary<string, object> values)
        {
            if (!Exists(tableName, id))
                return false;

            var cmd = new StringBuilder();
            cmd.AppendLine($@"UPDATE {tableName}");
            cmd.AppendLine($@"SET {string.Join(",", values.Select(p => $@"{p.Key} = {p.Value.AsSqlString()}"))}");
            cmd.AppendLine($@"WHERE {Item.IdField} = {id}");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }

        private bool DeleteItem(string tableName, int id)
        {
            if (!Exists(tableName, id))
                return false;

            var cmd = new StringBuilder();
            cmd.AppendLine($@"DELETE FROM {tableName}");
            cmd.AppendLine($@"WHERE {Item.IdField} = {id}");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }


        private void CreateDb()
        {
            var cmd = new StringBuilder();
            cmd.AppendLine($@"CREATE TABLE [{GenreTable}] ("                            );
            cmd.AppendLine($@"  [{Genre.IdField}]     INTEGER     NOT NULL UNIQUE,"    );
            cmd.AppendLine($@"  [{Genre.NameField}]   VARCHAR(64) NOT NULL UNIQUE,"    );
            cmd.AppendLine($@"  [{Genre.RatingField}] INTEGER     NOT NULL DEFAULT 0," );
            cmd.AppendLine($@"  [{Genre.TagField}]    VARCHAR(64),"                    );
            cmd.AppendLine($@"  PRIMARY KEY ([{Genre.IdField}]));"                      );

            cmd.AppendLine($@"CREATE TABLE [{ArtistTable}] (");
            cmd.AppendLine($@"  [{Artist.IdField}]            INTEGER     NOT NULL UNIQUE,");
            cmd.AppendLine($@"  [{Artist.NameField}]          VARCHAR(64) NOT NULL UNIQUE,");
            cmd.AppendLine($@"  [{Artist.AlbumsCountField}]   INTEGER     NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Artist.TracksCountField}]   INTEGER     NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Artist.RatingField}]        INTEGER     NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Artist.TagField}]           VARCHAR(64),");
            cmd.AppendLine($@"  PRIMARY KEY  ([{Artist.IdField}]));");

            cmd.AppendLine($@"CREATE TABLE [{AlbumTable}] (");
            cmd.AppendLine($@"  [{Album.IdField}]                INTEGER          NOT NULL UNIQUE,");
            cmd.AppendLine($@"  [{Album.NameField}]             VARCHAR(64)  NOT NULL UNIQUE,");
            cmd.AppendLine($@"  [{Album.ArtistIdField}]          INTEGER          NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Album.YearField}]              INTEGER          NOT NULL DEFAULT 1900,");
            cmd.AppendLine($@"  [{Album.TracksCountField}]        INTEGER          NOT NULL DEFAULT 0, ");
            cmd.AppendLine($@"  [{Album.RatingField}]            INTEGER          NOT NULL DEFAULT 0, ");
            cmd.AppendLine($@"  [{Album.TagField}]              VARCHAR(64), ");
            cmd.AppendLine($@"  PRIMARY KEY ([{Album.IdField}]),");
            cmd.AppendLine($@"  FOREIGN KEY ([{Album.ArtistIdField}]) ");
            cmd.AppendLine($@"    REFERENCES [{ArtistTable}]([{Artist.IdField}]) ON DELETE SET DEFAULT);");

            cmd.AppendLine($@"CREATE TABLE [{TrackTable}] (");
            cmd.AppendLine($@"  [{Track.IdField}]                INTEGER          NOT NULL UNIQUE,");
            cmd.AppendLine($@"  [{Track.NameField}]             VARCHAR(64)  NOT NULL UNIQUE, ");
            cmd.AppendLine($@"  [{Track.ArtistIdField}]          INTEGER          NOT NULL DEFAULT 0, ");
            cmd.AppendLine($@"  [{Track.AlbumIdField}]           INTEGER          NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Track.GenreIdField}]           INTEGER          NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Track.DurationField}]          INTEGER          NOT NULL DEFAULT 0,");
            cmd.AppendLine($@"  [{Track.UriField}]              VARCHAR      NOT NULL UNIQUE,");
            cmd.AppendLine($@"  [{Track.RatingField}]            INTEGER          NOT NULL DEFAULT 0, ");
            cmd.AppendLine($@"  [{Track.TagField}]              VARCHAR(64),");
            cmd.AppendLine($@"  PRIMARY KEY ([{Track.IdField}]),");
            cmd.AppendLine($@"  FOREIGN KEY ([{Track.ArtistIdField}]) ");
            cmd.AppendLine($@"    REFERENCES [{ArtistTable}]([{Artist.IdField}]) ON DELETE SET DEFAULT,");
            cmd.AppendLine($@"  FOREIGN KEY ([{Track.AlbumIdField}])");
            cmd.AppendLine($@"    REFERENCES [{AlbumTable}]([{Album.IdField}]) ON DELETE SET DEFAULT,");
            cmd.AppendLine($@"  FOREIGN KEY ([{Track.GenreIdField}]) ");
            cmd.AppendLine($@"    REFERENCES [{GenreTable}]([{Genre.IdField}]) ON DELETE SET DEFAULT);");

            cmd.AppendLine($@"INSERT INTO [{GenreTable}]  ({Genre.IdField}, {Genre.NameField}, {Genre.TagField}) ");
            cmd.AppendLine($@"    VALUES ({Genre.DefaultId}, 'Unknown genre', 'default');");

            cmd.AppendLine($@"INSERT INTO [{ArtistTable}] ({Artist.IdField}, {Artist.NameField}, {Artist.TagField})");
            cmd.AppendLine($@"    VALUES ({Artist.DefaultId}, 'Unknown artist', 'default');");

            cmd.AppendLine($@"INSERT INTO [{AlbumTable}] ({Album.IdField}, {Album.NameField}, {Album.ArtistIdField}, {Album.TagField}) ");
            cmd.AppendLine($@"    VALUES ({Album.DefaultId}, 'Unknown album', 0, 'default');");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }

        }

        

        #endregion
    }
}
