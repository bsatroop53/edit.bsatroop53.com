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

namespace BsaTroop53Editor.Web.Models
{
    public class PhotoInfo
    {
        // ---------------- Fields ----------------

        private static uint nextId = 0;

        // ---------------- Constructor ----------------

        public PhotoInfo( string orginalFileName, string newFileName, string base64Value )
        {
            this.Id = ++nextId;

            this.OriginalFileName = orginalFileName;
            this.NewFileName = newFileName;
            this.Base64Value = base64Value;
        }

        // ---------------- Properties ----------------

        public uint Id { get; }

        public string OriginalFileName { get; }

        public string NewFileName { get; }

        public string Base64Value { get; }

        public string? AltText { get; set; } = null;

        public string? Caption { get; set; } = null;
    }
}
