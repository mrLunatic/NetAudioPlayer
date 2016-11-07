using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.AudioPlayerServer.Components;
using NetAudioPlayer.AudioPlayerServer.Model;

namespace NetAudioPlayer.AudioPlayerServer.Service
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
