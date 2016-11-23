using System;
using NetAudioPlayer.Common.Data;

namespace NetAudioPlayer.ServerCore.Components.Player
{
    /// <summary>
    /// Компонент, отвечающий за работу с аудио
    /// </summary>
    public interface IAudioEngine : IDisposable
    {
        /// <summary>
        /// Длина композиции
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Текущая позиция воспроизведения
        /// </summary>
        TimeSpan Position { get; }

        /// <summary>
        /// Уровень громкости в диапазоне [0.00 - 1.00]
        /// </summary>
        double Volume { get; set; }

        /// <summary>
        /// Событие, происходящее при остановке воспроизведения
        /// </summary>
        event EventHandler PlaybackStopped; 

        /// <summary>
        /// Инициализирует аудиоустроства
        /// </summary>
        void Init();

        /// <summary>
        /// Деинициализирует аудиоустройства
        /// </summary>
        void Deinit();

        /// <summary>
        /// Воспроизвести элемент
        /// </summary>
        /// <param name="track">Элемент для воспроизведения</param>
        void Play(Track track);

        /// <summary>
        /// Приостановить воспроизведение
        /// </summary>
        void Pause();

        /// <summary>
        /// Возобновляет воспроизведение
        /// </summary>
        void Resume();

        /// <summary>
        /// Остановить воспроизведение
        /// </summary>
        void Stop();

        /// <summary>
        /// Перейти к указанному временному промежутку
        /// </summary>
        /// <param name="position">Временной промежуток</param>
        void Seek(TimeSpan position);
    }
}
