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

namespace BsaTroop53Editor.Web.Models;

public class Post
{
    // ---------------- Properties ----------------

    [Required]
    public string? Title { get; set; }

    [Required]
    public DateTime? PostDate { get; set; }

    [Required]
    public string? Description { get; set; }

    public string? Author { get; set; }

    [Required]
    public PostCategory Category { get; set; } = PostCategory.News;

    [Required]
    public string? PostContents { get; set; } =
        "## Markdown Editor\n\nWrite your post here!\n\nIf you don't know Markdown, click the (?) button above.";

    [Required]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Please acknowledge the contributor guidelines.")]
    public bool Agreed { get; set; } = false;
}