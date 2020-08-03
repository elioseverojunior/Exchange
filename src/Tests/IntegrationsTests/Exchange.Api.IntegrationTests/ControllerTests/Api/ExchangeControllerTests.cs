using System;
using System.Net.Http;
using Exchange.Api.IntegrationTests.TestStartup;
using Xunit;
using Xunit.Abstractions;

namespace Exchange.Api.IntegrationTests.ControllerTests.Api
{
    public class ExchangeControllerTests : IClassFixture<ApiStartupTestFixture>, IDisposable
    {
        private readonly ApiStartupTestFixture _fixture;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;
        
        public ExchangeControllerTests(ApiStartupTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _client = fixture.CreateClient();
            _testOutputHelper = output;
            fixture.Output = output;
        }
        
        public void Dispose() => _fixture.Output = null;
    }
}