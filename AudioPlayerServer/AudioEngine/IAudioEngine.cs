using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayerServer.Messages;

namespace AudioPlayerServer.AudioEngine
{
    interface IAudioEngine
    {
        PlayerState State { get; }

        TimeSpan ItemDuration { get; }

        TimeSpan Position { get; }

        string Item { get; }


        event EventHandler StateChanged;
        

        void Play();

        void Play(string fileName);

        void Pause();

        void Stop();

        void Seek(TimeSpan newPosition);

    }
}
