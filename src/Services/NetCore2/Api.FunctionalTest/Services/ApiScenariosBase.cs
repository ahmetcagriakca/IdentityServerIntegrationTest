using System.IO;
using System.Reflection;
using Api.FunctionalTest.Middleware;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Api.FunctionalTest.Services
{
    public class ApiScenariosBase
    {
        private const string ApiUrlBase = "Identity";


        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(ApiTestsStartup))
                        .Location;
            return new TestServer(Api.Program.CreateWebHostBuilder<Api.ApiTestsStartup<AutoAuthorizeMiddleware>>(new string[] { }).UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("Services/appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                }));
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
