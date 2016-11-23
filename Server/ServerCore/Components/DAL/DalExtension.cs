using System;
using System.Collections.Generic;
using NetAudioPlayer.Common.Data;

namespace NetAudioPlayer.ServerCore.Components.DAL
{
    internal static class DalExtension
    {
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


        private static T GetItem<T>(IDictionary<string, object> p) where T : Item
        {
            var item = Activator.CreateInstance<T>();

            item.Id = p[Track.IdField].AsInt();
            item.Name = p[Track.NameField].AsString();
            item.Rating = p[Track.RatingField].AsInt();
            item.Tag = p[Track.TagField].AsString();

            return item;
        }

        public static Track GetTrack(this IDictionary<string, object> p)
        {
            var track = GetItem<Track>(p);

            track.ArtistId = p[Track.ArtistIdField].AsInt();
            track.ArtistName = p[Track.ArtistNameField].AsString();
            track.AlbumId = p[Track.AlbumIdField].AsInt();
            track.AlbumName = p[Track.AlbumNameField].AsString();
            track.AlbumNumber = p[Track.AlbumNumberField].AsInt();
            track.GenreId = p[Track.GenreIdField].AsInt();
            track.GenreName = p[Track.GenreNameField].AsString();
            track.Duration = p[Track.DurationField].AsInt();
            track.Uri = p[Track.UriField].AsString();

            return track;            
        }

        public static Artist GetArtist(this IDictionary<string, object> p)
        {
            var artist = GetItem<Artist>(p);

            artist.AlbumsCount = p[Artist.AlbumsCountField].AsInt();
            artist.TracksCount = p[Artist.TracksCountField].AsInt();

            return artist;
        }

        public static Album GetAlbum(this IDictionary<string, object> p)
        {
            var album = GetItem<Album>(p);

            album.ArtistId = p[Album.ArtistIdField].AsInt();
            album.ArtistName = p[Album.ArtistNameField].AsString();
            album.Year = p[Album.YearField].AsInt();
            album.TracksCount = p[Album.TracksCountField].AsInt();

            return album;           
        }

        public static Genre GetGenre(this IDictionary<string, object> p)
        {
            var genre = GetItem<Genre>(p);

            return genre;
        }
    }
}