using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAudioPlayer.DataAccessLayer.Model
{
    public class RatingParameter
    {
        public int? RatingMin { get; set; }

        public int? RatingMax { get; set; }

        public int? Rating { get; set; }
    }
}
