using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAudioPlayer.Core.Model
{
    /// <summary>
    /// Состояния плеера
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// Воспроизведение
        /// </summary>
        Play,
        /// <summary>
        /// Воспроизведение приостановлено
        /// </summary>
        Pause,
        /// <summary>
        /// Плеер остановлен
        /// </summary>
        Stop
    }
}
