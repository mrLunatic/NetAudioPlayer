using System.Collections.Generic;

namespace NetAudioPlayer.Core.Components.DAL
{
    public class CommonParameter
    {
        public IEnumerable<int> Ids { get; set; }
        public string Name { get; set; }        
        public string Tag { get; set; }
        public RatingParameter Rating { get; set; }
        public PageParameter Page { get; set; }
    }
}
