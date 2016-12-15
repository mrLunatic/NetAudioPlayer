using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        #region Track

        public int CreateTrack(string name, string artistName, string albumName, int albumNumber, string genreName, int duration, string uri)
        {
            var cmd = $@"
INSERT OR IGNORE INTO {Genre.Table} ({Genre.NameColumn}) VALUES ('{genreName}');
INSERT OR IGNORE INTO {Artist.Table} ({Artist.NameColumn}) VALUES ('{artistName}');
INSERT OR IGNORE INTO {Album.Table}
(
    {Album.NameColumn},
    {Album.ArtistIdColumn},
    {Album.GenreIdColumn}
) 
VALUES 
(
    '{albumName}',
    (SELECT {Artist.IdColumn} FROM {Artist.Table} WHERE {Artist.NameColumn} = '{artistName}'),
    (SELECT {Genre.IdColumn} FROM {Genre.Table} WHERE {Genre.NameColumn} = '{genreName}')
);

INSERT INTO {Track.Table} 
(
    {Track.NameColumn},
    {Track.ArtistIdColumn},
    {Track.AlbumIdColumn},
    {Track.AlbumNumberColumn},
    {Track.GenreIdColumn},
    {Track.DurationColumn},
    {Track.UriColumn}
)
VALUES
(
    '{name}',
    (SELECT {Artist.IdColumn} FROM {Artist.Table} WHERE {Artist.NameColumn} = '{artistName}'),
    (SELECT {Album.IdColumn} FROM {Album.Table} WHERE {Album.NameColumn} = '{albumName}'),
    {albumNumber},
    (SELECT {Genre.IdColumn} FROM {Genre.Table} WHERE {Genre.NameColumn} = '{genreName}'),
    {duration},
    '{uri}'
);
SELECT {LastRow};";

            return ExecuteInt(cmd);
        }

        public ITrack GetTrack(int id)
        {
            var cmd =$@"
SELECT 
    {Trk}.{Track.IdColumn},
    {Trk}.{Track.NameColumn},
    {Trk}.{Track.RatingColumn},
    {Trk}.{Track.TagColumn},
    {Art}.{Artist.IdColumn},
    {Art}.{Artist.NameColumn},
    {Alb}.{Album.IdColumn},
    {Alb}.{Album.NameColumn},
    {Gen}.{Genre.IdColumn },
    {Gen}.{Genre.NameColumn},
    {Trk}.{Track.AlbumNumberColumn},
    {Trk}.{Track.DurationColumn},
    {Trk}.{Track.UriColumn} 
FROM
    {Track.Table} as {Trk},
    {Artist.Table} as {Art},
    {Album.Table} as {Alb},
    {Genre.Table} as {Gen}
WHERE
    {Trk}.{Track.IdColumn} = {id} AND
    {Art}.{Artist.IdColumn} = {Trk}.{Track.ArtistIdColumn} AND
    {Alb}.{Album.IdColumn} = {Trk}.{Track.AlbumIdColumn} AND
    {Gen}.{Genre.IdColumn} = {Trk}.{Track.GenreIdColumn}                                    
LIMIT 1;";

            return ExecuteReader(
                cmd,
                reader => new Track
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    ArtistId = reader.GetInt32(4),
                    ArtistName = reader.GetString(5),
                    AlbumId = reader.GetInt32(6),
                    AlbumName = reader.GetString(7),
                    GenreId = reader.GetInt32(8),
                    GenreName = reader.GetString(9),
                    AlbumNumber = reader.GetInt32(10),
                    Duration = reader.GetInt32(11),
                    Uri = reader.GetString(12)                        
                });
        }

        public IEnumerable<ITrack> GetTracks(TrackRequestParameters p)
        {
            var cmd = $@"
SELECT 
    {Trk}.{Track.IdColumn},
    {Trk}.{Track.NameColumn},
    {Trk}.{Track.RatingColumn},
    {Trk}.{Track.TagColumn},
    {Art}.{Artist.IdColumn},
    {Art}.{Artist.NameColumn},
    {Alb}.{Album.IdColumn},
    {Alb}.{Album.NameColumn},
    {Gen}.{Genre.IdColumn },
    {Gen}.{Genre.NameColumn},
    {Trk}.{Track.AlbumNumberColumn},
    {Trk}.{Track.DurationColumn},
    {Trk}.{Track.UriColumn} 
FROM
    {Track.Table} as {Trk},
    {Artist.Table} as {Art},
    {Album.Table} as {Alb},
    {Genre.Table} as {Gen}
WHERE
    {Art}.{Artist.IdColumn} = {Trk}.{Track.ArtistIdColumn} AND
    {Alb}.{Album.IdColumn} = {Trk}.{Track.AlbumIdColumn} AND
    {Gen}.{Genre.IdColumn} = {Trk}.{Track.GenreIdColumn} AND
    {p.GetConditions() ?? RequestParameters.DefaultCondition}
ORDER BY
    {(p.OrderBy ?? TrackRequestParameters.DefaultOrderBy).GetOrderStrings().ToJoinString(", ")}
LIMIT 
    {p.Limit ?? RequestParameters.DefaultLimit}
OFFSET
    {p.Offset ?? RequestParameters.DefaultOffset};";

            return ExecuteReaderList(
                cmd,
                reader => new Track
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    ArtistId = reader.GetInt32(4),
                    ArtistName = reader.GetString(5),
                    AlbumId = reader.GetInt32(6),
                    AlbumName = reader.GetString(7),
                    GenreId = reader.GetInt32(8),
                    GenreName = reader.GetString(9),
                    AlbumNumber = reader.GetInt32(10),
                    Duration = reader.GetInt32(11),
                    Uri = reader.GetString(12)
                });
        }

        public bool UpdateTrack(int id, TrackUpdateParameters parameters)
        {
            var setString = parameters.GetSetString();

            if (string.IsNullOrEmpty(setString))
                return false;

            var changed = ExecuteInt(
                $@"
UPDATE {Track.Table} 
SET ({setString})
WHERE {Track.IdColumn} = {id};
SELECT {Changes}");

            return changed != 0;
        }

        public bool DeleteTrack(int id)
        {
            var deleted = ExecuteInt($@"
DELETE FROM {Track.Table} 
WHERE {Track.IdColumn} = {id};
SELECT {Changes};");

            return deleted != 0;
        }

        #endregion


    }
}
