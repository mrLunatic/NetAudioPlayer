using System.Diagnostics;
using Spartan.Common.Data;

namespace Spartan.ServerCore.Components.Player
{
    [DebuggerDisplay("{Track}. Played: {IsPlayed}")]
    public class PlaylistItem
    {
        public ITrack Track { get; }

        public int? HistoryIndex { get; set; }

        public bool IsPlayed => HistoryIndex.HasValue;        

        public PlaylistItem(ITrack track)
        {
            Track = track;
        }
    }
}