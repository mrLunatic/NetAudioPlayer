using System;
using System.Runtime.Remoting;
using System.Timers;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{
    internal sealed class PlayState : StateBase
    {
        private readonly Timer _timer = new Timer(250);

        protected override IMessage HandleNextMessage(NextMessage message)
        {
            WaveOut.Stop();
            _timer.Stop();

            Playlist.Next();

            if (Playlist.Item != null)
            {
                WaveOut.Init(Playlist.Item.WaveStream);
                WaveOut.Play();
                _timer.Start();
            }
             
            return new ResponseMessage();
        }

        protected override IMessage HandleSeekMessage(SeekMessage message)
        {
            if (message.Position > Playlist.Item.WaveStream.TotalTime)
            {
                return new ResponseMessage(ErrorCode.Unhandled);
            }

            Playlist.Item.WaveStream.CurrentTime = message.Position;

            return new ResponseMessage();
        }

        protected override IMessage HandleStatusMessage(StatusMessage message)
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
                if (message.Volume.Value > 1)
                {
                    message.Volume = 1;
                }
                else if (message.Volume.Value < 0)
                {
                    message.Volume = 0;
                }

                WaveOut.Volume = (float) message.Volume.Value;
            }

            return new ResponseMessage();
        }

        protected override IMessage HandleStopMessage(StopMessage message)
        {
            WaveOut.Stop();
            _timer.Stop();

            SwitchState<StopState>();

            return new ResponseMessage();
        }

        protected override IMessage HandlePauseMessage(PauseMessage message)
        {
            WaveOut.Pause();
            _timer.Stop();

            SwitchState<PauseState>();

            return new ResponseMessage();
        }

        protected override IMessage HandlePlayMessage(PlayMessage message)
        {
            return base.HandlePlayMessage(message);
        }

        protected override IMessage HandlePrevMessage(PrevMessage message)
        {
            return base.HandlePrevMessage(message);

        }

        protected override IMessage HandleStatusInfoMessage(StatusInfoMessage message)
        {
            return base.HandleStatusInfoMessage(message);
        }
    }
}
