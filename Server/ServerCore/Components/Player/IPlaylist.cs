using System.Collections.Generic;
using NetAudioPlayer.Common.Data;
using NetAudioPlayer.Common.Model;

namespace NetAudioPlayer.ServerCore.Components.Player
{
    /// <summary>
    /// Плейлист
    /// </summary>
    public interface IPlaylist
    {
        /// <summary>
        /// Текущая композиция
        /// </summary>
        Track Item { get; }

        /// <summary>
        /// Список композиций
        /// </summary>
        IEnumerable<Track> Items { get; }

        /// <summary>
        /// Режим повторения
        /// </summary>
        RepeatMode RepeatMode { get; set; }

        /// <summary>
        /// Режим перемещивания
        /// </summary>
        bool Shuffle { get; set; }
    
        /// <summary>
        /// Сбрасывает содержимое плейлиста и заполняет его согласно переданному списку
        /// </summary>
        /// <param name="items">Новые элементы для воспроизведения</param>
        void Init(IEnumerable<Track> items);

        /// <summary>
        /// Переходит к следующей композиции
        /// </summary>
        void Next();

        /// <summary>
        /// Начинает воспроизведение списка с указанного элемента
        /// </summary>
        /// <param name="track">Элемент, с которого следует начать воспроизведение списка</param>
        void Play(Track track = null);

        /// <summary>
        /// Переходит к предыдущей композиции
        /// </summary>
        void Prev();

        /// <summary>
        /// Сбрасывает содержимое плейлиста
        /// </summary>
        void Reset();
    }
}