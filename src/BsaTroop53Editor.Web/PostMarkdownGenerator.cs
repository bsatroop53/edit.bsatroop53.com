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

using System.Text;
using System.Text.RegularExpressions;
using BsaTroop53Editor.Web.Models;

namespace BsaTroop53Editor.Web
{
    public static class PostMarkdownGenerator
    {
        // ---------------- Fields ----------------

        private static readonly Regex whitespaceRegex = new Regex(
            @"\s+",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture
        );

        private static readonly Regex quoteRegex = new Regex(
            @"""",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture
        );

        private static readonly Regex newLineRegex = new Regex(
            @"[\r\n]+",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture
        );

        // ---------------- Functions ----------------

        public static string ToMarkdownFile( this Post post, out string fileName )
        {
            ArgumentNullException.ThrowIfNull( post.Title, nameof( post.Title ) );
            ArgumentNullException.ThrowIfNull( post.Description, nameof( post.Description ) );

            fileName = GetMarkdownFileName( post );

            var fileContents = new StringBuilder();

            // Add markdown stuff.
            fileContents.AppendLine( "---" );
            fileContents.AppendLine( $@"layout: ""{GetLayout( post )}""" );
            fileContents.AppendLine( $@"title: ""{EscapeYaml( post.Title )}""" );
            if( post.Author is not null )
            {
                fileContents.AppendLine( $@"author: ""{EscapeYaml( post.Author )}""" );
            }
            fileContents.AppendLine( $@"category: ""{post.Category.ToHumanReadableString()}""" );
            fileContents.AppendLine( $@"description: ""{EscapeYaml( post.Description )}""" );
            if( post.Tags?.Any() ?? false )
            {
                fileContents.AppendLine( GetTagString( post ) );
            }
            fileContents.AppendLine( @$"is_date_estimate: {post.IsPostDateAnEstimate}" );

            if(
                ( post.Category == PostCategory.EagleProjects ) &&
                ( post.Latitude is not null ) &&
                ( post.Longitude is not null )
            )
            {
                fileContents.AppendLine( $"latitude: {post.Latitude}" );
                fileContents.AppendLine( $"longitude: {post.Longitude}" );
            }

            if( post.Photos?.Any() ?? false )
            {
                fileContents.AppendLine( $@"image_gallery: ""{post.GetXmlRelativePath()}""" );
            }

            fileContents.AppendLine( "---" );
            fileContents.AppendLine();

            if( string.IsNullOrWhiteSpace( post.PostContents ) == false )
            {
                fileContents.AppendLine( post.PostContents );
            }

            return fileContents.ToString();
        }

        public static string GetMarkdownFileNameWithoutExtension( this Post post )
        {
            ArgumentNullException.ThrowIfNull( post.PostDate, nameof( post.PostDate ) );
            ArgumentNullException.ThrowIfNull( post.Title, nameof( post.Title ) );

            return $"{post.PostDate.Value.ToString( "yyyy-MM-dd" )}-{whitespaceRegex.Replace( post.Title, "" )}";
        }

        private static string GetMarkdownFileName( Post post )
        {
            return $"{GetMarkdownFileNameWithoutExtension( post )}.md";
        }

        private static string GetLayout( Post post )
        {
            if( post.Photos?.Any() ?? false )
            {
                return "imagegallerypost2";
            }
            else
            {
                // No photos means use the standard layout.
                return "post";
            }
        }

        private static string GetTagString( Post post )
        {
            var tagBuilder = new StringBuilder();
            tagBuilder.Append( "tags: [" );
            foreach( string tag in ( post.Tags ?? Array.Empty<string>() ).OrderBy( t => t ) )
            { 
                tagBuilder.Append( $@"""{EscapeYaml( tag )}"", " );
            }
            
            // Remove trailing comma
            tagBuilder.Remove( tagBuilder.Length - 2, 2 );
            tagBuilder.Append( "]" );

            return tagBuilder.ToString();
        }

        private static string EscapeYaml( string yamlValue )
        {
            yamlValue = quoteRegex.Replace( yamlValue, @"\""" );
            yamlValue = newLineRegex.Replace( yamlValue, " " );

            return yamlValue;
        }
    }
}
