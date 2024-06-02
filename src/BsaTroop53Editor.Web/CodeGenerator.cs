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

using OtpNet;

namespace BsaTroop53Editor.Web
{
    internal sealed partial class CodeGenerator
    {
        // ---------------- Fields ----------------

        private readonly Totp totp;

        // ---------------- Constructor ----------------

        public CodeGenerator()
        {
            var keyGenerator = new KeyGenerator.KeyGenerator();

            this.totp = new Totp(
                keyGenerator.Key.ToArray(),
                step: 30,
                mode: OtpHashMode.Sha512,
                totpSize: 8,
                timeCorrection: null
            );

            // Clear key from RAM.
            // It still is somewhere in RAM in the Totp class,
            // but it is in one less spot.
            keyGenerator.ClearKey();
        }

        // ---------------- Functions ----------------

        public string GetCode()
        {
            return this.totp.ComputeTotp();
        }
    }
}