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

using System.Xml.Linq;
using BsaTroop53Editor.Web.Models;

namespace BsaTroop53Editor.Web
{
    public static class PhotoXmlGenerator
    {
        // ---------------- Fields ----------------

        internal const string xmlFolder = "_gallerydata";

        // ---------------- Functions ----------------

        /// <param name="fileName">
        /// Set to empty string if this method returns null.
        /// </param>
        /// <returns>
        /// Null if no XML file for photos should be generated.
        /// </returns>
        public static XDocument? ToGalleryXml( this Post post, out string fileName )
        {
            if( ( post.Photos is null ) || ( post.Photos.Any() == false ) )
            {
                fileName = "";
                return null;
            }

            fileName = GetPhotoXmlFileName( post );

            var dec = new XDeclaration( "1.0", "utf-8", "yes" );
            var doc = new XDocument( dec );

            string galleryPath = GetGalleryPath( post );

            var root = new XElement( "ImageGallery" );
            root.Add( new XAttribute( "ImageDir", galleryPath ) );
            root.Add( new XAttribute( "ThumbnailOutputDir", $"{galleryPath}/thumbnails/" ) );
            root.Add( new XAttribute( "DefaultThumbnailScale", "0.70" ) );

            foreach( PhotoInfo photo in post.Photos )
            {
                var imageElement = new XElement(
                    "Image",
                    new XElement( "Alt", photo.AltText ),
                    new XElement( "Caption", photo.Caption ),
                    new XElement( "ThumbnailScale", photo.GetCompressionSize() )
                );

                imageElement.Add( new XAttribute( "FileName", photo.NewFileName ) );

                root.Add( imageElement );
            }

            doc.Add( root );
            return doc;
        }

        public static string GetPhotoXmlFileNameWithouExtension( this Post post )
        {
            ArgumentNullException.ThrowIfNull( post.Title, nameof( post.Title ) );

            return $"{post.Title.ToFileName()}";
        }

        public static string GetXmlRelativePath( this Post post )
        {
            return $"{xmlFolder}/{GetPhotoXmlFileName( post )}";
        }

        private static string GetPhotoXmlFileName( Post post )
        {
            return $"{GetPhotoXmlFileNameWithouExtension( post )}.xml";
        }

        private static string GetGalleryPath( Post post )
        {
            ArgumentNullException.ThrowIfNull( post.Title, nameof( post.Title ) );

            return $"static/img/galleries/{post.Title.ToFileName()}";
        }
    }
}
