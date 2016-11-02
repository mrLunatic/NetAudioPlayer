using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Model
{
    
    public interface IPlayListItemLoader
    {
        PlayListItem LoadItem(string item);
    }

    [DebuggerDisplay("{Name}. Played: {IsPlayed}")]
    public class PlayListItem
    {
        public string Name { get; }

        public WaveStream WaveStream { get; }

        public int? HistoryIndex { get; set; }

        public bool IsPlayed => HistoryIndex.HasValue;        

        public PlayListItem(string name, WaveStream stream)
        {
            Name = name;
            WaveStream = stream;
        }
    }

    [DebuggerDisplay("{_items.Count} items. Current: {Item.Name}")]
    public class Playlist
    {
        private readonly IList<PlayListItem> _items = new List<PlayListItem>();

        private readonly Random _random = new Random();

        private readonly IPlayListItemLoader _itemLoader;

        private PlayListItem _item;

        private int _historyCount;


        public IEnumerable<string> Items => this._items.Select(p => p.Name);

        public PlayListItem Item
        {
            get { return _item; }
            private set
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

                OnItemChanged(_item);
            }
        }

        public bool Shuffle { get; set; }

        public RepeatMode RepeatMode { get; set; }


        public event EventHandler<PlayListItem> ItemChanged;


        public Playlist(IPlayListItemLoader itemLoader)
        {
            this._itemLoader = itemLoader;
        }

        public void Init(IEnumerable<string> items)
        {
            this.Reset();

            foreach (var item in items)
            {
                var playListItem = this._itemLoader.LoadItem(item);

                this._items.Add(playListItem);                
            }
        }

        public void Reset()
        {
            Item = null;

            foreach (var playListItem in _items)
            {
                playListItem.WaveStream?.Dispose();
            }

            _items.Clear();            
            _historyCount = 0;
        }

        public bool Play()
        {
            return Next();
        }

        public bool Play(string item)
        {
            var playListItem = _items.FirstOrDefault(p => p.Name == item);

            if (playListItem == null)
            {
                Init(new [] {item} );

                playListItem = _items[0];
            }

            Item = playListItem;

            return Item != null;
        }

        public bool Next()
        {
            var avaliableItems = _items.Where(p => !p.IsPlayed).ToArray();

            if (RepeatMode == RepeatMode.NoRepeat && !avaliableItems.Any())
            {
                Item = null;
                return false;
            }
            if (RepeatMode == RepeatMode.One && Item != null)
            {
                OnItemChanged(Item);
                return true;
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
                return false;
            }

            var index = Shuffle ? _random.Next(avaliableItems.Length) : 0;

            Item = avaliableItems[index];

            return true;
        }

        public bool Prev()
        {
            var played = _items.Where(p => p.IsPlayed).ToArray();

            if (played.Length == 0 && Item == null)
            {
                return false;
            }

            Item.HistoryIndex = null;

            _historyCount--;
            var item = played.FirstOrDefault(p => p.HistoryIndex == _historyCount);

            if (item != null)
            {
                _item = item;

                OnItemChanged(_item);
                return true;
            }

            return false;
        }

        protected virtual void OnItemChanged(PlayListItem e)
        {
            ItemChanged?.Invoke(this, e);
        }

        private PlayListItem LoadItem(string item)
        {
            return new PlayListItem(item, null);
        }
    }
}
