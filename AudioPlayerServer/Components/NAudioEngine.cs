using System;
using System.Threading;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Utils;
using NAudio.Wave;
using NetAudioPlayer.Core.Model.Json;

namespace NetAudioPlayer.AudioPlayerServer.Components
{
    public sealed class NAudioEngine : IAudioEngine
    {
        private WaveStream _stream;

        private bool _disposed;

        private bool _stopInCode;
        private WasapiOut _waveOut;

        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                if (WaveOut != null && WaveOut.PlaybackState != PlaybackState.Stopped)
                {
                    _stopInCode = true;
                    WaveOut.Stop();
                    _stopInCode = false;
                }

                WaveOut?.Dispose();

                Stream?.Dispose();
            }
        }

        #endregion

        #region Implementation of IAudioEngine

        /// <summary>
        /// Длина композиции
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Текущая позиция воспроизведения
        /// </summary>
        public TimeSpan Position => WaveOut?.GetPositionTimeSpan() ?? TimeSpan.Zero;

        /// <summary>
        /// Уровень громкости в диапазоне [0.00 - 1.00]
        /// </summary>
        public double Volume
        {
            get
            {
                return WaveOut?.Volume ?? 0.00;
            }
            set
            {
                if (WaveOut == null)
                {
                    return;
                }

                if (value > 1.00)
                {
                    value = 1.00;
                }
                else if (value < 0.00)
                {
                    value = 0.00;
                }

                WaveOut.Volume = (float)value;
            }
        }

        public WaveStream Stream
        {
            get { return _stream; }
            set
            {
                _stream = value;
                Duration = _stream?.TotalTime ?? TimeSpan.Zero;
            }
        }

        public WasapiOut WaveOut
        {
            get { return _waveOut; }
            set
            {
                if (_waveOut != null)
                {
                    _waveOut.PlaybackStopped -= WaveOutOnPlaybackStopped;
                }

                _waveOut = value;

                if (_waveOut != null)
                {
                    _waveOut.PlaybackStopped += WaveOutOnPlaybackStopped;
                }
            }
        }

        public event EventHandler PlaybackStopped;

        /// <summary>
        /// Инициализирует аудиоустроства
        /// </summary>
        public void Init()
        {
                    
        }



        /// <summary>
        /// Деинициализирует аудиоустройства
        /// </summary>
        public void Deinit()
        {
            if (WaveOut != null)
            {
                WaveOut.Dispose();
                WaveOut = null;

                Stream?.Dispose();
                Stream = null;

            }
        }

        /// <summary>
        /// Воспроизвести элемент
        /// </summary>
        /// <param name="track">Элемент для воспроизведения</param>
        public void Play(Track track)
        {
            if (WaveOut != null && WaveOut.PlaybackState != PlaybackState.Stopped)
            {
                _stopInCode = true;
                WaveOut.Stop();
                WaveOut.Dispose();
                WaveOut = null;
                _stopInCode = false;
            }

            Stream = new MediaFoundationReader(
                track.Uri, 
                new MediaFoundationReader.MediaFoundationReaderSettings()
                {
                    RepositionInRead = true,
                    SingleReaderObject = true,
                    RequestFloatOutput = true
                });

            track.Duration = Stream.TotalTime.TotalSeconds;

            WaveOut = new WasapiOut(AudioClientShareMode.Shared, 0);
            WaveOut.Init(Stream);
            WaveOut.Play();

        }

        /// <summary>
        /// Приостановить воспроизведение
        /// </summary>
        public void Pause()
        {
            if (WaveOut.PlaybackState != PlaybackState.Playing)
            {
                return;
            }

            WaveOut.Pause();
        }

        public void Resume()
        {

            if (WaveOut.PlaybackState != PlaybackState.Paused)
            {
                return;
            }

            WaveOut.Play();
        }

        /// <summary>
        /// Остановить воспроизведение
        /// </summary>
        public void Stop()
        {
            if (WaveOut.PlaybackState != PlaybackState.Stopped)
            {
                _stopInCode = true;
                WaveOut.Stop();
                _stopInCode = false;
            }
        }

        /// <summary>
        /// Перейти к указанному временному промежутку
        /// </summary>
        /// <param name="position">Временной промежуток</param>
        public void Seek(TimeSpan position)
        {
            if (Stream == null || position > Stream.TotalTime)
            {
                return;
            }

            Stream.CurrentTime = position;
        }

        #endregion

        private void WaveOutOnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            if (!_stopInCode)
            {
                OnPlaybackStopped();
            }
        }

        private void OnPlaybackStopped()
        {
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
        }
    }
}
