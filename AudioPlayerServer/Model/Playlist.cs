using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this._items.Clear();
            this.Item = null;
        }

        public void Play(string item)
        {
            var playListItem = this._items.FirstOrDefault(p => p.Name == item);

            if (playListItem == null)
            {
                Init(new [] {item} );

                playListItem = this._items[0];
            }

            this.Item = playListItem;
        }

        public void Next()
        {
            var avaliableItems = this._items.Where(p => !p.IsPlayed).ToArray();

            if (RepeatMode == RepeatMode.NoRepeat && !avaliableItems.Any())
            {
                this.Item = null;
                return;
            }
            if (RepeatMode == RepeatMode.One && this.Item != null)
            {
                OnItemChanged(Item);
                return;
            }
            if (this.RepeatMode == RepeatMode.All && avaliableItems.Length == 0)
            {
                foreach (PlayListItem playListItem in this._items)
                {
                    _historyCount = 0;
                    playListItem.HistoryIndex = null;
                }

                avaliableItems = this._items.ToArray();
            }

            if (avaliableItems.Length == 0)
            {
                return;
            }

            var index = this.Shuffle ? this._random.Next(avaliableItems.Length) : 0;

            this.Item = avaliableItems[index];
       
        }

        public void Prev()
        {
            var played = this._items.Where(p => p.IsPlayed).ToArray();

            if (played.Length == 0 && this.Item == null)
            {
                return;
            }

            this.Item.HistoryIndex = null;

            var item = played.FirstOrDefault(p => p.HistoryIndex == this._historyCount);

            this._historyCount--;

            if (item != null)
            {
                this.Item = item;
            }

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
