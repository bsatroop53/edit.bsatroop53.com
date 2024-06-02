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
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;

namespace BsaTroop53Editor.KeyGenerator
{
    [Generator]
    public sealed class SeedGenerator : ISourceGenerator
    {
        // ---------------- Functions ----------------

        public void Execute( GeneratorExecutionContext context )
        {
            int seed = 0;
            string keySeed = Environment.GetEnvironmentVariable( "KEY_SEED" );
            if( int.TryParse( keySeed, out seed ) == false )
            {
                context.ReportDiagnostic(
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "BSAT53_01",
                            "No key seed specified.",
                            "KEY_SEED environment variable not specified, using default seed.",
                            "Configuration",
                            DiagnosticSeverity.Warning,
                            true
                        ),
                        null
                    )
                );
            }

            // Find the main method
            var mainMethod = context.Compilation.GetEntryPoint( context.CancellationToken );

            string source = $@"
namespace {nameof( BsaTroop53Editor )}.KeyGenerator;
internal sealed partial class KeyGenerator
{{
    public KeyGenerator() : this( {seed} )
    {{
    }}
}}
";

            context.AddSource( "KeyGenerator.g.cs", source );
        }

        public void Initialize( GeneratorInitializationContext context )
        {
        }
    }
}