using Spartan.Common.Message;
using Spartan.Common.Model;
using Spartan.ServerCore.Components.State;

namespace Spartan.ServerCore.Components.Player.State
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
