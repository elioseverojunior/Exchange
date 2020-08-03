using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Exchange.Core.Contracts.ExchangeRates;
using Exchange.Core.Contracts.Segments;
using Exchange.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Exchange.Api.Controllers.v1
{
    /// <summary>
    /// Quotation
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/quotation")]
    [Produces(MediaTypeNames.Application.Json)]
    public class QuotationController : ControllerBase
    {
        private readonly ILogger<ExchangeRatesController> _logger;
        private ISegmentTaxService SegmentTaxSvc { get; }
        private IExchangeRateService ExchangeRateSvc { get; }
        private IQuotationService QuotationSvc { get; }
        
        public QuotationController(ILogger<ExchangeRatesController> logger, 
            IQuotationService quotationService,
            IExchangeRateService exchangeRateService, 
            ISegmentTaxService segmentTaxService)
        {
            _logger = logger;
            QuotationSvc = quotationService;
            ExchangeRateSvc = exchangeRateService;
            SegmentTaxSvc = segmentTaxService;
        }

        /// <summary>
        /// Get Exchange Rate to Currency Code
        /// </summary>
        /// <param name="currencyCode">Target Currency Code to Calculate Quotation</param>
        /// <param name="segment">Customer Segment</param>
        /// <param name="amountToBy">Amount Required by Customer In Quotation</param>
        /// <returns></returns>
        [HttpPost("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Get All Available Rates by Currency Codes")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuotation(ExchangeRateCurrencyCodes currencyCode,
            Segment segment, ulong amountToBy)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now:u}|Starting Calculate Quotation");
                var result = QuotationSvc.Calculate(
                    await ExchangeRateSvc.GetLatestRates(),
                    await SegmentTaxSvc.GetCustomerSegmentTax(segment),
                    amountToBy);
                _logger.LogInformation($"{DateTime.Now:u}|Customer Quotation is {result.Total}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now:u}|{ex.Message}|{ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }
    }
}