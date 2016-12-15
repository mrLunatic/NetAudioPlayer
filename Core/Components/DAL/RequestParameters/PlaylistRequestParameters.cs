using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    public class PlaylistRequestParameters : RequestParameters
    {
        public static readonly IEnumerable<SortingItem<PlaylistSorting>> DefaultOrderBy
            = new[]
            {
                new SortingItem<PlaylistSorting>(PlaylistSorting.Name),
            };

        public IEnumerable<SortingItem<PlaylistSorting>> OrderBy { get; set; }
    }
}
