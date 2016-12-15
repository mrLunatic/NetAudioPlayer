using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.ServerNet45.Data
{
    internal class PlaylistMap
    {
        public const string Table = @"playlist_map";

        public const string IdColumn = @"id";

        public const string PlaylistIdColumn = @"playlist_id";

        public const string TrackIdColumn = @"track_id";

        public int Id { get; set; }

        public int PlaylistId { get; set; }

        public int TrackId { get; set; }
    }
}
