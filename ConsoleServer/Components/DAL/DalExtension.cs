using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;
using Spartan.ServerCore.Components.DAL.RequestParameters;
using Spartan.ServerCore.Components.DAL.UpdateParameters;
using Spartan.ServerNet45.Data;

namespace Spartan.ServerNet45.Components.DAL
{
    internal static class DalExtension
    {
        private static readonly string Alb = SqliteDal.Alb;

        private static readonly string Art = SqliteDal.Art;

        private static readonly string Gen = SqliteDal.Gen;

        private static readonly string Plt = SqliteDal.Plst;

        private static readonly string Trk = SqliteDal.Trk;

        

        private static string AsString(this object obj)
        {
            if (obj == null)
                return null;

            return Convert.ToString(obj);
        }

        private static int AsInt(this object obj, int defaultValue = default(int))
        {
            if (obj == null)
                return defaultValue;

            return Convert.ToInt32(obj);
        }

        public static string AsSqlString(this object obj)
        {
            if (obj is string)
            {
                return $"'{obj}'";
            }

            return obj.ToString();
        }

        public static string ToJoinString<T>(this IEnumerable<T> items, string separator)
        {
            if (items == null)
            {
                return null;
            }

            return string.Join(separator, items);
        }

        public static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> func)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (func == null)
                throw new ArgumentNullException(nameof(func));

            var items = new List<T>();

            while (reader.Read())
            {
                var item = func.Invoke(reader);

                items.Add(item);
            }

            return items;
        } 

        #region RequestParameters

        private static IDictionary<object, string> SortingMap { get; }
            = new Dictionary<object, string>
            {
                { AlbumSorting.Id,              $@"{Alb}.{Album.IdColumn}" },
                { AlbumSorting.Name,            $@"{Alb}.{Album.NameColumn}"},
                { AlbumSorting.Rating,          $@"{Alb}.{Album.RatingColumn}" },
                { AlbumSorting.Tag,             $@"{Alb}.{Album.TagColumn}" },
                { AlbumSorting.ArtistId,        $@"{Art}.{Artist.IdColumn}" },
                { AlbumSorting.ArtistName,      $@"{Art}.{Artist.NameColumn}" },
                { AlbumSorting.GenreId,         $@"{Gen}.{Genre.IdColumn}" },
                { AlbumSorting.GenreName,       $@"{Gen}.{Genre.NameColumn}" },
                { AlbumSorting.Year,            $@"{Alb}.{Album.YearColumn}" },
                { AlbumSorting.TracksCount,     $@"{Alb}.{Album.TracksCountColumn}" },

                { ArtistSorting.Id,             $@"{Art}.{Artist.IdColumn}" },
                { ArtistSorting.Name,           $@"{Art}.{Artist.NameColumn}" },
                { ArtistSorting.Rating,         $@"{Art}.{Artist.RatingColumn}" },
                { ArtistSorting.Tag,            $@"{Art}.{Artist.TagColumn}" },
                { ArtistSorting.AlbumsCount,    $@"{Art}.{Artist.AlbumsCountColumn}" },
                { ArtistSorting.TracksCount,    $@"{Art}.{Artist.TracksCountColumn}" },

                { GenreSorting.Id,              $@"{Gen}.{Genre.IdColumn}" },
                { GenreSorting.Name,            $@"{Gen}.{Genre.NameColumn}" },
                { GenreSorting.Rating,          $@"{Gen}.{Genre.RatingColumn}" },
                { GenreSorting.Tag,             $@"{Gen}.{Genre.TagColumn}" },

                { PlaylistSorting.Id,           $@"{Plt}.{Playlist.IdColumn}" },
                { PlaylistSorting.Name,         $@"{Plt}.{Playlist.NameColumn}" },
                { PlaylistSorting.Rating,       $@"{Plt}.{Playlist.RatingColumn}" },
                { PlaylistSorting.Tag,          $@"{Plt}.{Playlist.TagColumn}" },
                { PlaylistSorting.GenreId,      $@"{Gen}.{Genre.IdColumn}" },
                { PlaylistSorting.GenreName,    $@"{Gen}.{Genre.NameColumn}" },
                { PlaylistSorting.TracksCount,  $@"{Plt}.{Playlist.TracksCountColumn}" },

                { TrackSorting.Id,              $@"{Trk}.{Track.IdColumn}" },
                { TrackSorting.Name,            $@"{Trk}.{Track.NameColumn}" },
                { TrackSorting.Rating,          $@"{Trk}.{Track.RatingColumn}" },
                { TrackSorting.Tag,             $@"{Trk}.{Track.TagColumn}" },
                { TrackSorting.ArtistId,        $@"{Art}.{Artist.IdColumn}" },
                { TrackSorting.ArtistName,      $@"{Art}.{Artist.NameColumn}" },
                { TrackSorting.AlbumId,         $@"{Alb}.{Album.IdColumn}" },
                { TrackSorting.AlbumName,       $@"{Alb}.{Album.NameColumn}" },
                { TrackSorting.AlbumNumber,     $@"{Trk}.{Track.AlbumNumberColumn}" },
                { TrackSorting.Duration,        $@"{Trk}.{Track.DurationColumn}" },
                { TrackSorting.Uri,             $@"{Trk}.{Track.UriColumn}" },
            }; 

        private const string Asc = @"ASC";

        private const string Desc = @"DESC";

        public static IEnumerable<string> GetOrderStrings<T>(this IEnumerable<SortingItem<T>> sorting )
        {
            var orders = new List<string>();

            foreach (var orderBy in sorting)
            {
                string columnName;
                if (SortingMap.TryGetValue(orderBy.Field, out columnName))
                {
                    var asc = orderBy.Descending ? Desc : Asc;

                    orders.Add($@"{columnName} {asc}");
                }
            }

            return orders;
        }

        public static string GetConditions(this AlbumRequestParameters p)
        {
            var where = new List<string>();

            if (p.Ids != null)
                where.Add($@"{Alb}.{Album.IdColumn} IN ({string.Join(",", p.Ids)})");

            if (!string.IsNullOrWhiteSpace(p.Name))
                where.Add($@"{Alb}.{Album.NameColumn} LIKE '{p.Name}%'");

            if (!string.IsNullOrWhiteSpace(p.Tag))
                where.Add($@"{Alb}.{Album.TagColumn} LIKE '%{p.Tag}%'");

            if (p.RatingMin.HasValue)
                where.Add($@"{Alb}.{Album.RatingColumn} >= {p.RatingMin}");

            if (p.RatingMax.HasValue)
                where.Add($@"{Alb}.{Album.RatingColumn} <= {p.RatingMax}");

            if (p.Rating.HasValue)
                where.Add($@"{Alb}.{Album.RatingColumn} = {p.Rating}");

            if (p.ArtistId.HasValue)
                where.Add($@"{Art}.{Artist.IdColumn} = {p.ArtistId}");

            if (string.IsNullOrWhiteSpace(p.ArtistName))
                where.Add($@"{Art}.{Artist.NameColumn} LIKE '{p.ArtistName}%'");

            if (p.YearMin.HasValue)
                where.Add($@"{Alb}.{Album.YearColumn} >= {p.YearMin}");

            if (p.YearMax.HasValue)
                where.Add($@"{Alb}.{Album.YearColumn} <= {p.YearMax}");

            if (p.Year.HasValue)
                where.Add($@"{Alb}.{Album.YearColumn} = {p.Year}");

            return where.Any() ? where.ToJoinString(" AND ") : null;
        }

        public static string GetConditions(this ArtistRequestParameters p)
        {
            var where = new List<string>();

            if (p.Ids != null)
                where.Add($@"{Art}.{Artist.IdColumn} IN ({string.Join(",", p.Ids)})");

            if (!string.IsNullOrWhiteSpace(p.Name))
                where.Add($@"{Art}.{Artist.NameColumn} LIKE '%{p.Name}%'");

            if (!string.IsNullOrWhiteSpace(p.Tag))
                where.Add($@"{Art}.{Artist.TagColumn} LIKE '%{p.Tag}%'");

            if (p.RatingMin.HasValue)
                where.Add($@"{Art}.{Artist.RatingColumn} >= {p.RatingMin}");

            if (p.RatingMax.HasValue)
                where.Add($@"{Art}.{Artist.RatingColumn} <= {p.RatingMax}");

            if (p.Rating.HasValue)
                where.Add($@"{Art}.{Artist.RatingColumn} = {p.Rating}");

            return where.Any() ? where.ToJoinString(" AND ") : null;
        }

        public static string GetConditions(this GenreRequestParameters p)
        {
            var where = new List<string>();

            if (p.Ids != null)
                where.Add($@"{Gen}.{Genre.IdColumn} IN ({string.Join(",", p.Ids)})");

            if (!string.IsNullOrWhiteSpace(p.Name))
                where.Add($@"{Gen}.{Genre.NameColumn} LIKE '{p.Name}%'");

            if (!string.IsNullOrWhiteSpace(p.Tag))
                where.Add($@"{Gen}.{Genre.TagColumn} LIKE '%{p.Tag}%'");

            if (p.RatingMin.HasValue)
                where.Add($@"{Gen}.{Genre.RatingColumn} >= {p.RatingMin}");

            if (p.RatingMax.HasValue)
                where.Add($@"{Gen}.{Genre.RatingColumn} <= {p.RatingMax}");

            if (p.Rating.HasValue)
                where.Add($@"{Gen}.{Genre.RatingColumn} = {p.Rating}");

            return where.Any() ? where.ToJoinString(" AND ") : null;
        }

        public static string GetConditions(this PlaylistRequestParameters p)
        {
            var where = new List<string>();

            if (p.Ids != null)
                where.Add($@"{Plt}.{Playlist.IdColumn} IN ({string.Join(",", p.Ids)})");

            if (!string.IsNullOrWhiteSpace(p.Name))
                where.Add($@"{Plt}.{Playlist.NameColumn} LIKE '{p.Name}%'");

            if (!string.IsNullOrWhiteSpace(p.Tag))
                where.Add($@"{Plt}.{Playlist.TagColumn} LIKE '%{p.Tag}%'");

            if (p.RatingMin.HasValue)
                where.Add($@"{Plt}.{Playlist.RatingColumn} >= {p.RatingMin}");

            if (p.RatingMax.HasValue)
                where.Add($@"{Plt}.{Playlist.RatingColumn} <= {p.RatingMax}");

            if (p.Rating.HasValue)
                where.Add($@"{Plt}.{Playlist.RatingColumn} = {p.Rating}");

            return where.Any() ? where.ToJoinString(" AND ") : null;
        }

        public static string GetConditions(this TrackRequestParameters p)
        {
            var where = new List<string>();

            if (p.Ids != null)
                where.Add($@"{Trk}.{Track.IdColumn} IN ({string.Join(",", p.Ids)})");

            if (!string.IsNullOrWhiteSpace(p.Name))
                where.Add($@"{Trk}.{Track.NameColumn} LIKE '{p.Name}%'");

            if (!string.IsNullOrWhiteSpace(p.Tag))
                where.Add($@"{Trk}.{Track.TagColumn} LIKE '%{p.Tag}%'");

            if (p.RatingMin.HasValue)
                where.Add($@"{Trk}.{Track.RatingColumn} >= {p.RatingMin}");

            if (p.RatingMax.HasValue)
                where.Add($@"{Trk}.{Track.RatingColumn} <= {p.RatingMax}");

            if (p.Rating.HasValue)
                where.Add($@"{Trk}.{Track.RatingColumn} = {p.Rating}");

            if (p.ArtistId.HasValue)
                where.Add($@"{Art}.{Artist.IdColumn} = {p.ArtistId.Value}");

            if (!string.IsNullOrWhiteSpace(p.ArtistName))
                where.Add($@"{Art}.{Artist.NameColumn} LIKE '{p.ArtistName}%'");

            if (p.AlbumId.HasValue)
                where.Add($@"{Alb}.{Album.IdColumn} = {p.AlbumId.Value}");

            if (string.IsNullOrWhiteSpace(p.AlbumName))
                where.Add($@"{Alb}.{Album.NameColumn} LIKE '{p.AlbumName}%'");

            if (p.AlbumNumber.HasValue)
                where.Add($@"{Trk}.{Track.AlbumNumberColumn} = {p.AlbumNumber.Value}");

            if (p.GenreId.HasValue)
                where.Add($@"{Gen}.{Genre.IdColumn} = {p.GenreId}");

            if (string.IsNullOrWhiteSpace(p.GenreName))
                where.Add($@"{Gen}.{Genre.NameColumn} LIKE '{p.GenreName}%'");

            if (string.IsNullOrWhiteSpace(p.Uri))
                where.Add($@"{Trk}.{Track.UriColumn} LIKE '{p.Uri}%'");

            return where.Any() ? where.ToJoinString(" AND ") : null;
        }

        #endregion

        #region Update Parameters

        public static string GetSetString(this AlbumUpdateParameters p)
        {
            var items = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(p.Name))
                items.Add(Album.NameColumn, p.Name.AsSqlString());

            if (p.Rating.HasValue)
                items.Add(Album.RatingColumn, p.Rating.Value.AsSqlString());

            if (!string.IsNullOrEmpty(p.Tag))
                items.Add(Album.TagColumn, p.Tag.AsSqlString());

            if (p.ArtistId.HasValue)
                items.Add(Album.ArtistIdColumn, p.ArtistId.Value.AsSqlString());

            if (p.Year.HasValue)
                items.Add(Album.YearColumn, p.Year.Value.AsSqlString());

            return items
                .Select(item => $@"{item.Key} = {item.Value}")
                .ToJoinString(", ");
        }

        public static string GetSetString(this ArtistUpdateParameters p)
        {
            var items = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(p.Name))
                items.Add(Artist.NameColumn, p.Name.AsSqlString());

            if (p.Rating.HasValue)
                items.Add(Artist.RatingColumn, p.Rating.Value.AsSqlString());

            if (!string.IsNullOrEmpty(p.Tag))
                items.Add(Artist.TagColumn, p.Tag.AsSqlString());

            return string.Join(",", items.Select(item => $@"{item.Key} = {item.Value}"));
        }

        public static string GetSetString(this GenreUpdateParameters p)
        {
            var items = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(p.Name))
                items.Add(Genre.NameColumn, p.Name.AsSqlString());

            if (p.Rating.HasValue)
                items.Add(Genre.RatingColumn, p.Rating.Value.AsSqlString());

            if (!string.IsNullOrEmpty(p.Tag))
                items.Add(Genre.TagColumn, p.Tag.AsSqlString());

            return string.Join(",", items.Select(item => $@"{item.Key} = {item.Value}"));
        }

        public static string GetSetString(this PlaylistUpdateParameters p)
        {
            var items = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(p.Name))
                items.Add(Playlist.NameColumn, p.Name.AsSqlString());

            if (p.Rating.HasValue)
                items.Add(Playlist.RatingColumn, p.Rating.Value.AsSqlString());

            if (!string.IsNullOrEmpty(p.Tag))
                items.Add(Playlist.TagColumn, p.Tag.AsSqlString());

            return string.Join(",", items.Select(item => $@"{item.Key} = {item.Value}"));
        }

        public static string GetSetString(this TrackUpdateParameters p)
        {
            var items = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(p.Name))
                items.Add(Track.NameColumn, p.Name.AsSqlString());

            if (p.Rating.HasValue)
                items.Add(Track.RatingColumn, p.Rating.Value.AsSqlString());

            if (!string.IsNullOrEmpty(p.Tag))
                items.Add(Track.TagColumn, p.Tag.AsSqlString());

            if (p.ArtistId.HasValue)
                items.Add(Track.ArtistIdColumn, p.ArtistId.Value.AsSqlString());

            if (p.AlbumId.HasValue)
                items.Add(Track.AlbumIdColumn, p.AlbumId.Value.AsSqlString());

            if (p.AlbumNumber.HasValue)
                items.Add(Track.AlbumNumberColumn, p.AlbumNumber.Value.AsSqlString());

            if (p.GenreId.HasValue)
                items.Add(Track.GenreIdColumn, p.GenreId.Value.AsSqlString());

            if (p.Duration.HasValue)
                items.Add(Track.DurationColumn, p.Duration.Value.AsSqlString());

            if (p.Uri != null)
                items.Add(Track.UriColumn, p.Uri.AsSqlString());

            return string.Join(",", items.Select(item => $@"{item.Key} = {item.Value}"));
        }

        #endregion
    }
}