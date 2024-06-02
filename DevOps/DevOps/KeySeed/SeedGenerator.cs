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

using System.Security.Cryptography;
using Cake.Common.Diagnostics;
using Cake.Frosting;

namespace DevOps.KeySeed
{
    [TaskName( "generate_key_seed" )]
    [TaskDescription( "Generates the seed used to help generate the TOTP key that must be the same on the client and server." )]
    public sealed class SeedGenerator : DevopsTask
    {
        // ---------------- Functions ----------------

        public override void Run( BuildContext context )
        {
            var buffer = new byte[4];
            using( var rng = RandomNumberGenerator.Create() )
            {
                rng.GetBytes( buffer, 0, buffer.Length );
            }

            int seed = BitConverter.ToInt32( buffer, 0 );

            File.WriteAllText( context.KeySeedFile.FullPath, seed.ToString().Trim() );
            context.Information( $"Key seed file written to {context.KeySeedFile.FullPath}." );
        }
    }
}
