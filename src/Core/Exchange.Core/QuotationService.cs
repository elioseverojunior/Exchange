using System.Globalization;
using Exchange.Core.Configurations;
using Exchange.Core.Contracts.ExchangeRates;
using Exchange.Core.Contracts.Quotations;
using Exchange.Core.Contracts.Segments;
using Exchange.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Exchange.Core
{
    /// <summary>
    /// Quotation Service
    /// </summary>
    public class QuotationService: IQuotationService
    {
        private readonly ILogger<IQuotationService> _logger;
        private IApiConfigurationSettings ExchangeSettings { get; }
        private IApiConfigurationSettings TaxSettings { get; }

        /// <summary>
        /// Customer Quotation Service
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="exchangeRateService"></param>
        /// <param name="segmentTaxService"></param>
        public QuotationService(ILogger<QuotationService> logger, 
            IConfiguration configuration, IExchangeRateService exchangeRateService, ISegmentTaxService segmentTaxService)
        {
            _logger = logger;
            ExchangeSettings = configuration.GetSection("ExchangeSettings").Get<ApiConfigurationSettings>();
            TaxSettings = configuration.GetSection("TaxSettings").Get<ApiConfigurationSettings>();
            SegmentTaxSvc = segmentTaxService;
            ExchangeRateSvc = exchangeRateService;
        }

        private IExchangeRateService ExchangeRateSvc { get; }

        private ISegmentTaxService SegmentTaxSvc { get; }

        /// <summary>
        /// Calculate Customer Quotation
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="segment"></param>
        /// <param name="amountToBy"></param>
        /// <returns></returns>
        public Quotation Calculate(ExchangeRate rates, SegmentTax segment, ulong amountToBy)
        {
            var quotation = new Quotation(rates, segment, amountToBy);
            quotation.Total = $"R$ {quotation.AmountToBuy * quotation.CurrencyCodeExchange * (1 + quotation.Segment.Tax):0.00}"
                .ToString(CultureInfo.CurrentCulture);
            return quotation;
        }
    }
}