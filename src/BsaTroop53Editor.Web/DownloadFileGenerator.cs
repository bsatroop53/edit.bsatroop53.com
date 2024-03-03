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
                var zipArchive = new ZipArchive( stream, ZipArchiveMode.Create );

                await WriteMarkdownFile( zipArchive, post );
                await WriteImageXmlFile( zipArchive, post );
                foreach( PhotoInfo photo in post.Photos ?? new List<PhotoInfo>() )
                {
                    await WriteImage( zipArchive, post, photo );
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
