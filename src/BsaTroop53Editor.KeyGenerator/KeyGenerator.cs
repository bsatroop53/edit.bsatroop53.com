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

namespace BsaTroop53Editor.KeyGenerator
{
    internal sealed partial class KeyGenerator
    {
        // ---------------- Fields ----------------

        private readonly List<byte> key;

        // ---------------- Constructor ----------------

        public KeyGenerator( int seed )
        {
            var key = new byte[512];

            var rand = new Random( seed );
            rand.NextBytes( key );

            this.key = new List<byte>( key );
            this.Key = this.key.AsReadOnly();

            if( OperatingSystem.IsBrowser() )
            {
                // Fill with garbage in case someone tries to do a RAM
                // dump to get the key.
                this.Base64Key = DateTime.UtcNow.GetHashCode().ToString();
            }
            else
            {
                // Used post-compile with command line tools to get the key
                // so the server can use it.  This runs on a desktop,
                // not a browser, so its safe to set.
                this.Base64Key = Convert.ToBase64String( key );
            }
        }

        // ---------------- Propeties ----------------

        public string Base64Key { get; }

        public IReadOnlyList<byte> Key { get; }

        // ---------------- Functions ----------------

        public void ClearKey()
        {
            this.key.Clear();
        }
    }
}
