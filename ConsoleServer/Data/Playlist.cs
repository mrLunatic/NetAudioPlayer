using System.Collections.Generic;
using System.Net.Http.Formatting;
using Spartan.Common.Data;

namespace Spartan.ServerNet45.Data
{
    internal class Playlist : IPlaylist
    {
        #region Constants

        public const string IdColumn = @"id";

        public const string NameColumn = @"name";

        public const string RatingColumn = @"rating";

        public const string TagColumn = @"tag";

        public const string Table = @"playlist";

        public const string TracksCountColumn = @"tracks_count";

        public const int DefaultRating = 0;

        public const string DefaultTag = @"";

        #endregion

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Rating { get; set; }

        public string Tag { get; set; }

        public int TracksCount { get; set; }

        #endregion

    }
}
