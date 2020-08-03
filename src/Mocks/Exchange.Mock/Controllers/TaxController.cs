using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Exchange.Core.Contracts.Segments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Exchange.Mock.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/tax")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TaxController : ControllerBase
    {
        private static IList<SegmentTax> SegmentationTax { get; set; }
        private ISegmentTaxInternalService SegmentTaxSvc { get; }
        private readonly ILogger<TaxController> _logger;

        public TaxController(IConfiguration configuration, ILogger<TaxController> logger, ISegmentTaxInternalService segmentTaxService)
        {
            _logger = logger;
            SegmentationTax = configuration.GetSection("SegmentTax").Get<List<SegmentTax>>();
            SegmentTaxSvc = segmentTaxService;
        }
        
        /// <summary>
        /// Get Customer Segment Tax
        /// </summary>
        /// <param name="segment">Customer Segment</param>
        /// <returns>
        /// Returns an SegmentationTax json type
        /// </returns>
        /// <seealso cref="SegmentationTax">
        /// Notice the use of the cref attribute to reference a specific class.
        /// </seealso>
        [HttpGet("bysegment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCustomerSegmentTax([FromQuery]Segment segment)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now:O}|Getting Tax of {nameof(Segment)} \"{segment}\"");
                var result = SegmentTaxSvc.GetCustomerSegmentTax(segment);
                _logger.LogInformation($"{DateTime.Now:O}|Tax of {nameof(Segment)} \"{result.Segment}\" is {result.Tax}");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}|{e.StackTrace}");
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Get All Customer's Segment and Taxes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllCustomerSegmentTax()
        {
            try
            {
                return Ok(SegmentationTax.ToList());
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
