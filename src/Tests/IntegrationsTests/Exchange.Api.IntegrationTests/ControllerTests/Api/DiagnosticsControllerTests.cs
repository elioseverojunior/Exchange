using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Exchange.Api.IntegrationTests.TestStartup;
using Xunit;
using Xunit.Abstractions;

namespace Exchange.Api.IntegrationTests.ControllerTests.Api
{
    public class DiagnosticsControllerTests : IClassFixture<ApiStartupTestFixture>, IDisposable
    {
        private readonly ApiStartupTestFixture _fixture;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public DiagnosticsControllerTests(ApiStartupTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
            _testOutputHelper = output;
        }

        public void Dispose() => _fixture.Output = null;

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[]
                {
                    "v1", "diagnostics/version", @"{""version"":""1.0.0.0""}"
                },
                new object[]
                {
                    "v1", "diagnostics/appinfo",
                    @"{""title"":""Exchange.Api"",""version"":""1.0.0.0"",""description"":""Exchange API"",""product"":""Exchange.Api"",""copyright"":""© All Rights Reserved"",""company"":""Elio Severo Junior"",""informationalVersion"":""1.0.0.0""}"
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task Should_Get_Response_From_ApiVersion_In_DiagnosticsController(string apiVersion,
            string controller, string expected)
        {
            var response = await _client.GetAsync($"/api/{apiVersion}/{controller}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine($"Expected is: {expected} and we got {result}");
            Assert.Equal(expected, result);
        }
    }
}