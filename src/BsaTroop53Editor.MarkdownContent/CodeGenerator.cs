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
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;

namespace BsaTroop53Editor.MarkdownContent
{
    [Generator]
    public sealed class CodeGenerator : ISourceGenerator
    {
        // ---------------- Functions ----------------

        public void Execute( GeneratorExecutionContext context )
        {
            var buffer = new byte[4];
            using( var rng = RandomNumberGenerator.Create() )
            {
                rng.GetBytes( buffer, 0, buffer.Length );
            }

            int seed = BitConverter.ToInt32( buffer, 0 );

            // Find the main method
            var mainMethod = context.Compilation.GetEntryPoint( context.CancellationToken );

            string source = $@"
namespace {nameof( BsaTroop53Editor )}.Web;
internal sealed partial class CodeGenerator
{{
    public CodeGenerator() : this( {seed} )
    {{
    }}
}}
";

            context.AddSource( "CodeGenerator.g.cs", source );
        }

        public void Initialize( GeneratorInitializationContext context )
        {
        }
    }
}