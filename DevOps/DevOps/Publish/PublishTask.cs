﻿//
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

using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Publish;
using Cake.Core.IO;
using Cake.Frosting;

namespace DevOps.Publish
{
    [TaskName( "publish" )]
    public sealed class PublishTask : DevopsTask
    {
        // ---------------- Functions ----------------

        public override bool ShouldRun( BuildContext context )
        {
            if( context.FileExists( context.KeySeedFile ) == false )
            {
                context.Information( "A key seed file must be generated via the 'generate_key_seed' target before publishing." );
                return false;
            }

            return true;
        }

        public override void Run( BuildContext context )
        {
            context.EnsureDirectoryExists( context.DistFolder );

            DirectoryPath looseFilesDir = context.LooseFilesDistFolder;
            context.EnsureDirectoryExists( looseFilesDir );
            context.CleanDirectory( looseFilesDir );

            context.Information( "Publishing App" );

            var publishOptions = new DotNetPublishSettings
            {
                Configuration = "Release",
                OutputDirectory = looseFilesDir.ToString()
            };

            FilePath servicePath = context.SrcDir.CombineWithFilePath(
                "BsaTroop53Editor.Web/BsaTroop53Editor.Web.csproj"
            );

            context.DotNetPublish( servicePath.ToString(), publishOptions );
            context.Information( string.Empty );

            //CopyRootFile( context, "Readme.md" );
            //CopyRootFile( context, "Credits.md" );
            CopyRootFile( context, "License.md" );
        }

        private static void CopyRootFile( BuildContext context, FilePath fileName )
        {
            fileName = context.RepoRoot.CombineWithFilePath( fileName );
            context.Information( $"Copying '{fileName}' to dist" );
            context.CopyFileToDirectory( fileName, context.LooseFilesDistFolder );
        }
    }
}
