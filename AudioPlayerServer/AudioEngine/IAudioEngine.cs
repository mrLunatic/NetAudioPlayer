using System;
using System.Collections.Generic;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.AudioPlayerServer.AudioEngine
{
    /// <summary>
    /// Компонент, отвечающий за воспроизведение аудиофайлов
    /// </summary>
    interface IAudioEngine
    {
        /// <summary>
        /// Текущее состояние плеера
        /// </summary>
        PlayerState State { get; }

        /// <summary>
        /// Длительность текущей композиции
        /// </summary>
        TimeSpan ItemDuration { get; }

        /// <summary>
        /// Текущее положение
        /// </summary>
        TimeSpan Position { get; }

        /// <summary>
        /// Текущая композиция
        /// </summary>
        string Item { get; }

        /// <summary>
        /// Список композиций
        /// </summary>
        IEnumerable<string> Items { get; }

        /// <summary>
        /// Событие изменения состояния плеера
        /// </summary>
        event EventHandler StateChanged;
        
        /// <summary>
        /// Возобновить воспроизведение
        /// </summary>
        void Play();

        /// <summary>
        /// Воспроизвести список композиций, начиная с указанной
        /// </summary>
        /// <param name="items">Список композиций для воспроизведения</param>
        /// <param name="firstItem">Композиция, с которой необходимо начать воспроизведение</param>
        void Play(IEnumerable<string> items, string firstItem = null);

        /// <summary>
        /// Приостановить воспроизведение
        /// </summary>
        void Pause();

        /// <summary>
        /// Остановить воспроизведение
        /// </summary>
        void Stop();

        /// <summary>
        /// Перейти к указанной позиции
        /// </summary>
        /// <param name="newPosition">Новая позиция</param>
        void Seek(TimeSpan newPosition);

        /// <summary>
        /// Перейти к следующей композиции
        /// </summary>
        void Next();

        /// <summary>
        /// Перейти к началу текущей композиции / предыдущей композиции
        /// </summary>
        void Prev();

    }
}
