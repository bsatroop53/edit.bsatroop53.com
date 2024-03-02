//
// BsaTroop53 Editor - A way to create posts for bsatroop53.com
// Copyright (C) 2024 Seth Hendrick
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BsaTroop53Editor.Web.Models;

public class Post
{
    // ---------------- Properties ----------------

    [Required]
    public string? Title { get; set; }

    [Required]
    public DateOnly? PostDate { get; set; }

    public bool IsPostDateAnEstimate { get; set; } = false;

    [Required]
    public string? Description { get; set; }

    public string? Author { get; set; }

    [Range(
        -90,
        90, 
        ErrorMessage = "Invalid Latitude (Must be between -90 and 90)",
        MaximumIsExclusive = false,
        MinimumIsExclusive = false
    )]
    public double? Latitude { get; set; }

    [Range(
        -180,
        180,
        ErrorMessage = "Invalid Longitude (Must be between -180 and 180)",
        MaximumIsExclusive = false,
        MinimumIsExclusive = false
    )]
    public double? Longitude { get; set; }

    [Required]
    public PostCategory Category { get; set; } = PostCategory.News;

    /// <remarks>
    /// Post contents can be empty if there is at least one photo.
    /// </remarks>
    public string? PostContents { get; set; } =
        "## Markdown Editor\n\nWrite your post here!\n\nIf you don't know Markdown, click the (?) button above.";

    [Required]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Please acknowledge the contributor guidelines.")]
    public bool Agreed { get; set; } = false;

    public ICollection<string>? Tags { get; set; } = null;

    public ICollection<string>? Photos { get; set; } = null;
}