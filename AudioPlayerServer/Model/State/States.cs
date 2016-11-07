using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{
    internal static class States
    {
        public static IdleState Idle { get; } = Activator.CreateInstance<IdleState>();

        public static PlayState Play { get; } = Activator.CreateInstance<PlayState>();

        public static PauseState Pause { get; } = Activator.CreateInstance<PauseState>();

        public static StopState Stop { get; } = Activator.CreateInstance<StopState>();
    }
}
