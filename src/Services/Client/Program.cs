// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static async Task Main()
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            #region Discovery
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            #endregion

            #region Get Token 
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            #endregion

            #region Call Api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            #region NetCore2 identity
            Console.WriteLine("ApiCall Started");
            var responseApi = await apiClient.GetAsync("http://localhost:5022/identity");
            if (!responseApi.IsSuccessStatusCode)
            {
                Console.WriteLine(responseApi.StatusCode);
            }
            else
            {
                var content = await responseApi.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.WriteLine("ApiCall Finished");
            #endregion
            #region NetCore3 identity
            Console.WriteLine("ApiCall Started");
            var responseApiNetcore = await apiClient.GetAsync("http://localhost:5030/identity");
            if (!responseApiNetcore.IsSuccessStatusCode)
            {
                Console.WriteLine(responseApiNetcore.StatusCode);
            }
            else
            {
                var content = await responseApiNetcore.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.WriteLine("ApiCall Finished");
            #endregion
            #endregion
        }
    }
}