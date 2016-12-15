using System;

namespace Spartan.ServerCore.Components.Common
{
    public interface ITimer
    {
        event EventHandler Tick;

        void Start(double interval);

        void Stop();


    }
}
