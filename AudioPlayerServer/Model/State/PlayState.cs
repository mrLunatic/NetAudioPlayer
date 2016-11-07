using System;
using System.Timers;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{
    internal sealed class PlayState : StateBase
    {
        protected override PlayerState State { get; } = PlayerState.Play;

        private readonly Timer _timer = new Timer(1000);


        public PlayState()
        {
            _timer.Elapsed += TimerOnElapsed;
            AudioEngine.PlaybackStopped += AudioEngineOnPlaybackStopped;
        }

        private void AudioEngineOnPlaybackStopped(object sender, EventArgs eventArgs)
        {
            Playlist.Next();

            if (Playlist.Item != null)
            {
                AudioEngine.Play(Playlist.Item);
            }
            else
            {
                AudioEngine.Stop();
                _timer.Stop();

                SwitchState(States.Stop);
            }
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            SendShortStatusMessage();
        }


        protected override IMessage HandleNextMessage(NextMessage message)
        {
            try
            {
                _timer.Stop();

                Playlist.Next();

                if (Playlist.Item != null)
                {
                    AudioEngine.Play(Playlist.Item);

                    SendStatusMessage();

                    _timer.Start();
                }
                else
                {
                    SwitchState(States.Stop);
                }

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }   
        }

        protected override IMessage HandleSeekMessage(SeekMessage message)
        {
            try
            {
                AudioEngine.Seek(TimeSpan.FromSeconds(message.Position));

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }

        protected override IMessage HandleStatusMessage(StatusMessage message)
        {
            try
            {
                if (message.Repeat.HasValue)
                {
                    Playlist.RepeatMode = message.Repeat.Value;
                }

                if (message.Shuffle.HasValue)
                {
                    Playlist.Shuffle = message.Shuffle.Value;
                }

                if (message.Volume.HasValue)
                {
                    AudioEngine.Volume = (float) message.Volume.Value;
                }

                SendStatusMessage();

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }

        protected override IMessage HandleStopMessage(StopMessage message)
        {
            try
            {
                _timer.Stop();

                SwitchState(States.Stop);

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }

        protected override IMessage HandlePauseMessage(PauseMessage message)
        {
            try
            {
                _timer.Stop();

                SwitchState(States.Pause);

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }

        protected override IMessage HandlePlayMessage(PlayMessage message)
        {
            try
            {
                _timer.Stop();

                Playlist.Init(message.Items);
                Playlist.Play(message.Item);
                AudioEngine.Play(Playlist.Item);

                _timer.Start();

                SendStatusMessage();

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }

        protected override IMessage HandlePrevMessage(PrevMessage message)
        {
            try
            {
                _timer.Stop();

                if (AudioEngine.Position > TimeSpan.FromSeconds(5))
                {
                    Playlist.Prev();
                }

                if (Playlist.Item != null)
                {
                    AudioEngine.Play(Playlist.Item);

                    SendStatusMessage();

                    _timer.Start();
                }
                else
                {
                    SwitchState(States.Stop);
                }
                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }
    }
}
