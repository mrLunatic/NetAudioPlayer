using System;

namespace NetAudioPlayer.ServerCore.Components.State
{
    internal static class States
    {
        public static IdleState Idle { get; } = Activator.CreateInstance<IdleState>();

        public static PlayState Play { get; } = Activator.CreateInstance<PlayState>();

        public static PauseState Pause { get; } = Activator.CreateInstance<PauseState>();

        public static StopState Stop { get; } = Activator.CreateInstance<StopState>();
    }
}
