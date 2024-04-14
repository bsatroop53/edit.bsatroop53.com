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

using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using BsaTroop53Editor.Web.Models;

namespace BsaTroop53Editor.Web
{
    public static class DownloadFileGenerator
    {
        public static async Task<FileToDownload> CreateZipFile( this Post post )
        {
            bool hasPhotos = ( post.Photos is not null ) && post.Photos.Any();

            string fileName = hasPhotos ?
                $"{post.GetMarkdownFileNameWithoutExtension()}.zip.bsat53" :
                $"{post.GetMarkdownFileNameWithoutExtension()}.md.bsat53";

            // If we don't have photos, just return the markdown file contents
            // and call it a day.
            if( hasPhotos == false )
            {
                return new FileToDownload(
                    fileName,
                    new MemoryStream(
                        Encoding.UTF8.GetBytes( post.ToMarkdownFile( out string _ ) )
                    )
                );
            }

            // Otherwise, time to zip some stuff.
            MemoryStream? stream = null;
            try
            {
                stream = new MemoryStream();
                using( var zipArchive = new ZipArchive( stream, ZipArchiveMode.Create, true ) )
                {
                    await WriteMarkdownFile( zipArchive, post );
                    await WriteImageXmlFile( zipArchive, post );
                    foreach( PhotoInfo photo in post.Photos ?? new List<PhotoInfo>() )
                    {
                        await WriteImage( zipArchive, post, photo );
                    }
                }

            }
            catch( Exception )
            {
                stream?.Dispose();
                throw;
            }

            return new FileToDownload( fileName, stream );
        }

        private static async Task WriteMarkdownFile( ZipArchive zipArchive, Post post )
        {
            string markdownString = post.ToMarkdownFile( out string markdownFileName );
            ZipArchiveEntry markdownFile = zipArchive.CreateEntry( $"_posts/{markdownFileName}" );
            using( Stream markdownFileStream = markdownFile.Open() )
            using( StreamWriter writer = new StreamWriter( markdownFileStream ) )
            {
                await writer.WriteAsync( markdownString );
            }
        }

        private static async Task WriteImageXmlFile( ZipArchive zipArchive, Post post )
        {
            XDocument? xmlFile = post.ToGalleryXml( out string fileName );
            if( xmlFile is null )
            {
                return;
            }

            ZipArchiveEntry markdownFile = zipArchive.CreateEntry( $"{PhotoXmlGenerator.xmlFolder}/{fileName}" );
            using( Stream markdownFileStream = markdownFile.Open() )
            {
                await xmlFile.SaveAsync( markdownFileStream, SaveOptions.None, CancellationToken.None );
            }
        }

        private static async Task WriteImage( ZipArchive zipArchive, Post post, PhotoInfo photo )
        {
            ZipArchiveEntry photoEntry = zipArchive.CreateEntry( $"static/img/galleries/{post.GetPhotoXmlFileNameWithouExtension()}/{photo.NewFileName}" );
            using( Stream photoStream = photoEntry.Open() )
            {
                byte[] photoAsByte = Convert.FromBase64String( photo.Base64Value );
                await photoStream.WriteAsync( photoAsByte );
            }
        }
    }
}
