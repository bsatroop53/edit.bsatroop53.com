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

using BsaTroop53Editor.Web.Models;

namespace BsaTroop53Editor.Tests.Models
{
    [TestClass]
    public sealed class PhotoInfoTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void NewFileNameTest()
        {
            DoNewFileNameTest( "Something Neat.jpg", "something_neat" );
            DoNewFileNameTest( "NoExtension", "noextension" );
            
            // Only allow special characters . - and _
            DoNewFileNameTest( "123Spec_-ial!@#$%^&*()';.:\"',.jpg", "123spec_-ial____________.____" );
        }

        // ---------------- Test Helpers ----------------

        private static void DoNewFileNameTest(
            string originalFileName,
            string expectedFileNameWithoutExtension
        )
        {
            var uut = new PhotoInfo( originalFileName, "abcd", 1000 );

            string expectedFileName = $"{expectedFileNameWithoutExtension}_{uut.Id}.jpg";

            Assert.AreEqual( expectedFileName, uut.NewFileName );
        }
    }
}
