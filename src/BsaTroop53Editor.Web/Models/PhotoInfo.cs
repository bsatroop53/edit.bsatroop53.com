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

        public PhotoInfo( string orginalFileName, string base64Value , long fileSize )
        {
            this.Id = ++nextId;

            this.OriginalFileName = orginalFileName;

            this.NewFileName =
                $"{Path.GetFileNameWithoutExtension( this.OriginalFileName.ToFileName() )}_{this.Id}.jpg";
           
            this.Base64Value = base64Value;
            this.FileSize = fileSize;
        }

        // ---------------- Properties ----------------

        public uint Id { get; }

        public string OriginalFileName { get; }

        public string NewFileName { get; }

        public string Base64Value { get; }

        public long FileSize { get; }

        public string? AltText { get; set; } = null;

        public string? Caption { get; set; } = null;

        // ---------------- Functions ----------------

        public double GetCompressionSize()
        {
            // Thumbnails should only be like 0.5MB
            // at most.
            double maxSize = 0.5 * Constants.MegaByte;

            if( this.FileSize <= maxSize )
            {
                return 0.99;
            }

            return Math.Max( 0.65, maxSize / this.FileSize );
        }
    }
}
