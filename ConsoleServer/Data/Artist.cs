using System.Collections.Generic;
using Spartan.Common.Data;

namespace Spartan.ServerNet45.Data
{
    internal class Artist : IArtist
    {
        #region Constants

        public const string IdColumn = @"id";

        public const string NameColumn = @"name";

        public const string RatingColumn = @"rating";

        public const string TagColumn = @"tag";

        public const string Table = @"artist";

        public const string AlbumsCountColumn = @"albums_count";

        public const string TracksCountColumn = @"tracks_count";

        public const int DefaultRating = 0;

        public const int DefaultId = 1;

        public const string DefaultName = @"Unknown artist";

        public const string DefaultTag = @"";

        #endregion
 
        #region Properties

        public int AlbumsCount { get; set; }

        public int TracksCount { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Rating { get; set; }

        public string Tag { get; set; }

        #endregion
    }
}
