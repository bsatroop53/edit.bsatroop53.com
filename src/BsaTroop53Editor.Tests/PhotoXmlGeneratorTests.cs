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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BsaTroop53Editor.Web;
using BsaTroop53Editor.Web.Models;

namespace BsaTroop53Editor.Tests
{
    [TestClass]
    public sealed class PhotoXmlGeneratorTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void NullPhotosTest()
        {
            // Setup
            var post = new Post
            {
                Photos = null
            };

            // Act
            XDocument? xmlFile = post.ToGalleryXml( out string fileName );

            // Check
            Assert.IsNull( xmlFile );
            Assert.AreEqual( "", fileName );
        }

        [TestMethod]
        public void NoPhotosTest()
        {
            // Setup
            var post = new Post
            {
                Photos = new List<PhotoInfo>()
            };

            // Act
            XDocument? xmlFile = post.ToGalleryXml( out string fileName );

            // Check
            Assert.IsNull( xmlFile );
            Assert.AreEqual( "", fileName );
        }

        [TestMethod]
        public void PhotosTest()
        {
            // Setup
            var photos = new List<PhotoInfo>
            {
                new PhotoInfo( "1930 gerrysipter franklinvandewal jaypaul.jpg", "abcef", 50 )
                {
                    AltText = "Three scouts.",
                    Caption = "Patrol Activity - 1930's."
                },
                new PhotoInfo( "4apr1931 Newspaper Thurstonpauleagle.jpg", "12345", 2 * Constants.MegaByte )
            };

            var post = new Post
            {
                Title = "Historical Photographs",
                Photos = photos
            };

            string expectedString =
$@"<ImageGallery ImageDir=""static/img/galleries/historical_photographs"" ThumbnailOutputDir=""static/img/galleries/historical_photographs/thumbnails/"" DefaultThumbnailScale=""0.70"">
  <Image FileName=""1930_gerrysipter_franklinvandewal_jaypaul_{photos[0].Id}.jpg"">
    <Alt>Three scouts.</Alt>
    <Caption>Patrol Activity - 1930's.</Caption>
    <ThumbnailScale>0.99</ThumbnailScale>
  </Image>
  <Image FileName=""4apr1931_newspaper_thurstonpauleagle_{photos[1].Id}.jpg"">
    <Alt />
    <Caption />
    <ThumbnailScale>0.65</ThumbnailScale>
  </Image>
</ImageGallery>";

            // Act
            XDocument? xmlFile = post.ToGalleryXml( out string fileName );

            // Check
            Assert.IsNotNull( xmlFile );

            Assert.AreEqual( expectedString, xmlFile.ToString( SaveOptions.None ) );
            Assert.AreEqual( "historical_photographs.xml", fileName );
        }
    }
}
