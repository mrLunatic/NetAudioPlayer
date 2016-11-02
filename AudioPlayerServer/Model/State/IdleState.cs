using NetAudioPlayer.Core.Message;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{
    internal sealed class IdleState : StateBase
    {
        #region Overrides of PlayerServiceStateBase

        protected override IMessage HandlePlayMessage(PlayMessage message)
        {                         
            var response = SwitchState<PlayState>(message);

            return response;
        }

        #endregion
    }
}
