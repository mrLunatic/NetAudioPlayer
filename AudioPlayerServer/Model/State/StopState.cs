using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{
    internal sealed class StopState : StateBase
    {
        protected override PlayerState State { get; } = PlayerState.Stop;

        protected override void OnSwitched(StateBase prevState)
        {
            AudioEngine.Stop();
            Playlist.Reset();
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

        protected override IMessage HandleSwitchOffMessage(SwitchOffMessage message)
        {
            try
            {
                SwitchState(States.Idle);

                return null;
            }
            catch (Exception e)
            {
                return new ResponseMessage(message, e);
            }
        }
    }
}
