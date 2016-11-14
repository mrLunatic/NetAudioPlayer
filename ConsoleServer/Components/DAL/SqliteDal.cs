using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
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

        public int CreateTrack(string name, int artistId, int albumId, int? albumNumber, int genreId, int duration,
            string uri, string tag)
        {
            throw new NotImplementedException();
        }

        public Track GetTrack(int id)
        {
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
            return Create(
                GenreTable, 
                new Dictionary<string, string>
                {
                    { Genre.NameField,  $@"'{name}'" },
                    { Genre.TagField,   $@"'{tag ?? string.Empty}'" }
                });
        }

        public Genre GetGenre(int id)
        {   
            var req = new List<string>
            {
                { Genre.IdField },
                { Genre.NameField},
                { Genre.RatingField},
                { Genre.TagField } 
            };

            var p = GetItem(GenreTable, id, req);

            return new Genre()
            {
                Id = Convert.ToInt32(p[Genre.IdField]),
                Name = Convert.ToString(p[Genre.NameField]),
                Rating = Convert.ToInt32(p[Genre.RatingField]),
                Tag = Convert.ToString(p[Genre.TagField])
            };
        }

        public IEnumerable<Genre> GetGenres(GenreRequestParameters parameters)
        {
            var req = new StringBuilder();
            req.AppendLine($@"SELECT {Genre.IdField}, {Genre.NameField}, {Genre.RatingField}, {Genre.TagField} FROM {GenreTable}");
            req.AppendLine($@"WHERE {GetCommonWhere(parameters)}");

            if (parameters.MaxCount.HasValue)
            {
                req.Append($@"LIMIT {parameters.MaxCount.Value}");
            }

            return ExecuteReader(req.ToString(), GetGenres);
        }
                         
        public bool UpdateGenre(int id, GenreUpdateParameters parameters)
        {
            var items = GetCommonUpdateItems(parameters);

            if (items.Count == 0 || !Exists(GenreTable, id))
            {
                return false;
            }

            var set = string.Join(",", items.Select(p => $@"{p.Key} = {p.Value}"));
            var req = $@"UPDATE {GenreTable} SET {set} WHERE {Genre.IdField} = {id};";

            ExecuteNonQuery(req);

            return true;
        }

        public bool DeleteGenre(int id)
        {
            if (!Exists(GenreTable, id))
            {
                return false;
            }

            var cmd = $@"DELETE FROM {GenreTable} WHERE {Item.IdField} = {id}";

            ExecuteNonQuery(cmd);

            return true;
        }

        #endregion

        #endregion

        #region Private methods
 
        private static Dictionary<string, string> GetCommonUpdateItems(CommonUpdateParameters parameters)
        {
            var items = new Dictionary<string, string>();

            if (parameters.Name != null)
            {
                items.Add(Item.NameField, parameters.Name);
            }

            if (parameters.Rating.HasValue)
            {
                items.Add(Item.RatingField, parameters.Rating.Value.ToString("D"));
            }

            if (parameters.Tag != null)
            {
                items.Add(Item.TagField, parameters.Tag);
            }
            return items;
        }

        private static Genre GetGenre(IDataReader reader)
        {
            if (!reader.Read())
            {
                return null;
            }

            return new Genre()
            {
                Id = Convert.ToInt32(reader[Genre.IdField]),
                Name = Convert.ToString(reader[Genre.NameField]),
                Rating = Convert.ToInt32(reader[Genre.RatingField]),
                Tag = Convert.ToString(reader[Genre.TagField])
            };
        }

        private static IEnumerable<Genre> GetGenres(IDataReader reader)
        {
            var genres = new List<Genre>();
            Genre genre;
            while ((genre = GetGenre(reader) )!= null)
            {
                genres.Add(genre);                
            }
            return genres;
        } 

        private static StringBuilder GetCommonWhere(CommonRequestParameters p)
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

            return where;
        } 

        private int ExecuteNonQuery(string cmd)
        {
            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd;

                connection.Open();
                var result =  command.ExecuteNonQuery();
                connection.Close();

                return result;
            }
        }

        private T ExecuteScalar<T>(string cmd)
        {
            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd;

                connection.Open();
                var result = command.ExecuteScalar(CommandBehavior.SingleResult);

                if (result is long)
                {
                    result = Convert.ToInt32((long) result);
                }

                return (T) result;
            }
        }

        private void ExecuteReader(string cmd, Action<IDataReader> action)
        {
            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd;

                connection.Open();
                var reader = command.ExecuteReader();

                action.Invoke(reader);

                connection.Close();
            }
        }

        private T ExecuteReader<T>(string cmd, Func<IDataReader, T> action)
        {
            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = cmd;

                connection.Open();
                var reader = command.ExecuteReader();

                var result = action.Invoke(reader);

                connection.Close();
                
                return result;
            }
        }

        private bool Exists(string tableName, int id)
        {
            var req = $@"SELECT EXISTS (SELECT {Item.IdField} FROM {tableName} WHERE {Item.IdField} = {id} LIMIT 1);";

            return ExecuteScalar<int>(req) > 0;            
        }

        private int Create(string tableName, IDictionary<string, string> items)
        {
            var cmd = new StringBuilder();
            cmd.AppendLine($@"INSERT INTO {tableName} ({string.Join(",", items.Keys)})");
            cmd.AppendLine($@"VALUES ({string.Join(",", items.Keys)});");
            cmd.AppendLine($@"SELECT last_insert_rowid();");

            return ExecuteScalar<int>(cmd.ToString());
        }

        private IDictionary<string, object> GetItem(string tableName, int id, List<string> fields)
        {
            var req = $@"SELECT {string.Join(",", fields)} FROM {tableName} WHERE {Item.IdField} = {id}";

            using (var connection = new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;"))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = req;

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

        private void CreateDb()
        {
            ExecuteNonQuery($@"
CREATE TABLE [{GenreTable}] (
  [{Genre.IdField}]     INTEGER     NOT NULL UNIQUE, 
  [{Genre.NameField}]   VARCHAR(64) NOT NULL UNIQUE, 
  [{Genre.RatingField}] INTEGER     NOT NULL DEFAULT 0, 
  [{Genre.TagField}]    VARCHAR(64), 
  PRIMARY KEY ([{Genre.IdField}])
);");

            ExecuteNonQuery($@"
CREATE TABLE [{ArtistTable}] (
  [{Artist.IdField}]            INTEGER     NOT NULL UNIQUE, 
  [{Artist.NameField}]          VARCHAR(64) NOT NULL UNIQUE, 
  [{Artist.AlbumsCountField}]   INTEGER     NOT NULL DEFAULT 0, 
  [{Artist.TracksCountField}]   INTEGER     NOT NULL DEFAULT 0, 
  [{Artist.RatingField}]        INTEGER     NOT NULL DEFAULT 0, 
  [{Artist.TagField}]           VARCHAR(64),  
  PRIMARY KEY  ([{Artist.IdField}])
);");

            ExecuteNonQuery($@"
CREATE TABLE [{AlbumTable}] (
  [{Album.IdField}]                INTEGER          NOT NULL UNIQUE, 
  [{Album.NameField}]             VARCHAR(64)  NOT NULL UNIQUE, 
  [{Album.ArtistIdField}]          INTEGER          NOT NULL DEFAULT 0, 
  [{Album.YearField}]              INTEGER          NOT NULL DEFAULT 1900, 
  [{Album.TracksCountField}]        INTEGER          NOT NULL DEFAULT 0, 
  [{Album.RatingField}]            INTEGER          NOT NULL DEFAULT 0, 
  [{Album.TagField}]              VARCHAR(64), 
  PRIMARY KEY ([{Album.IdField}]),
  FOREIGN KEY ([{Album.ArtistIdField}]) REFERENCES [{ArtistTable}]([{Artist.IdField}]) 
             ON DELETE SET DEFAULT
);");

            ExecuteNonQuery($@"
CREATE TABLE [{TrackTable}] (
  [{Track.IdField}]                INTEGER          NOT NULL UNIQUE, 
  [{Track.NameField}]             VARCHAR(64)  NOT NULL UNIQUE, 
  [{Track.ArtistIdField}]          INTEGER          NOT NULL DEFAULT 0, 
  [{Track.AlbumIdField}]           INTEGER          NOT NULL DEFAULT 0,
  [{Track.GenreIdField}]           INTEGER          NOT NULL DEFAULT 0,
  [{Track.DurationField}]          INTEGER          NOT NULL DEFAULT 0,
  [{Track.UriField}]              VARCHAR      NOT NULL UNIQUE,
  [{Track.RatingField}]            INTEGER          NOT NULL DEFAULT 0, 
  [{Track.TagField}]              VARCHAR(64), 
  PRIMARY KEY ([{Track.IdField}]),
  FOREIGN KEY ([{Track.ArtistIdField}]) REFERENCES [{ArtistTable}]([{Artist.IdField}]) ON DELETE SET DEFAULT,
  FOREIGN KEY ([{Track.AlbumIdField}]) REFERENCES [{AlbumTable}]([{Album.IdField}]) ON DELETE SET DEFAULT
  FOREIGN KEY ([{Track.GenreIdField}]) REFERENCES [{GenreTable}]([{Genre.IdField}]) ON DELETE SET DEFAULT
);");
            ExecuteNonQuery($@"
INSERT INTO [{GenreTable}]  ({Genre.IdField}, {Genre.NameField}, {Genre.TagField}) 
    VALUES ({Genre.DefaultId}, 'Unknown genre', 'default');
INSERT INTO [{ArtistTable}] ({Artist.IdField}, {Artist.NameField}, {Artist.TagField}) 
    VALUES ({Artist.DefaultId}, 'Unknown artist', 'default');
INSERT INTO [{AlbumTable}]  ({Album.IdField}, {Album.NameField}, {Album.ArtistIdField}, {Album.TagField}) 
    VALUES ({Album.DefaultId}, 'Unknown album', 0, 'default');
");

        }

        

        #endregion
    }
}
