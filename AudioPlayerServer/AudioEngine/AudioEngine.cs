using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AudioPlayerServer.Messages;
using NAudio.Wave;

namespace AudioPlayerServer.AudioEngine
{
    class AudioEngine : IAudioEngine, IDisposable
    {            
        private readonly WaveOut _waveOut = new WaveOut();

        private readonly Timer _timer = new Timer(1000);

        private bool _disposed = false;

        private WaveStream _stream;



        public PlayerState State { get; private set; }

        public TimeSpan ItemDuration { get; private set; }

        public TimeSpan Position { get; private set; }

        public string Item { get; private set; }


        public event EventHandler StateChanged;


        public AudioEngine()
        {
            _waveOut.PlaybackStopped += (sender, args) =>
            {
                _stream?.Dispose();
                _stream = null;

                State = PlayerState.Stop;
                ItemDuration = TimeSpan.Zero;
                Position = TimeSpan.Zero;
                Item = string.Empty;

                OnStateChanged();
            };
            _timer.Elapsed += (sender, args) =>
            {
                OnStateChanged();
            };
        }


        public void Play()
        {
            if (State != PlayerState.Pause) return;

            _waveOut.Resume();

            if (!_timer.Enabled)
                _timer.Start();

            State = PlayerState.Play;
            OnStateChanged();


        }

        public void Play(string fileName)
        {
            _stream?.Dispose();            

            _stream = new MediaFoundationReader(fileName);

            _waveOut.Init(_stream);

            _waveOut.Play();

            if (!_timer.Enabled)
                _timer.Start();


        }

        public void Pause()
        {
            if (State != PlayerState.Play) return;

            OnStateChanged();

            _waveOut.Pause();

            if (_timer.Enabled)
                _timer.Stop();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Seek(TimeSpan newPosition)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            if (_disposed) return;

            _waveOut?.Dispose();
            _stream?.Dispose();
        }
    }
}
