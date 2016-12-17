using System.Collections.Generic;
using System.Diagnostics;
using Spartan.Common.Data;

namespace Spartan.ServerNet45.Data
{
    [DebuggerDisplay("Genre {Id}: {Name}")]
    /// <summary>
    /// Жанр
    /// </summary>
    internal class Genre : IGenre
    {
        #region Constants

        public const string IdColumn = @"id";

        public const string NameColumn = @"name";

        public const string RatingColumn = @"rating";

        public const string TagColumn = @"tag";

        public const string Table = @"genre";

        public static readonly string NameIndex = $@"{Table}_name_index";

        public const int DefaultRating = 0;

        public const int DefaultId = 1;

        public const string DefaultName = @"Unknown genre";

        public const string DefaultTag = @"";

        #endregion

        #region Implementation of IItem

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Rating { get; set; }

        public string Tag { get; set; }

        #endregion
    }
}
