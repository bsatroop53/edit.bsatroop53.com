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

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BsaTroop53Editor.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

using var httpClient = new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) };
builder.Services.AddScoped(sp => httpClient);

DateTime? serverTime = await GetServerTime();
if( serverTime is null )
{
    Console.WriteLine( "Can not get server time, must use client time" );
}

builder.Services.AddScoped( sp => new CodeGenerator( serverTime ) );

await builder.Build().RunAsync();

async Task<DateTime?> GetServerTime()
{
    try
    {
        HttpResponseMessage response = await httpClient.GetAsync( $"{FileUploader.ServerUrl}/datetime.txt" );
        string responseString = await response.Content.ReadAsStringAsync();

        if( long.TryParse( responseString, out long ticks ) )
        {
            return new DateTime( ticks, DateTimeKind.Utc );
        }

        return null;
    }
    catch( Exception )
    {
        return null;
    }
}
