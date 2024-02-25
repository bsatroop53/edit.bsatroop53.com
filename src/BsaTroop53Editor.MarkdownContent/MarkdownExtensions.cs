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
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax.Inlines;

namespace BsaTroop53Editor.MarkdownContent
{
    internal class MarkdownExtension : IMarkdownExtension
    {
        public void Setup( MarkdownPipelineBuilder pipeline )
        {
        }

        public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
        {
            var htmlRender = renderer as HtmlRenderer;
            if( htmlRender is null )
            {
                return;
            }

            var linkRender = renderer.ObjectRenderers.FindExact<LinkInlineRenderer>();
            if( linkRender != null )
            {
                linkRender.TryWriters.Remove( TryLinkInlineRenderer );
                linkRender.TryWriters.Add( TryLinkInlineRenderer );
            }

            var autoLinkRender = renderer.ObjectRenderers.Find<AutolinkInlineRenderer>();
            if( autoLinkRender != null )
            {
                autoLinkRender.TryWriters.Remove( TryAutoLinkInlineRenderer );
                autoLinkRender.TryWriters.Add( TryAutoLinkInlineRenderer );
            }
        }

        private bool TryLinkInlineRenderer( HtmlRenderer renderer, LinkInline linkInline )
        {
            if( linkInline.Url is null )
            {
                return false;
            }

            // Only process absolute Uri
            if(
                ( Uri.TryCreate( linkInline.Url, UriKind.RelativeOrAbsolute, out Uri uri ) == false ) ||
                ( uri.IsAbsoluteUri == false )
            )
            {
                return false;
            }

            linkInline.SetAttributes(
                new HtmlAttributes()
                {
                    Properties = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>( "target", "_blank" ),
                        new KeyValuePair<string, string>( "rel", HtmlFormatter.ATagRelProperties )
                    }
                }
            );

            return false;
        }

        private bool TryAutoLinkInlineRenderer( HtmlRenderer renderer, AutolinkInline linkInline )
        {
            if( linkInline.Url is null )
            {
                return false;
            }

            // Only process absolute Uri
            if(
                ( Uri.TryCreate( linkInline.Url, UriKind.RelativeOrAbsolute, out Uri uri ) == false ) ||
                ( uri.IsAbsoluteUri == false )
            )
            {
                return false;
            }

            linkInline.SetAttributes(
                new HtmlAttributes()
                {
                    Properties = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>( "target", "_blank" ),
                        new KeyValuePair<string, string>( "rel", HtmlFormatter.ATagRelProperties )
                    }
                }
            );

            return false;
        }
    }
}
