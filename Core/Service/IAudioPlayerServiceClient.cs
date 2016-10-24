using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.Core.Service
{
    public interface IAudioPlayerServiceClient
    {
        Error Play();

        Error Play(IEnumerable<string> items, string firstItem = null);

        Error Pause();

        Error Stop();

        Error PlayNext();

        Error PlayPrev();

        Error Seek(TimeSpan position);

        Tuple<StatusInfoMessage, Error> GetStatusInfo();

        Error SetStatus(RepeatMode? repeat = null, bool? shuffle = null, double? volume = null);

    }
}
