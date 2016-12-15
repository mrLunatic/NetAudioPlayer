using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.Common.Model.Sorting
{
    public enum PlaylistSorting
    {
        [EnumMember(Value = "id")]
        Id,
        [EnumMember(Value = @"name")]
        Name,
        [EnumMember(Value = @"rating")]
        Rating,
        [EnumMember(Value = @"tag")]
        Tag,
        [EnumMember(Value = @"genreId")]
        GenreId,
        [EnumMember(Value = @"genreName")]
        GenreName,
        [EnumMember(Value = @"tracksCount")]
        TracksCount
    }
}
