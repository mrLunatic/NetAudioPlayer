using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.ServerNet45.Data
{
    [DebuggerDisplay("Playlist map {Id}: {PlaylistId} -> {TrackId}")]
    internal class PlaylistMap
    {
        public const string Table = @"playlist_map";

        public static readonly string InsertTrigger = $@"{Table}_insert_trigger";

        public static readonly string UpdatePlaylistIdTrigger = $@"{Table}_{PlaylistIdColumn}_update_trigger";

        public static readonly string DeleteTrigger = $@"{Table}_delete_trigger";

        public static readonly string PlaylistIdIndex = $@"{Table}_{PlaylistIdColumn}_index";

        public const string IdColumn = @"id";

        public const string PlaylistIdColumn = @"playlist_id";

        public const string TrackIdColumn = @"track_id";

        public int Id { get; set; }

        public int PlaylistId { get; set; }

        public int TrackId { get; set; }
    }
}
