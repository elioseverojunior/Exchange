using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Exchange.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Exchange.Api.Controllers.v1
{
    /// <summary>
    /// Exchange Rates
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/exchange")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly ILogger<ExchangeRatesController> _logger;
        private IExchangeRateService ExchangeRateSvc { get; }

        public ExchangeRatesController(ILogger<ExchangeRatesController> logger,
            IExchangeRateService exchangeRateService)
        {
            _logger = logger;
            ExchangeRateSvc = exchangeRateService;
        }
        
        /// <summary>
        /// Get Latest Exchange Rate
        /// </summary>
        /// <returns></returns>
        [HttpGet("latest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Get All Available Rates by Currency Codes")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLatestExchangeRate()
        {
            try
            {
                return Ok(await ExchangeRateSvc.GetLatestRates());
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, $"{e.Message}|{e.StackTrace}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Exchange Rate From Given Date
        /// </summary>
        /// <param name="date">Date to Get Exchange Quotation</param>
        /// <returns></returns>
        [HttpGet("all/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExchangeRateByDate(string date)
        {
            try
            {
                return Ok(await ExchangeRateSvc.GetExchangeRateByDate(date));
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}|{e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
    }
}