﻿using System.Diagnostics;
using System.IO;
using NAudio.Wave;
using NetAudioPlayer.Core.Data;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Model
{
    [DebuggerDisplay("{Name}. Played: {IsPlayed}")]
    public class PlaylistItem
    {
        public Track Track { get; }

        public int? HistoryIndex { get; set; }

        public bool IsPlayed => HistoryIndex.HasValue;        

        public PlaylistItem(Track track)
        {
            Track = track;
        }
    }
}