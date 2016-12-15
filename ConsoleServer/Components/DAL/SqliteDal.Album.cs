using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spartan.Common.Data;
using Spartan.ServerCore.Components.DAL.RequestParameters;
using Spartan.ServerCore.Components.DAL.UpdateParameters;
using Spartan.ServerNet45.Data;

namespace Spartan.ServerNet45.Components.DAL
{
    public sealed partial class SqliteDal
    {
        public int CreateAlbum(string name, string artistName, string genreName, int year)
        {
            var cmd = $@"
INSERT OR IGNORE INTO {Artist.Table} ({Artist.NameColumn}) VALUES ('{artistName}');
INSERT OR IGNORE INTO {Genre.Table } ({Genre.NameColumn }) VALUES ('{genreName }');
INSERT INTO {Album.Table}
(
    {Album.NameColumn},
    {Album.ArtistIdColumn},
    {Album.GenreIdColumn},
    {Album.YearColumn}
)
VALUES
(
    '{name}',
    (SELECT {Artist.IdColumn} FROM {Artist.Table} WHERE {Artist.NameColumn} = '{artistName}]),
    (SELECT {Genre.IdColumn} FROM {Genre.Table} WHERE {Genre.NameColumn} = '{genreName}'),
    {year}
);
SELECT {LastRow};";

            return ExecuteInt(cmd);
        }

        public IAlbum GetAlbum(int id)
        {
            var cmd = $@"
SELECT
    {Alb}.{Album.IdColumn}, 
    {Alb}.{Album.NameColumn}, 
    {Alb}.{Album.RatingColumn}, 
    {Alb}.{Album.TagColumn},
    {Art}.{Artist.IdColumn}, 
    {Alb}.{Artist.NameColumn},
    {Gen}.{Genre.IdColumn}, 
    {Gen}.{Genre.NameColumn},
    {Alb}.{Album.YearColumn}, 
    {Alb}.{Album.TracksCountColumn}
FROM
    {Album.Table  } as {Alb}, 
    {Artist.Table } as {Art}, 
    {Genre.Table  } as {Gen}
WHERE
    {Alb}.{Album.IdColumn  } = {id}, 
    {Art}.{Artist.IdColumn } = {Alb}.{Album.ArtistIdColumn},
    {Gen}.{Genre.IdColumn  } = {Alb}.{Album.GenreIdColumn}
LIMIT 1;";

            return ExecuteReader(
                cmd,
                reader => new Album
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    ArtistId = reader.GetInt32(4),
                    ArtistName = reader.GetString(5),
                    GenreId = reader.GetInt32(6),
                    GenreName = reader.GetString(7),
                    Year = reader.GetInt32(8),
                    TracksCount = reader.GetInt32(9)
                });
        }

        public IEnumerable<IAlbum> GetAlbums(AlbumRequestParameters p)
        {
            var cmd = $@"
SELECT
    {Alb}.{Album.IdColumn},
    {Alb}.{Album.NameColumn},
    {Alb}.{Album.RatingColumn},
    {Alb}.{Album.TagColumn},
    {Art}.{Artist.IdColumn},
    {Art}.{Artist.NameColumn},
    {Gen}.{Genre.IdColumn},
    {Gen}.{Genre.NameColumn},
    {Alb}.{Album.YearColumn},
    {Alb}.{Album.TracksCountColumn}
FROM
    {Album.Table} as {Alb},
    {Artist.Table} as {Art},
    {Genre.Table} as {Gen}
WHERE 
    {Art}.{Artist.IdColumn} = {Alb}.{Album.ArtistIdColumn} AND
    {Gen}.{Genre.IdColumn} = {Alb}.{Album.GenreIdColumn} AND
    {p.GetConditions() ?? RequestParameters.DefaultCondition}
ORDER BY 
    {(p.OrderBy ?? AlbumRequestParameters.DefaultOrderBy).GetOrderStrings().ToJoinString(", ")}
LIMIT 
    {p.Limit ?? RequestParameters.DefaultLimit}
OFFSET 
    {p.Offset ?? RequestParameters.DefaultOffset};";

            return ExecuteReaderList(
                cmd,
                reader => new Album
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    ArtistId = reader.GetInt32(4),
                    ArtistName = reader.GetString(5),
                    GenreId = reader.GetInt32(6),
                    GenreName = reader.GetString(7),
                    Year = reader.GetInt32(8),
                    TracksCount = reader.GetInt32(9)
                });

        }

        public bool UpdateAlbum(int id, AlbumUpdateParameters parameters)
        {
            var setString = parameters.GetSetString();

            if (setString.Length == 0)
                return false;

            var cmd = $@"
UPDATE {Album.Table} 
SET {setString}
WHERE {Album.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        public bool DeleteAlbum(int id)
        {
            var cmd = $@"
DELETE FROM {Album.Table} 
WHERE {Album.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }
    }
}
