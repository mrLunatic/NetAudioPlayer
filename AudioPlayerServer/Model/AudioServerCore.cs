using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace NetAudioPlayer.AudioPlayerServer.Model
{
    public abstract class AudioServerBase
    {

        internal WaveOut WaveOut { get; }

        internal WaveStream Stream { get; }



    }
}
