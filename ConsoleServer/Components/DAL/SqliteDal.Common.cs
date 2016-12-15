using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spartan.ServerCore.Components.DAL;
using Spartan.ServerNet45.Data;

namespace Spartan.ServerNet45.Components.DAL
{
    public sealed partial class SqliteDal : IDal
    {
        public const string Alb = @"alb";

        public const string Art = @"art";

        public const string Gen = @"gen";

        public const string Trk = @"trk";

        public const string Plst = @"plst";

        public const string Plstm = @"plstm";

        private const string LastRow = @"last_insert_rowid()";

        private const string Changes = @"changes()";

        private const string New = @"NEW";

        private const string Old = @"OLD";

        #region Fields

        private readonly string _dbFileName;

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

        private SQLiteConnection OpenConnection()
        {
            return new SQLiteConnection($@"Data Source={_dbFileName}; Version=3;");
        }

        private void ExecuteScalar(string sql)
        {
            using (var connection = OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private int ExecuteInt(string sql)
        {
            object result;

            using (var connection = OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                connection.Open();

                result = command.ExecuteScalar();

                connection.Close();
            }

            return Convert.ToInt32(result);
        }

        private T ExecuteReader<T>(string sql, Func<IDataReader, T> func)
        {
            T result;

            using (var connection = OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    result = func.Invoke(reader);
                }

                connection.Close();
            }

            return result;
        }

        private IEnumerable<T> ExecuteReaderList<T>(string sql, Func<IDataReader, T> func)
        {
            var items = new List<T>();

            using (var connection = OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = func.Invoke(reader);

                        items.Add(item);
                    }
                }

                connection.Close();
            }

            return items;
        }
    }
}
