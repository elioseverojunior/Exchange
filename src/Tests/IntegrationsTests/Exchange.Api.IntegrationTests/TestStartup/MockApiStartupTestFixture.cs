using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Exchange.Api.IntegrationTests.TestStartup
{
    [ExcludeFromCodeCoverage]
    public class MockApiStartupTestFixture : 
        WebApplicationFactory<Mock.Program>
    {
        public ITestOutputHelper Output { get; set; }
        
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder =  base.CreateHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddXUnit(Output);
            });

            return builder;
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(IHostedService));
            });
        }
        
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = base.CreateWebHostBuilder();
            builder.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddXUnit(Output);
                })
                .UseStartup<Mock.Startup>();
        
            return builder;
        }
    }
}