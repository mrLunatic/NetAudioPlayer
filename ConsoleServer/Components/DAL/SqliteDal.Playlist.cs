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
        public int CreatePlaylist(string name)
        {
            var cmd = $@"
INSERT INTO {Playlist.NameColumn} ({Playlist.NameColumn})
VALUES ('{name}');
SELECT {LastRow};";

            return ExecuteInt(cmd);
        }

        public IPlaylist GetPlaylist(int id)
        {
            var cmd = $@"
SELECT 
    {Plst}.{Playlist.IdColumn},
    {Plst}.{Playlist.NameColumn},
    {Plst}.{Playlist.RatingColumn},
    {Plst}.{Playlist.TagColumn},
    {Plst}.{Playlist.TracksCountColumn}
FROM
    {Playlist.Table} as {Plst}
WHERE
    {Plst}.{Playlist.IdColumn} = {id}
LIMIT 1;";

            return ExecuteReader(
                cmd,
                reader => new Playlist
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    TracksCount = reader.GetInt32(4)
                });
        }

        public IEnumerable<IPlaylist> GetPlaylists(PlaylistRequestParameters p)
        {
            var cmd = $@"
SELECT 
    {Plst}.{Playlist.IdColumn},
    {Plst}.{Playlist.NameColumn},
    {Plst}.{Playlist.RatingColumn},
    {Plst}.{Playlist.TagColumn},
    {Plst}.{Playlist.TracksCountColumn}
FROM
    {Playlist.Table} as {Plst}
WHERE
    {p.GetConditions() ?? RequestParameters.DefaultCondition }
ORDER BY
    {(p.OrderBy ?? PlaylistRequestParameters.DefaultOrderBy).GetOrderStrings().ToJoinString(", ")}
LIMIT
    {p.Limit ?? RequestParameters.DefaultLimit}
OFFSET
    {p.Offset ?? RequestParameters.DefaultOffset}";

            return ExecuteReaderList(
                cmd,
                reader => new Playlist
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    TracksCount = reader.GetInt32(4)
                });
        }

        public bool UpdatePlaylist(int id, PlaylistUpdateParameters parameters)
        {
            var setString = parameters.GetSetString();

            if (setString.Length == 0)
                return false;

            var cmd = $@"
UPDATE {Playlist.Table}
SET {setString}
WHERE {Playlist.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        public bool DeletePlaylist(int id)
        {
            var cmd = $@"
DELETE FROM {Playlist.Table}
WHERE {Playlist.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        public IEnumerable<int> GetPlaylistTrackIds(int id)
        {
            var cmd = $@"
SELECT {Plstm}.{PlaylistMap.TrackIdColumn}
FROM {PlaylistMap.Table} as {Plstm}
WHERE {Plstm}.{PlaylistMap.PlaylistIdColumn} = {id}";

            return ExecuteReaderList(
                cmd,
                reader => reader.GetInt32(0));
        }

        public IEnumerable<ITrack> GetPlaylistTracks(int id)
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
    {Trk}.{Track.AlbumNumberColumn},
    {Gen}.{Genre.IdColumn},
    {Gen}.{Genre.NameColumn},
    {Trk}.{Track.DurationColumn},
    {Trk}.{Track.UriColumn}
FROM
    {PlaylistMap.Table} as {Plstm},
    {Track.Table} as {Trk},
    {Artist.Table} as {Art},
    {Album.Table} as {Alb},
    {Genre.Table} as {Gen}
WHERE
    {Plstm}.{PlaylistMap.PlaylistIdColumn} = {id} AND
    {Trk}.{Track.IdColumn} = {Plstm}.{PlaylistMap.TrackIdColumn} AND
    {Art}.{Artist.IdColumn} = {Trk}.{Track.ArtistIdColumn} AND
    {Alb}.{Album.IdColumn} = {Trk}.{Track.AlbumIdColumn} AND
    {Gen}.{Genre.IdColumn} = {Trk}.{Track.GenreIdColumn};";

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
                    AlbumNumber = reader.GetInt32(8),
                    GenreId = reader.GetInt32(9),
                    GenreName = reader.GetString(10),
                    Duration = reader.GetInt32(11),
                    Uri = reader.GetString(12)
                });
        }

        public bool AddTrackToPlaylist(int id, int trackId)
        {
            var cmd = $@"
INSERT OR IGNORE INTO {PlaylistMap.Table}
    ({PlaylistMap.PlaylistIdColumn}, {PlaylistMap.TrackIdColumn})
VALUES
    ({id}, {trackId});
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        public bool RemoveTrackFromPlaylist(int id, int trackId)
        {
            var cmd = $@"
DELETE FROM {PlaylistMap.Table}
WHERE
    {PlaylistMap.PlaylistIdColumn} = {id} AND
    {PlaylistMap.TrackIdColumn} = {trackId};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }
    }
}
