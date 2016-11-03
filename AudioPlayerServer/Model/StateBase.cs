using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.Model
{
    public abstract class ServerStateBase
    {
        public virtual void OnSwitched() { }

        public abstract Error OnNextMessage(NextMessage msg);

        public abstract Error OnPauseMessage(PauseMessage msg);

        public abstract Error OnPlayMessage(PlayMessage msg);

        public abstract Error OnPrevMessage(PrevMessage msg);

        public abstract Error OnSekMessage(SeekMessage msg);

        public abstract Error OnStatusInfoMessage(StatusInfoMessage msg);

        public abstract Error OnStatusMessage(StatusMessage msg);

        public abstract Error OnStopMessage(StopMessage msg);

    }
}
