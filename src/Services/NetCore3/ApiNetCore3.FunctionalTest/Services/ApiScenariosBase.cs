using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ApiNetCore3;
using ApiNetCore3.FunctionalTest.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ApiNetCore3.FunctionalTest.Services
{
    public class ApiScenariosBase
    {
        private const string ApiUrlBase = "Identity";


        public async  Task<TestServer> CreateServer()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Services/appsettings.json", optional: true)
                .Build();
            var host =
                await new HostBuilder()
                    .ConfigureWebHost(webBuilder =>
                    {
                        webBuilder
                            .UseTestServer()
                            .Configure(app => { })
                            .UseConfiguration(configurationRoot)
                            .UseStartup<ApiNetCore3.ApiTestsStartup<AutoAuthorizeMiddleware>>();
                    })
                    .StartAsync();
            var server = host.GetTestServer();
            return server;
        }

        public static class Get
        {
            public static string Identity = $"{ApiUrlBase}";
        }

        public static class Post
        {
            public static string CreateBasket = $"{ApiUrlBase}/";
            public static string CheckoutOrder = $"{ApiUrlBase}/checkout";
        }
    }
}
