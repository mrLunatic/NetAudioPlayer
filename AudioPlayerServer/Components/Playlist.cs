using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NetAudioPlayer.AudioPlayerServer.Model;
using NetAudioPlayer.AudioPlayerServer.Service;
using NetAudioPlayer.Core.Data;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Components
{
    [DebuggerDisplay("{_items.Count} items. Current: {Item?.Name}")]
    public class Playlist : IPlaylist
    {
        #region Private fields

        private readonly IList<PlaylistItem> _items = new List<PlaylistItem>();

        private readonly Random _random = new Random();

        private PlaylistItem _item;

        private int _historyCount;

        #endregion

        #region Properies

        public IEnumerable<Track> Items => this._items.Select(p => p.Track);

        public Track Item => _item?.Track;

        private PlaylistItem ItemInternal
        {
            get { return _item; }
            set
            {
                if (Equals(_item, value))
                {
                    return;
                }

                this._item = value;
                if (this._item != null)
                {
                    this._item.HistoryIndex = ++_historyCount;
                }
            }
        }

        public bool Shuffle { get; set; }

        public RepeatMode RepeatMode { get; set; }

        #endregion

        #region Public Methods

        public void Init(IEnumerable<Track> items)
        {
            ItemInternal = null;

            _items.Clear();
            _historyCount = 0;

            foreach (var item in items)
            {
                var playListItem = new PlaylistItem(item);

                this._items.Add(playListItem);
            }
        }

        public void Reset()
        {
            ItemInternal = null;

            foreach (var playlistItem in _items)
            {
                playlistItem.HistoryIndex = null;
            }
            _historyCount = 0;
        }

        public void Play()
        {
            Next();
        }

        public void Play(Track track)
        {
            if (track != null)
            {
                var playListItem = _items.FirstOrDefault(p => p.Track.Id == track.Id);

                if (playListItem == null)
                {
                    Init(new[] {track});

                    playListItem = _items[0];
                }

                ItemInternal = playListItem;
            }
            else
            {
                Next();
            }
        }

        public void Next()
        {
            var avaliableItems = _items.Where(p => !p.IsPlayed).ToArray();

            if (RepeatMode == RepeatMode.NoRepeat && !avaliableItems.Any())
            {
                ItemInternal = null;
                return;
            }
            if (RepeatMode == RepeatMode.One && ItemInternal != null)
            {
                return;
            }
            if (RepeatMode == RepeatMode.All && avaliableItems.Length == 0)
            {
                foreach (var playListItem in _items)
                {
                    _historyCount = 0;
                    playListItem.HistoryIndex = null;
                }

                avaliableItems = _items.ToArray();
            }

            if (avaliableItems.Length == 0)
            {
                return;
            }

            var index = Shuffle ? _random.Next(avaliableItems.Length) : 0;

            ItemInternal = avaliableItems[index];
        }

        public void Prev()
        {
            var played = _items.Where(p => p.IsPlayed).ToArray();

            if (played.Length == 0 && ItemInternal == null)
            {
                return;
            }

            ItemInternal.HistoryIndex = null;

            _historyCount--;
            _item = played.FirstOrDefault(p => p.HistoryIndex == _historyCount);
        }

        #endregion
    }
}
