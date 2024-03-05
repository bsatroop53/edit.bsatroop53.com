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

using Microsoft.AspNetCore.Components;

namespace BsaTroop53Editor.Web
{
    internal static partial class MarkdownContent
    {
        // ---------------- Fields ----------------

        private static readonly string licenseHtml = "";

        private static readonly string creditsHtml = "";

        private static readonly string homeHtml = "";

        private static readonly string faqHtml = "";

        private static readonly string privacyHtml = "";

        private static readonly string releaseNotesHtml = "";

        // ---------------- Constructor ----------------

        // Constructor is source-code generated.

        // ---------------- Properties ----------------

        public static MarkupString LicenceString => (MarkupString)licenseHtml;

        public static MarkupString CreditsString => (MarkupString)creditsHtml;

        public static MarkupString HomePageString => (MarkupString)homeHtml;

        public static MarkupString FaqString => (MarkupString)faqHtml;

        public static MarkupString PrivacyString => (MarkupString)privacyHtml;

        public static MarkupString ReleaseNotesString => (MarkupString)releaseNotesHtml;
    }
}
