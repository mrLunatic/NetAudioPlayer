using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.Common.Model.Sorting
{
    public enum TrackSorting
    {
        [EnumMember(Value = @"id")]
        Id,
        [EnumMember(Value = @"name")]
        Name,
        [EnumMember(Value = @"rating")]
        Rating,
        [EnumMember(Value = @"tag")]
        Tag,
        [EnumMember(Value = @"duration")]
        Duration,
        [EnumMember(Value = @"artistId")]
        ArtistId,
        [EnumMember(Value = @"artistName")]
        ArtistName,
        [EnumMember(Value = @"albumId")]
        AlbumId,
        [EnumMember(Value = @"AlbumName")]
        AlbumName,
        [EnumMember(Value = @"albumNumber")]
        AlbumNumber,
        [EnumMember(Value = @"genreId")]
        GenreId,
        [EnumMember(Value = @"genreName")]
        GenreName,
        [EnumMember(Value = @"uri")]
        Uri
    }
}
