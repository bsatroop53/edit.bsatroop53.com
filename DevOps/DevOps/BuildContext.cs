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

using Cake.Common;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace DevOps
{
    public class BuildContext : FrostingContext
    {
        // ---------------- Fields ----------------

        internal const string KeySeedFileArg = "key_seed_file";

        // ---------------- Constructor ----------------

        public BuildContext( ICakeContext context ) :
            base( context )
        {
            this.RepoRoot = context.Environment.WorkingDirectory;
            this.SrcDir = this.RepoRoot.Combine( "src" );
            this.Solution = this.SrcDir.CombineWithFilePath( "BsaTroop53Editor.sln" );
            this.DistFolder = this.RepoRoot.Combine( "dist" );
            this.LooseFilesDistFolder = this.DistFolder.Combine( "files" );
            this.TestResultsFolder = this.RepoRoot.Combine( "TestResults" );
            this.WebCsProj = this.SrcDir.CombineWithFilePath( "BsaTroop53Editor.Web/BsaTroop53Editor.Web.csproj" );
            this.TestCsProj = this.SrcDir.CombineWithFilePath( "BsaTroop53Editor.Tests/BsaTroop53Editor.Tests.csproj" );

            if( context.HasArgument( KeySeedFileArg ) )
            {
                this.KeySeedFile = new FilePath( context.Argument<string>( KeySeedFileArg ) );
            }
            else
            {
                this.KeySeedFile = null;
            }
        }

        // ---------------- Properties ----------------

        public DirectoryPath RepoRoot { get; }

        public DirectoryPath SrcDir { get; }

        public FilePath Solution { get; }

        public DirectoryPath DistFolder { get; }

        public DirectoryPath LooseFilesDistFolder { get; }

        public DirectoryPath TestResultsFolder { get; }

        public FilePath WebCsProj { get; }

        public FilePath TestCsProj { get; }

        public FilePath? KeySeedFile { get; }

        // ---------------- Functions ----------------

        public void SetKeySeedEnvironmentVariable()
        {
            ArgumentNullException.ThrowIfNull( this.KeySeedFile, KeySeedFileArg );

            string fileContents = File.ReadAllText( this.KeySeedFile.FullPath ).Trim();
            int seed = int.Parse( fileContents );

            System.Environment.SetEnvironmentVariable( "KEY_SEED", seed.ToString() );
        }
    }
}
