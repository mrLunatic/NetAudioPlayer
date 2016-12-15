using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spartan.Common.Data
{
    public interface ITrackStore
    {
        [JsonProperty(DataContract.TrackStoreTracksCountField)]
        int TracksCount { get; }
    }
}
