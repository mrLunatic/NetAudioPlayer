using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedBass;
using Spartan.Common.Data;
using Spartan.ServerCore.Components.Player;

namespace Spartan.BassAudioEngine.Components.Player
{
    public class BassAudioEngine : IAudioEngine
    {
        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IAudioEngine

        /// <summary>
        /// Длина композиции
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Текущая позиция воспроизведения
        /// </summary>
        public TimeSpan Position { get; }

        /// <summary>
        /// Уровень громкости в диапазоне [0.00 - 1.00]
        /// </summary>
        public double Volume { get; set; }


        public event EventHandler PlaybackStopped;

        /// <summary>
        /// Инициализирует аудиоустроства
        /// </summary>
        public void Init()
        {
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Деинициализирует аудиоустройства
        /// </summary>
        public void Deinit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Приостановить воспроизведение
        /// </summary>
        public void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возобновляет воспроизведение
        /// </summary>
        public void Resume()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Остановить воспроизведение
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Перейти к указанному временному промежутку
        /// </summary>
        /// <param name="position">Временной промежуток</param>
        public void Seek(TimeSpan position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Воспроизвести элемент
        /// </summary>
        /// <param name="track">Элемент для воспроизведения</param>
        public void Play(ITrack track)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
