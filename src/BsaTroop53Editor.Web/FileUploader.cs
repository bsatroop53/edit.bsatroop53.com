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
    public static class FileUploader
    {
        public async static Task<SubmissionResult> TryUpload( HttpClient client, string fileName, Stream stream )
        {
            #if DEBUG
            const string url = "http://localhost:9253/Upload";
            #else
            const string url = "https://upload.bsatroop53.com/Upload";
            #endif

            using var request = new HttpRequestMessage( HttpMethod.Post, url );
            request.Headers.UserAgent.Add( new ProductInfoHeaderValue( "T53_EDITOR_CLIENT", VersionInfo.Version ) );

            using var content = new MultipartFormDataContent();
            using var streamContent = new StreamContent( stream );
            content.Add( streamContent, "File", fileName );
            request.Content = content;

            HttpResponseMessage response = await client.SendAsync( request );
            string message = await response.Content.ReadAsStringAsync();

            return new SubmissionResult( response.IsSuccessStatusCode, message );
        }
    }
}
