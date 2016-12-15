using System.Collections.Generic;
using Spartan.Common.Data;

namespace Spartan.ServerNet45.Data
{
    /// <summary>
    /// Композиция
    /// </summary>
    internal class Track : ITrack
    {
        #region Constants

        public const string Table = @"track";

        public const string IdColumn = @"id";

        public const string NameColumn = @"name";

        public const string RatingColumn = @"rating";

        public const string TagColumn = @"tag";

        public const string DurationColumn = @"duration";

        public const string ArtistIdColumn = @"aristId";

        public const string AlbumIdColumn = @"albumId";

        public const string AlbumNumberColumn = @"albumNumber";

        public const string GenreIdColumn = @"genreId";

        public const string UriColumn = @"uriField";

        public const int DefaultRating = 0;

        public const int DefaultAlbumNumber = 0;

        public const int DefaultDuration = 0;

        public const string DefaultTag = @"";

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Rating { get; set; }

        public string Tag { get; set; }

        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        public int AlbumId { get; set; }

        public int AlbumNumber { get; set; }

        public string AlbumName { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public int Duration { get; set; }

        public string Uri { get; set; }

        #endregion
    }
}
