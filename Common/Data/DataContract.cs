using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.Common.Data
{
    public static class DataContract
    {
        #region Item

        public const string ItemIdField = @"id";

        public const string ItemNameField = @"name";

        public const string ItemRatingField = @"rating";

        public const string ItemTagField = @"tag";

        #endregion

        #region Album

        public const string AlbumArtistIdField = @"artistId";

        public const string AlbumArtistNameField = @"artistName";

        public const string AlbumYearField = @"year";

        public const string AlbumGenreIdField = @"genreId";

        public const string AlbumGenreNameField = @"genreName";

        public const string AlbumTrackCountField = @"trackCount";

        #endregion

        #region Artist

        public const string ArtistAlbumsCountField = @"albumsCount";

        #endregion

        #region Track

        public const string TrackDurationField = @"duration";

        public const string TrackArtistIdField = @"aristId";

        public const string TrackArtistNameField = @"artistName";

        public const string TrackAlbumIdField = @"albumId";

        public const string TrackAlbumNumberField = @"albumNumber";

        public const string TrackAlbumNameField = @"albumName";

        public const string TrackGenreIdField = @"genreId";

        public const string TrackGenreNameField = @"genreName";

        public const string TrackUriField = @"uriField";

        #endregion

        #region TrackStore

        public const string TrackStoreTracksCountField = @"tracksCount";

        #endregion


    }
}
