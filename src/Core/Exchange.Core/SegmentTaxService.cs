using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Exchange.Core.Configurations;
using Exchange.Core.Contracts.Segments;
using Exchange.Core.Extensions;
using Exchange.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Exchange.Core
{
    /// <summary>
    /// Segment Tax Service
    /// </summary>
    public class SegmentTaxService : ISegmentTaxService
    {
        private readonly ILogger<SegmentTaxService> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor with HttpClient
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SegmentTaxService(HttpClient httpClient, IConfiguration configuration, ILogger<SegmentTaxService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            Settings = configuration.GetSection("TaxSettings").Get<ApiConfigurationSettings>();
        }

        private IApiConfigurationSettings Settings { get; }

        /// <summary>
        /// Get Customer Segment Tax
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public async Task<SegmentTax> GetCustomerSegmentTax(Segment segment)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<SegmentTax>(await _httpClient
                    .RequestAsync(HttpMethod.Get, Settings, string.Format(Settings.RequestUri, segment))
                    .Result
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync());
                    
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{DateTime.Now:O}|{e.Message}|{e.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Get All Customer Segment Tax
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<SegmentTax> GetAllCustomerSegmentTax()
        {
            throw new NotImplementedException();
        }
    }
}