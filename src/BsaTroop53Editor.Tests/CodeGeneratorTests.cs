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

using BsaTroop53Editor.Web;

namespace BsaTroop53Editor.Tests
{
    [TestClass]
    public sealed class CodeGeneratorTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void GenerateKey()
        {
            string? assemblyLocation = Path.GetDirectoryName( typeof( CodeGeneratorTests ).Assembly.Location );
            Assert.IsNotNull( assemblyLocation );
            var distFolder = new DirectoryInfo(
                Path.Combine(
                    assemblyLocation, // net8.0
                    "..",             // Debug
                    "..",             // bin
                    "..",             // Project
                    "..",             // src
                    "..",             // root
                    "dist",
                    "keys"
                )
            );

            if( distFolder.Exists == false )
            {
                Directory.CreateDirectory( distFolder.FullName );
            }

            var codeGenerator = new CodeGenerator();

            string keyFile = Path.Combine( distFolder.FullName, "key" );
            File.WriteAllText( keyFile, codeGenerator.Base64Key );
        }
    }
}