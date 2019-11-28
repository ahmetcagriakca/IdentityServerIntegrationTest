using ApiNetCore3.FunctionalTest.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace ApiNetCore3.FunctionalTest.Services
{
    class ApiTestsStartup : ApiNetCore3.Startup
    {
        public ApiTestsStartup(IConfiguration env) : base(env)
        {
        }

        protected override IApplicationBuilder ConfigureAuth(IApplicationBuilder app)
        {
            if (Configuration["isTest"] == bool.TrueString.ToLowerInvariant())
            {
                return app.UseMiddleware<AutoAuthorizeMiddleware>();
            }
            else
            {
                return base.ConfigureAuth(app);
            }
        }
    }
}
