using System.Collections.Generic;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    public sealed class ArtistRequestParameters : RequestParameters
    {
        public static readonly IEnumerable<SortingItem<ArtistSorting>> DefaultOrderBy
            = new[]
            {
                new SortingItem<ArtistSorting>(ArtistSorting.Name),
            };

        public IEnumerable<SortingItem<ArtistSorting>> OrderBy { get; set; }
    }
}