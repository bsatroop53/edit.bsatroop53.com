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

using BsaTroop53Editor.Web;
using BsaTroop53Editor.Web.Models;

namespace BsaTroop53Editor.Tests
{
    [TestClass]
    public class PostMarkdownGeneratorTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void NoPhotosPost()
        {
            // Setup
            var uut = new Post
            {
                Agreed = true,
                Author = "Seth Hendrick",
                Category = PostCategory.EagleProjects,
                IsPostDateAnEstimate = false,
                Description = "Some Post",
                Latitude = 42.52742200970382,
                Longitude = -73.75677351300253,
                Photos = null,
                PostContents = "One Line Post!",
                PostDate = new DateOnly( 2024, 3, 2 ),
                Tags = new string[] {"tag", "tag with space", @"tag with ""quote" },
                Title = "Test Post"
            };

            const string expectedFileName = "2024-03-02-TestPost.md";

            const string expectedFileContents =
@"---
layout: ""post""
title: ""Test Post""
author: ""Seth Hendrick""
category: ""Eagle Projects""
description: ""Some Post""
tags: [""tag"", ""tag with space"", ""tag with \""quote""]
is_date_estimate: False
latitude: 42.52742200970382
longitude: -73.75677351300253
---

One Line Post!
";

            // Act
            string actualFileContents = uut.ToMarkdownFile( out string actualFileName );

            // Check
            Assert.AreEqual( expectedFileName, actualFileName );
            Assert.AreEqual( expectedFileContents, actualFileContents );
        }

        [TestMethod]
        public void PhotosPost()
        {
            // Setup
            var uut = new Post
            {
                Agreed = true,
                Author = "Seth Hendrick",
                Category = PostCategory.EagleProjects,
                IsPostDateAnEstimate = false,
                Description = "Some Post",
                Photos = new List<PhotoInfo>
                {
                    new PhotoInfo( "some file", "abcef", 1000 )
                },
                PostContents = $"Two{Environment.NewLine}Line Post!",
                PostDate = new DateOnly( 2024, 3, 2 ),
                Tags = new string[] { "tag", "tag with space", @"tag with ""quote" },
                Title = "Test Post"
            };

            const string expectedFileName = "2024-03-02-TestPost.md";

            const string expectedFileContents =
@"---
layout: ""imagegallerypost2""
title: ""Test Post""
author: ""Seth Hendrick""
category: ""Eagle Projects""
description: ""Some Post""
tags: [""tag"", ""tag with space"", ""tag with \""quote""]
is_date_estimate: False
---

Two
Line Post!
";

            // Act
            string actualFileContents = uut.ToMarkdownFile( out string actualFileName );

            // Check
            Assert.AreEqual( expectedFileName, actualFileName );
            Assert.AreEqual( expectedFileContents, actualFileContents );
        }

        [TestMethod]
        public void NoOptionalStuffPost()
        {
            // Setup
            var uut = new Post
            {
                Agreed = true,
                Category = PostCategory.PressReleases,
                IsPostDateAnEstimate = true,
                Description = "Description",
                Latitude = null,
                Longitude = null,
                Photos = null,
                PostContents = null,
                PostDate = new DateOnly( 2024, 12, 13 ),
                Tags = null,
                Title = "Test Post"
            };

            const string expectedFileName = "2024-12-13-TestPost.md";

            const string expectedFileContents =
@"---
layout: ""post""
title: ""Test Post""
category: ""Press Releases""
description: ""Description""
is_date_estimate: True
---

";

            // Act
            string actualFileContents = uut.ToMarkdownFile( out string actualFileName );

            // Check
            Assert.AreEqual( expectedFileName, actualFileName );
            Assert.AreEqual( expectedFileContents, actualFileContents );
        }

        /// <summary>
        /// Lat/Long should not appear unless its an eagle project.
        /// </summary>
        [TestMethod]
        public void IgnoreCoordinatesIfNotEagleProject()
        {
            // Setup
            var uut = new Post
            {
                Agreed = true,
                Category = PostCategory.PressReleases,
                IsPostDateAnEstimate = true,
                Description = "Description",
                Latitude = 30,
                Longitude = 0,
                Photos = null,
                PostContents = null,
                PostDate = new DateOnly( 2024, 12, 13 ),
                Tags = null,
                Title = "Test Post"
            };

            const string expectedFileName = "2024-12-13-TestPost.md";

            const string expectedFileContents =
@"---
layout: ""post""
title: ""Test Post""
category: ""Press Releases""
description: ""Description""
is_date_estimate: True
---

";

            // Act
            string actualFileContents = uut.ToMarkdownFile( out string actualFileName );

            // Check
            Assert.AreEqual( expectedFileName, actualFileName );
            Assert.AreEqual( expectedFileContents, actualFileContents );
        }
    }
}