using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ApiNetCore3.FunctionalTest.Services
{
    public class FunctionalTestScrenarios
    {
        [Fact]
        public async Task Identity_GetClaims()
        {
            using var apiServer = await new ApiScenariosBase().CreateServer();
            var apiClient = apiServer.CreateClient();
            var response = await apiClient.GetAsync("Identity");
            var result = await response.Content.ReadAsStringAsync();
            result.Should().NotBeNullOrEmpty($"Response must not be empty. StatusCode:{response.StatusCode}-ErrorDetail:{result}");
        }
        [Fact]
        public async Task Identity_GetTestResult()
        {
            using var apiServer = await new ApiScenariosBase().CreateServer();
            var apiClient = apiServer.CreateClient();
            var response = await apiClient.GetAsync("Identity/GetTestResult");
            var result = await response.Content.ReadAsStringAsync();
            result.Should().NotBeNullOrEmpty($"Response must not be empty. StatusCode:{response.StatusCode}-ErrorDetail:{result}");
        }
    }
}
