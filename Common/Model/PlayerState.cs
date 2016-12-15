namespace Spartan.Common.Model
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
        Stop,
        /// <summary>
        /// Спящий режим
        /// </summary>
        Idle
    }
}
