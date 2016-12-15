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
        #region Genre

        public int CreateGenre(string name)
        {
            var cmd = $@"
INSERT INTO {Genre.Table}
    ({Genre.NameColumn})
VALUES
    ('{name}');
SELECT {LastRow}";

            return ExecuteInt(cmd);
        }

        public IGenre GetGenre(int id)
        {
            var cmd = $@"
SELECT
    {Gen}.{Genre.IdColumn},
    {Gen}.{Genre.NameColumn},
    {Gen}.{Genre.RatingColumn},
    {Gen}.{Genre.TagColumn}
FROM
    {Genre.Table} as {Gen}
WHERE
    {Gen}.{Genre.IdColumn} = {id}
LIMIT 1;";

            return ExecuteReader(
                cmd,
                reader => new Genre
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3)
                });

        }

        public IEnumerable<IGenre> GetGenres(GenreRequestParameters p)
        {
            var cmd = $@"
SELECT
    {Gen}.{Genre.IdColumn},
    {Gen}.{Genre.NameColumn},
    {Gen}.{Genre.RatingColumn},
    {Gen}.{Genre.TagColumn}
FROM
    {Genre.Table} as {Gen}
WHERE
    {p.GetConditions() ?? RequestParameters.DefaultCondition}
ORDER BY
    {(p.OrderBy ?? GenreRequestParameters.DefaultOrderBy).GetOrderStrings().ToJoinString(", ")}
LIMIT
    {p.Limit ?? RequestParameters.DefaultLimit}
OFFSET
    {p.Offset ?? RequestParameters.DefaultOffset};";

            return ExecuteReaderList(
                cmd,
                reader => new Genre
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Rating = reader.GetInt32(2),
                    Tag = reader.GetString(3)
                });

        }

        public bool UpdateGenre(int id, GenreUpdateParameters parameters)
        {
            var setString = parameters.GetSetString();

            if (setString.Length == 0)
                return false;

            var cmd = $@"
UPDATE {Genre.Table}
SET
    {setString}
WHERE {Genre.IdColumn} = {id};
SELECT {Changes}";

            return ExecuteInt(cmd) != 0;
        }

        public bool DeleteGenre(int id)
        {
            var cmd = $@"
DELETE FROM {Genre.Table}
WHERE {Genre.IdColumn} = {id};
SELECT {Changes};";

            return ExecuteInt(cmd) != 0;
        }

        #endregion
    }
}
