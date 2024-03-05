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
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace BsaTroop53Editor.MarkdownContent
{
    [Generator]
    public sealed class MarkdownGenerator : ISourceGenerator
    {
        // ---------------- Fields ----------------

        private static readonly MarkDigEngine markdownEngine = new MarkDigEngine();

        // ---------------- Functions ----------------

        public void Execute( GeneratorExecutionContext context )
        {
            // Find the main method
            var mainMethod = context.Compilation.GetEntryPoint( context.CancellationToken );

            string source = $@"
namespace {nameof( BsaTroop53Editor )}.Web;
internal static partial class MarkdownContent
{{
    static MarkdownContent()
    {{
        licenseHtml = 
@""{ConvertMarkdownAsHtml( "License.md" )}
"";

        creditsHtml =
@""{ConvertMarkdownAsHtml( "Credits.md" )}
"";

        homeHtml = 
@""{ConvertMarkdownAsHtml( "Home.md" )}
"";

        faqHtml =
@""{ConvertMarkdownAsHtml( "FAQ.md" )}
"";

        privacyHtml =
@""{ConvertMarkdownAsHtml( "PrivacyPolicy.md" )}
"";

        releaseNotesHtml =
@""{ConvertMarkdownAsHtml( "ReleaseNotes.md" )}
"";
    }}
}}
            ";

            context.AddSource( "MarkdownContent.g.cs", source );
        }

        public void Initialize( GeneratorInitializationContext context )
        {
        }

        private static string ConvertMarkdownAsHtml( string resourceName )
        {
            string markDown = ReadStringResource(
                $"{nameof( BsaTroop53Editor )}.{nameof( MarkdownContent )}.Content.{resourceName}"
            );

            return markdownEngine.Convert( markDown ).Replace( "\"", "\"\"" );
        }

        private static string ReadStringResource( string resourceName )
        {
            Assembly assem = typeof( MarkdownGenerator ).Assembly;
            using( Stream stream = assem.GetManifestResourceStream( resourceName ) )
            {
                if( stream == null )
                {
                    throw new InvalidOperationException( $"Could not open stream for {resourceName}" );
                }

                using( StreamReader reader = new StreamReader( stream ) )
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
