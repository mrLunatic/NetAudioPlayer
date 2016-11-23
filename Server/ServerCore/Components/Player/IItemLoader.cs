using System;
using System.IO;

namespace NetAudioPlayer.ServerCore.Components.Player
{
    /// <summary>
    /// Загрузчик файлов
    /// </summary>
    public interface IItemLoader : IDisposable
    {
        /// <summary>
        /// Загружает указанный элемент
        /// </summary>
        /// <param name="item">Путь к композиции</param>
        /// <returns>Новый элемент плейлиста, созданный для указанной композиции</returns>
        Stream LoadItem(string item);

        /// <summary>
        /// Удаляет из памяти все предыдущие компоненты
        /// </summary>
        void Reset();
    }
}