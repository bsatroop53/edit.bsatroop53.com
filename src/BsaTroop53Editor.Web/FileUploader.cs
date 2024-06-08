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

using System.Net.Http.Headers;

namespace BsaTroop53Editor.Web
{
    internal static class FileUploader
    {
        #if DEBUG
        internal const string ServerUrl = "http://localhost:9253";
        #else
        internal const string ServerUrl = "https://upload.bsatroop53.com";
        #endif

        private const string url = $"{ServerUrl}/Upload";

        public async static Task<SubmissionResult> TryUpload(
            HttpClient client,
            CodeGenerator codeGenerator,
            string fileName,
            Stream stream
        )
        {
            using var request = new HttpRequestMessage( HttpMethod.Post, url );
            request.Headers.UserAgent.Add( new ProductInfoHeaderValue( "T53_EDITOR_CLIENT", VersionInfo.Version ) );

            using var content = new MultipartFormDataContent();
            using var streamContent = new StreamContent( stream );
            content.Add( streamContent, "File", fileName );

            using var stringContent = new StringContent( codeGenerator.GetCode() );
            content.Add( stringContent, "Key" );

            request.Content = content;

            HttpResponseMessage response = await client.SendAsync( request );
            string message = await response.Content.ReadAsStringAsync();

            return new SubmissionResult( response.IsSuccessStatusCode, message );
        }
    }
}
