using System;

namespace NetAudioPlayer.ServerCore.Components.Common
{
    public interface ITimer
    {
        event EventHandler Tick;

        void Start(double interval);

        void Stop();


    }
}
