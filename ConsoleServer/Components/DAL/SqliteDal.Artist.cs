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
        #region Artist

        public int CreateArtist(string name)
        {
            var cmd = $@"
INSERT INTO {Artist.Table}
  ({Artist.NameColumn})
VALUES
  ('{name}');
SELECT {LastRow}";

            return ExecuteInt(cmd);
        }

        public IArtist GetArtist(int id)
        {
            var cmd = $@"
SELECT 
    {Art}.{Artist.IdColumn},
    {Art}.{Artist.NameColumn},
    {Art}.{Artist.RatingColumn},
    {Art}.{Artist.TagColumn},
    {Art}.{Artist.AlbumsCountColumn},
    {Art}.{Artist.TracksCountColumn}
FROM
    {Artist.Table} as {Art}
WHERE
    {Art}.{Artist.IdColumn} = {id}
LIMIT 1;";

            return ExecuteReader(
                cmd,
                reader => new Artist
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    AlbumsCount = reader.GetInt32(4),
                    TracksCount = reader.GetInt32(5)
                });
        }

        public IEnumerable<IArtist> GetArtists(ArtistRequestParameters p)
        {
            var cmd = $@"
SELECT
    {Art}.{Artist.IdColumn},
    {Art}.{Artist.NameColumn},
    {Art}.{Artist.RatingColumn},
    {Art}.{Artist.TagColumn},
    {Art}.{Artist.AlbumsCountColumn},
    {Art}.{Artist.TracksCountColumn}
FROM 
    {Artist.Table} as {Art}
WHERE
    {p.GetConditions() ?? RequestParameters.DefaultCondition}
ORDER BY
    {(p.OrderBy ?? ArtistRequestParameters.DefaultOrderBy).GetOrderStrings().ToJoinString(", ")}
LIMIT
    {p.Limit ?? RequestParameters.DefaultLimit}
OFFSET
    {p.Offset ?? RequestParameters.DefaultOffset};";

            return ExecuteReaderList(                  
                cmd,
                reader => new Artist
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3),
                    AlbumsCount = reader.GetInt32(4),
                    TracksCount = reader.GetInt32(5)
                });
        }

        public bool UpdateArtist(int id, ArtistUpdateParameters parameters)
        {
            var setString = parameters.GetSetString();

            if (setString.Length == 0)
                return false;

            var cmd = $@"
UPDATE {Artist.Table}
SET {setString}
WHERE {Artist.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        public bool DeleteArtist(int id)
        {
            var cmd = $@"
DELETE FROM {Artist.Table}
WHERE {Artist.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        #endregion


    }
}
