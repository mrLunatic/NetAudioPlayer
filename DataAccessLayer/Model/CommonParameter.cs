using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAudioPlayer.DataAccessLayer.Model
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
