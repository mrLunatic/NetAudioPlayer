using System;
using System.Timers;
using NAudio.Wave;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.AudioEngine
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
            this._waveOut.PlaybackStopped += (sender, args) =>
            {
                this._stream?.Dispose();
                this._stream = null;

                State = PlayerState.Stop;
                ItemDuration = TimeSpan.Zero;
                Position = TimeSpan.Zero;
                Item = string.Empty;

                OnStateChanged();
            };
            this._timer.Elapsed += (sender, args) =>
            {
                OnStateChanged();
            };
        }


        public void Play()
        {
            if (State != PlayerState.Pause) return;

            this._waveOut.Resume();

            if (!this._timer.Enabled)
                this._timer.Start();

            State = PlayerState.Play;
            OnStateChanged();


        }

        public void Play(string fileName)
        {
            this._stream?.Dispose();            

            this._stream = new MediaFoundationReader(fileName);

            this._waveOut.Init(this._stream);

            this._waveOut.Play();

            if (!this._timer.Enabled)
                this._timer.Start();


        }

        public void Pause()
        {
            if (State != PlayerState.Play) return;

            OnStateChanged();

            this._waveOut.Pause();

            if (this._timer.Enabled)
                this._timer.Stop();
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
            if (this._disposed) return;

            this._waveOut?.Dispose();
            this._stream?.Dispose();
        }
    }
}
