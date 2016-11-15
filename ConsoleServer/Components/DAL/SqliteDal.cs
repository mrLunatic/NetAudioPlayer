using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using NetAudioPlayer.Core.Components.DAL;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.ConsoleServer.Components.DAL
{
    public sealed class SqliteDal : IDal
    {
        private const string ArtistTable = @"artist";

        private const string AlbumTable = @"album";

        private const string GenreTable = @"genre";

        private const string TrackTable = @"track";


        private readonly string _dbFileName;
        
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
            var p = GetItem(
                TrackTable,
                id,
                new[]
                {
                    Track.NameField,
                    Track.ArtistIdField,
                    Track.AlbumIdField,
                    Track.AlbumNumberField,
                    Track.GenreIdField,
                    Track.DurationField,
                    Track.UriField,
                    Track.TagField
                });

            throw new NotImplementedException();

        }

        public IEnumerable<Track> GetTracks(TrackRequestParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTrack(int id, TrackUpdateParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTrack(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Artist

        public int CreateArtist(string name, string tag)
        {
            throw new NotImplementedException();
        }

        public Artist GetArtist(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artist> GetArtists(ArtistRequestParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtist(int id, ArtistUpdateParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtist(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Album

        public int CreateAlbum(string name, int artistId, int year, string tag)
        {
            throw new NotImplementedException();
        }

        public Album GetAlbum(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetAlbums(AlbumRequestParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAlbum(int id, AlbumUpdateParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAlbum(int id)
        {
            throw new NotImplementedException();
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
            var p = GetItem(
                GenreTable, 
                id,
                new List<string> { Genre.IdField, Genre.NameField, Genre.RatingField, Genre.TagField });

            return new Genre
            {
                Id = Convert.ToInt32(p[Genre.IdField]),
                Name = Convert.ToString(p[Genre.NameField]),
                Rating = Convert.ToInt32(p[Genre.RatingField]),
                Tag = Convert.ToString(p[Genre.TagField])
            };
        }

        public IEnumerable<Genre> GetGenres(GenreRequestParameters parameters)
        {
            var items = GetItems(
                    GenreTable, 
                    new List<string> { Genre.IdField, Genre.NameField, Genre.RatingField, Genre.TagField }, 
                    GetCommonWhere(parameters));

            return items.Select(p => 
                new Genre
                {
                    Id = Convert.ToInt32(p[Genre.IdField]),
                    Name = Convert.ToString(p[Genre.NameField]),
                    Rating = Convert.ToInt32(p[Genre.RatingField]),
                    Tag = Convert.ToString(p[Genre.TagField])
                }); 
        }
                         
        public bool UpdateGenre(int id, GenreUpdateParameters parameters)
        {
            if (!Exists(GenreTable, id))
            {
                return false;
            }

            UpdateItem(
                GenreTable,
                id,
                GetGenreUpdateItems(parameters));

            return true;
        }

        public bool DeleteGenre(int id)
        {
            if (!Exists(GenreTable, id))
            {
                return false;
            }

            DeleteItem(GenreTable, id);

            return true;
        }

        #endregion

        #endregion

        #region Private methods
 
        private static IDictionary<string, object> GetCommonUpdateItems(CommonUpdateParameters parameters)
        {
            var items = new Dictionary<string, object>();

            if (parameters.Name != null)
            {
                items.Add(Item.NameField, parameters.Name);
            }

            if (parameters.Rating.HasValue)
            {
                items.Add(Item.RatingField, parameters.Rating.Value);
            }

            if (parameters.Tag != null)
            {
                items.Add(Item.TagField, parameters.Tag);
            }
            return items;
        }
        private static IDictionary<string, object> GetGenreUpdateItems(GenreUpdateParameters parameters)
        {
            return GetCommonUpdateItems(parameters);
        } 



        private static string GetCommonWhere(CommonRequestParameters p)
        {
            var where = new StringBuilder("1 = 1 ");

            if (p.Ids != null)
            {
                where.Append($@"AND {Item.IdField} IN ({string.Join(",", p.Ids)}) ");
            }

            if (!string.IsNullOrEmpty(p.Name))
            {
                where.Append($@"AND {Item.NameField} LIKE '%{p.Name}%' ");
            }

            if (!string.IsNullOrEmpty(p.Tag))
            {
                where.Append($@"AND {Item.TagField} LIKE '%{p.Tag}%' ");
            }

            if (p.RatingMin.HasValue)
            {
                where.Append($@"AND {Item.RatingField} >= {p.RatingMin} ");
            }

            if (p.RatingMax.HasValue)
            {
                where.Append($@"AND {Item.RatingField} <= {p.RatingMax} ");
            }

            if (p.Rating.HasValue)
            {
                where.Append($@"AND {Item.RatingField} = {p.Rating} ");
            }

            if (p.Offset.HasValue)
            {
                where.Append($@"AND {Item.IdField} > {p.Offset}");
            }

            return where.ToString();
        } 
      


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
            cmd.AppendLine($@"VALUES ({string.Join(",", items.Values.Select(ToSqlString))});");
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

        private IList<IDictionary<string, object>> GetItems(string tableName, List<string> fields, string where)
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

        private void UpdateItem(string tableName, int id, IDictionary<string, object> values)
        {
            var cmd = new StringBuilder();
            cmd.AppendLine($@"UPDATE {tableName}");
            cmd.AppendLine($@"SET {string.Join(",", values.Select(p => $@"{p.Key} = {ToSqlString(p.Value)}"))}");
            cmd.AppendLine($@"WHERE {Item.IdField} = {id}");

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd.ToString();

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void DeleteItem(string tableName, int id)
        {
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
        }


        private string ToSqlString(object obj)
        {
            if (obj is string)
            {
                return $"'{obj}'";
            }

            return obj.ToString();
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
