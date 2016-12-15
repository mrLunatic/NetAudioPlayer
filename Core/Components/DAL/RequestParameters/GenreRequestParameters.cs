using System.Collections.Generic;
using Spartan.Common.Model;
using Spartan.Common.Model.Sorting;

namespace Spartan.ServerCore.Components.DAL.RequestParameters
{
    public sealed class GenreRequestParameters : RequestParameters
    {
        public static readonly IEnumerable<SortingItem<GenreSorting>> DefaultOrderBy
            = new[]
            {
                new SortingItem<GenreSorting>(GenreSorting.Name),
            };

        public IEnumerable<SortingItem<GenreSorting>> OrderBy { get; set; }
    }
}