using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Exchange.Api.Controllers.v1;
using Exchange.Core.Contracts.ExchangeRates;
using Exchange.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Exchange.UnitTests.Controller
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRatesControllerTest
    {
        private readonly XunitLogger<ExchangeRatesController> _logger;
        private readonly IExchangeRateService _exchangeRateService = new ExchangeRateServiceFake();
        private ExchangeRatesController _exchangeRatesController;
        
        public ExchangeRatesControllerTest(ITestOutputHelper output)
        {
            _logger = new XunitLogger<ExchangeRatesController>(output);
            _exchangeRatesController = new ExchangeRatesController(_logger, _exchangeRateService);
        }
        
        [Fact]
        public async Task Given_ExchangeRatesController_GetLatestExchangeRate_OkObjectResult()
        {
            var actionResult = await _exchangeRatesController.GetLatestExchangeRate();
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as ExchangeRate;
            Assert.NotNull(model);

            var actual = model.Rates["BRL"];
            Assert.Equal(5.1670324105000001, actual);
        }

        [Fact]
        public async Task Given_ExchangeRatesController_GetLatestExchangeRate_BadRequestObjectResult()
        {
            _exchangeRatesController = new ExchangeRatesController(_logger, null);
            var actionResult = await _exchangeRatesController.GetLatestExchangeRate();
            
            var badRequestObjectResult = actionResult as BadRequestObjectResult;
            if (badRequestObjectResult != null)
            {
                var statusCode = badRequestObjectResult.StatusCode;
                Assert.Equal(400, statusCode);
            }

            Assert.IsType<BadRequestObjectResult>(badRequestObjectResult);
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ExchangeRateServiceFake : IExchangeRateService
    {
        private const string Rates = @"{
  ""rates"": {
        ""CAD"": 1.3418298447,
        ""HKD"": 7.7502532073,
        ""ISK"": 135.043889264,
        ""PHP"": 49.0580688724,
        ""DKK"": 6.2830857529,
        ""HUF"": 291.1461850101,
        ""CZK"": 22.0923362593,
        ""GBP"": 0.76006921,
        ""RON"": 4.0779034436,
        ""SEK"": 8.6795239703,
        ""IDR"": 14655.6380823768,
        ""INR"": 74.8105165429,
        ""BRL"": 5.1670324105,
        ""RUB"": 74.0124915598,
        ""HRK"": 6.3145678596,
        ""JPY"": 104.9206617151,
        ""THB"": 31.1850101283,
        ""CHF"": 0.9089297772,
        ""EUR"": 0.8440243079,
        ""MYR"": 4.2395340986,
        ""BGN"": 1.6507427414,
        ""TRY"": 6.9712187711,
        ""CNY"": 6.9747636732,
        ""NOK"": 9.0583220797,
        ""NZD"": 1.4999155976,
        ""ZAR"": 16.9615124916,
        ""USD"": 1,
        ""MXN"": 22.179270763,
        ""SGD"": 1.3711174882,
        ""AUD"": 1.3916272789,
        ""ILS"": 3.40243079,
        ""KRW"": 1189.7029034436,
        ""PLN"": 3.7165766374
    },
    ""base"": ""USD"",
    ""date"": ""2020-07-31T00:00:00-03:00""
}";

        public async Task<ExchangeRate> GetLatestRates()
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<ExchangeRate>(Rates));
        }

        public Task<ExchangeRate> GetExchangeRateByDate(string date)
        {
            throw new NotImplementedException();
        }
    }
}