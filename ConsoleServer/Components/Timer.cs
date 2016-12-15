using System;
using Spartan.ServerCore.Components.Common;

namespace Spartan.ServerNet45.Components
{
    public sealed class Timer : ITimer, IDisposable
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer();

        public Timer()
        {
            _timer.Elapsed += (sender, args) => Tick?.Invoke(this, new EventArgs());
        }

        #region Implementation of ITimer

        public event EventHandler Tick;
        public void Start(double interval)
        {
            _timer.Interval = interval;
            _timer.Start();

        }

        public void Stop()
        {
            _timer.Stop();
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            _timer.Dispose();
        }

        #endregion
    }
}
