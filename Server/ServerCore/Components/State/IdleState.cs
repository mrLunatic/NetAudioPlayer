using NetAudioPlayer.Common.Message;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.ServerCore.Components.State
{
    internal sealed class IdleState : StateBase
    {
        protected override PlayerState State { get; } = PlayerState.Idle;

        protected override IMessage HandlePlayMessage(PlayMessage message)
        {                     
            AudioEngine.Init();
                
            var response = SwitchState(States.Play, message);

            return response;
        }

        protected override void OnSwitched(StateBase prevState)
        {
            AudioEngine.Deinit();
            Playlist.Reset();
            
            SendStatusMessage();
                        
        }
    }
}
