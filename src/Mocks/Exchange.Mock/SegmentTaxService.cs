using System;
using System.Collections.Generic;
using System.Linq;
using Exchange.Core.Contracts.Segments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Exchange.Mock
{
    /// <summary>
    /// 
    /// </summary>
    public class SegmentTaxInternalService : ISegmentTaxInternalService
    {
        private readonly ILogger<SegmentTaxInternalService> _logger;
        private IList<SegmentTax> SegmentationTax { get; }

        /// <summary>
        /// Constructor Without HttpClient
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SegmentTaxInternalService(IConfiguration configuration, ILogger<SegmentTaxInternalService> logger)
        {
            SegmentationTax = configuration.GetSection("SegmentTaxes").Get<IList<SegmentTax>>();
            _logger = logger;
        }

        /// <summary>
        /// Get Customer Segment Tax
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SegmentTax GetCustomerSegmentTax(Segment segment)
        {
            try
            {
                return SegmentationTax.FirstOrDefault(tax => tax.Segment == segment);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}|{e.StackTrace}");
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