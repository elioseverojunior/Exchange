using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Exchange.Core.Configurations;
using Exchange.Core.Contracts.ExchangeRates;
using Exchange.Core.Extensions;
using Exchange.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Exchange.Core
{
    /// <summary>
    /// Exchange Rate Service
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {
        #region Fields

        private readonly HttpClient _httpClient;
        private readonly ILogger<ExchangeRateService> _logger;

        #endregion

        #region Properties

        #region Private

        private IApiConfigurationSettings ExchangeSettings { get; }

        #endregion
        

        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public ExchangeRateService(HttpClient httpClient, ILogger<ExchangeRateService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            ExchangeSettings = configuration.GetSection("ExchangeSettings").Get<ApiConfigurationSettings>();
        }

        /// <summary>
        /// Get Latest Rates
        /// </summary>
        /// <returns></returns>
        public async Task<ExchangeRate> GetLatestRates()
        {
            try
            {
                return JsonConvert.DeserializeObject<ExchangeRate>(await _httpClient
                    .RequestAsync(ExchangeSettings.RequestUri)
                    .Result
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync());
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}|{e.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Get Exchange Rage by Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ExchangeRate> GetExchangeRateByDate(string date)
        {
            try
            {
                IFormatProvider culture = new CultureInfo("en-US", true); 
                var dateValParsed = DateTime.ParseExact(date, "yyyy-MM-dd", culture);
                return JsonConvert.DeserializeObject<ExchangeRate>(await _httpClient
                    .RequestAsync(HttpMethod.Get, ExchangeSettings, $"{dateValParsed}?base=USD")
                    .Result
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync());
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}|{e.StackTrace}");
                throw;
            }
        }
    }
}