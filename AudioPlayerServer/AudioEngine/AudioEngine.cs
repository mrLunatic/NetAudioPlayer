using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Timers;
using NAudio.Wave;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.AudioEngine
{
    

    public class AudioEngine : IAudioEngine, IDisposable
    {
        class TracklistItem
        {
            public const int NonPlayedIndex = -1;

            public string Name { get; set; }

            public bool IsPlayed
            {
                get { return PlayedIndex == NonPlayedIndex; }
                set
                {
                    if (!value)
                    {
                        PlayedIndex = NonPlayedIndex;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public int PlayedIndex { get; set; } = NonPlayedIndex;

            public MediaFoundationReader Stream { get; set; } 
        }

        private readonly WaveOut _waveOut = new WaveOut();
        private readonly Timer _timer = new Timer(1000);
        private readonly IList<TracklistItem> _tracklist = new List<TracklistItem>();
        private readonly Random _random = new Random();
        private int _lastIndex;
        private TracklistItem _item;
        private bool _disposed = false;

        #region Properties

        /// <summary>
        /// Текущее состояние плеера
        /// </summary>
        public PlayerState State { get; private set; }

        /// <summary>
        /// Длительность текущей композиции
        /// </summary>
        public TimeSpan ItemDuration => _item?.Stream.TotalTime ?? TimeSpan.Zero;

        /// <summary>
        /// Текущее положение
        /// </summary>
        public TimeSpan Position => _item?.Stream.CurrentTime ?? TimeSpan.Zero;

        /// <summary>
        /// Текущая композиция
        /// </summary>
        public string Item => _item?.Name;

        /// <summary>
        /// Список композиций
        /// </summary>
        public IEnumerable<string> Items => _tracklist.Select(p => p.Name);

        /// <summary>
        /// Воспроизведение в случайном порядке
        /// </summary>
        public bool Shuffle { get; set; }

        /// <summary>
        /// Режим повторения
        /// </summary>
        public RepeatMode Repeat { get; set; }

        #endregion

        #region Events

        public event EventHandler StateChanged;

        #endregion

        public AudioEngine()
        {
            _waveOut.PlaybackStopped += WaveOutOnPlaybackStopped;
            _timer.Elapsed += TimerOnElapsed;
        }


        /// <summary>
        /// Возобновить воспроизведение
        /// </summary>
        public void Play()
        {
            if (State != PlayerState.Pause)
            {
                return;
            }
            
            _waveOut.Resume();
            _timer.Start();
        }

        /// <summary>
        /// Воспроизвести список композиций, начиная с указанной
        /// </summary>
        /// <param name="items">Список композиций для воспроизведения</param>
        /// <param name="firstItem">Композиция, с которой необходимо начать воспроизведение</param>
        public void Play(IEnumerable<string> items, string firstItem = null)
        {

            _tracklist.Clear();

            if (State != PlayerState.Stop)
            {
                _waveOut.Stop();
                _item?.Stream.Dispose();
            }

            _tracklist.Clear();

            foreach (var item in items)
            {
                _tracklist.Add(
                    new TracklistItem
                    {
                        Name = item,
                        Stream = new MediaFoundationReader(item)
                    });
            }

            if (_tracklist.Count == 0) return;


            _item = _tracklist.FirstOrDefault(p => p.Name == firstItem) ?? _tracklist.FirstOrDefault();

            if (_item == null)
            {
                return;
            }

            _waveOut.Init(_item.Stream);
            _waveOut.Play();

            _timer.Start();
            State = PlayerState.Play;
        }

        /// <summary>
        /// Приостановить воспроизведение
        /// </summary>
        public void Pause()
        {
            if (State == PlayerState.Play)
            {
                _waveOut.Pause();
                State = PlayerState.Pause;
            }
        }

        /// <summary>
        /// Остановить воспроизведение
        /// </summary>
        public void Stop()
        {
            if (State != PlayerState.Stop)
            {
                _timer.Stop();
                _waveOut.Stop();

                State = PlayerState.Stop;
            }
        }

        /// <summary>
        /// Перейти к указанной позиции
        /// </summary>
        /// <param name="newPosition">Новая позиция</param>
        public void Seek(TimeSpan newPosition)
        {
            if (newPosition < _item.Stream.TotalTime)
            {
                _item.Stream.CurrentTime = newPosition;
            }
        }

        /// <summary>
        /// Перейти к следующей композиции
        /// </summary>
        public void Next()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Перейти к началу текущей композиции / предыдущей композиции
        /// </summary>
        public void Prev()
        {
            throw new NotImplementedException();
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
        }

        private void WaveOutOnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
        {
        }


        /// <summary>
        /// Возвращает следующий элемент для воспроизведения
        /// </summary>
        /// <returns></returns>
        private TracklistItem GetNextItem()
        {
            if (Item != null && Repeat == RepeatMode.One)
            {
                return _tracklist
                    .First(p => p.Name == Item);
            }

            if (_tracklist.Count == 0)
            {
                return null;
            }

            var avaliableItems = _tracklist
                .Where(item => !item.IsPlayed)
                .ToList();

            if (avaliableItems.Count == 0 && Repeat == RepeatMode.NoRepeat)
            {
                return null;
            }

            if (avaliableItems.Count == 0)
            {
                _lastIndex = TracklistItem.NonPlayedIndex;

                foreach (var item in _tracklist)
                {
                    item.IsPlayed = false;
                    avaliableItems.Add(item);
                }
            }

            var nextIndex = Shuffle ? _random.Next(avaliableItems.Count) : 0;
            var nextItem = avaliableItems[nextIndex];
            nextItem.PlayedIndex = ++_lastIndex;
            return avaliableItems[nextIndex];
        }

        /// <summary>
        /// Возвращает предыдущий воспроизведенный элемент
        /// </summary>
        /// <returns></returns>
        private TracklistItem GetPrevItem()
        {
            if (_lastIndex == TracklistItem.NonPlayedIndex)
            {
                return null;                
            }

            var lastItem = _tracklist.First(p => p.PlayedIndex == _lastIndex);

            if (lastItem != null)
            {
                lastItem.IsPlayed = false;
                _lastIndex--;
                return lastItem;
            }
            
            return null;
        }
    }
}
