using System;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.Core.Components.State
{
    internal sealed class PauseState : StateBase
    {
        protected override PlayerState State { get; } = PlayerState.Pause;

        protected override void OnSwitched(StateBase prevState)
        {
            AudioEngine.Pause();
        }

        protected override IMessage HandleNextMessage(NextMessage message)
        {
            return SwitchState(States.Pause, message);
        }

        protected override IMessage HandleSeekMessage(SeekMessage message)
        {
            try
            {
                AudioEngine.Seek(TimeSpan.FromSeconds(message.Position));

                SendShortStatusMessage();

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
                    AudioEngine.Volume = (float)message.Volume.Value;
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
                SwitchState(States.Stop);

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
                return SwitchState(States.Play, message);
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
                return SwitchState(States.Play, message);
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }

        protected override IMessage HandleStatusInfoMessage(StatusInfoMessage message)
        {
            message.CurrentItem = Playlist.Item;
            message.CurrentItemDuration = AudioEngine.Duration.TotalSeconds;
            message.CurrentPosition = AudioEngine.Position.TotalSeconds;
            message.Items = Playlist.Items;
            message.Repeat = Playlist.RepeatMode;
            message.Shuffle = Playlist.Shuffle;
            message.State = PlayerState.Pause;
            message.Volume = AudioEngine.Volume;

            return message;
        }
    }
}
