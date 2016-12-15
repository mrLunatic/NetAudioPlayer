using System.Collections.Generic;
using Spartan.Common.Data;

namespace Spartan.ServerNet45.Data
{
    internal class Album : IAlbum
    {
        #region Constants

        public const string Table = @"album";

        public const string IdColumn = @"id";

        public const string NameColumn = @"name";

        public const string RatingColumn = @"rating";

        public const string TagColumn = @"tag";

        public const string ArtistIdColumn = @"artist_id";

        public const string GenreIdColumn = @"genre_id";

        public const string YearColumn = @"year";

        public const string TracksCountColumn = @"tracks_count";

        public const int DefaultRating = 0;

        public const int DefaultYear = 1900;

        public const int DefaultId = 1;

        public const string DefaultTag = @"";

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Rating { get; set; }

        public string Tag { get; set; }

        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public int Year { get; set; }

        public int TracksCount { get; set; }

        #endregion
    }
}
