using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Exchange.Api.IntegrationTests.TestStartup;
using Xunit;
using Xunit.Abstractions;

namespace Exchange.Api.IntegrationTests.ControllerTests.Mock
{
    public class TaxControllerTests : IClassFixture<MockApiStartupTestFixture>, IDisposable
    {
        private readonly MockApiStartupTestFixture _fixture;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        public TaxControllerTests(MockApiStartupTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
            _testOutputHelper = output;
        }

        public void Dispose() => _fixture.Output = null;

        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> TestDataSuccess =>
            new List<object[]>
            {
                new object[]
                {
                    "v1", "tax/bysegment?segment=Varejo", @"{""Segment"":""Varejo"",""Tax"":0.7}"
                },
                new object[]
                {
                    "v1", "tax/bysegment?segment=Personnalite", @"{""Segment"":""Personnalite"",""Tax"":0.6}"
                },
                new object[]
                {
                    "v1", "tax/bysegment?segment=Private", @"{""Segment"":""Private"",""Tax"":0.5}"
                }
            };

        [Theory]
        [MemberData(nameof(TestDataSuccess))]
        public async Task Should_Get_Response_From_MockApi_In_TaxController(string apiVersion,
            string controller, string expected)
        {
            var result = await GetAsyncResult(apiVersion, controller, expected);
            Assert.Equal(expected, result);
        }
        
        [ExcludeFromCodeCoverage]
        public static IEnumerable<object[]> TestDataBadRequest =>
            new List<object[]>
            {
                new object[]
                {
                    "v1", "tax/bysegment?segment=Varejos", "Response status code does not indicate success: 400 (Bad Request)."
                }
            };

        [Theory]
        [MemberData(nameof(TestDataBadRequest))]
        public async Task Should_Get_BadResponse_From_MockApi_In_TaxController(string apiVersion,
            string controller, string expected)
        {
            try
            {
                await GetAsyncResult(apiVersion, controller, expected);
            }
            catch (Exception e)
            {
                Assert.Equal(expected, e.Message);
            }
        }

        private async Task<string> GetAsyncResult(string apiVersion, string controller, string expected)
        {
            try
            {
                var response = await _client.GetAsync($"/api/{apiVersion}/{controller}");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                _testOutputHelper.WriteLine($"Expected is: {expected} and we got {result}");
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}