using System;

namespace NetAudioPlayer.Core.Components.Common
{
    public interface ITimer
    {
        event EventHandler Tick;

        void Start(double interval);

        void Stop();


    }
}
