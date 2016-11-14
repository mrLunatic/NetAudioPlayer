using System.IO;
using NetAudioPlayer.Core.Components.Player;

namespace NetAudioPlayer.ConsoleServer.Components
{
    internal class UniversalItemLoader : IItemLoader
    {
        #region IPlayListItemLoader

        public Stream LoadItem(string item)
        {
            return null;
        }

        /// <summary>
        /// Удаляет из памяти все предыдущие компоненты
        /// </summary>
        public void Reset()
        {
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion
    }
}
