using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.FunctionalTest.Services;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;
using Api.FunctionalTest.Services;

namespace Api.FunctionalTest
{
    public class FunctionalTestScrenarios
    {
        [Fact]
        public async Task Identity_GetClaims()
        {
            using (var apiServer = new ApiScenariosBase().CreateServer())
            {
                var apiClient = apiServer.CreateClient();
                var response = await apiClient.GetAsync("Identity");
                var result = await response.Content.ReadAsStringAsync();
                result.Should().NotBeNullOrEmpty($"Response must not be empty. StatusCode:{response.StatusCode}-ErrorDetail:{result}" );
            }
        }
        [Fact]
        public async Task Identity_GetTestResult()
        {
            using (var apiServer = new ApiScenariosBase().CreateServer())
            {
                var apiClient = apiServer.CreateClient();
                var response = await apiClient.GetAsync("Identity/GetTestResult");
                var result = await response.Content.ReadAsStringAsync();
                result.Should().NotBeNullOrEmpty($"Response must not be empty. StatusCode:{response.StatusCode}-ErrorDetail:{result}");
            }
        }
    }
}
