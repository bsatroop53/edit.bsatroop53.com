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

using System.Text.RegularExpressions;

namespace BsaTroop53Editor.Web
{
    public static class StringExtensions
    {
        // ---------------- Fields ----------------

        private static readonly Regex whitespaceRegex = new Regex(
            @"\s+",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture
        );

        // ---------------- Functions ----------------

        public static string ToSnakeCase( this string str )
        {
            str = str.ToLower();
            str = whitespaceRegex.Replace( str, "_" );

            return str;
        }
    }
}
