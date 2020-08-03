// ReSharper disable UnusedAutoPropertyAccessor.Local

using Exchange.Core.Contracts.ExchangeRates;
using Exchange.Core.Contracts.Segments;

namespace Exchange.Core.Contracts.Quotations
{
    /// <summary>
    /// Represents Request to Calculate Quotation
    /// </summary>
    public class Quotation
    {
        /// <summary>
        /// Local Currency Code
        /// </summary>
        private ExchangeRateCurrencyCodes LocalCurrencyCode { get; } 
            = ExchangeRateCurrencyCodes.BRL;
        
        /// <summary>
        /// Exchange Rates
        /// </summary>
        private ExchangeRate ExchangeRate { get; }

        /// <summary>
        /// Currency Code from target to buy
        /// </summary>
        public ExchangeRateCurrencyCodes CurrencyCode { get; private set; }
        
        /// <summary>
        /// Segment Tax Rate
        /// </summary>
        public SegmentTax Segment { get; private set; }
        
        /// <summary>
        /// Currency Code To Exchange
        /// </summary>
        public double CurrencyCodeExchange 
            => ExchangeRate.Rates[LocalCurrencyCode.ToString()];

        /// <summary>
        /// Amount in Currency BRL to Buy 
        /// </summary>
        public ulong AmountToBuy { get; set; }
        
        /// <summary>
        /// Total of Transaction
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Quotation
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="segment"></param>
        /// <param name="amountToBy"></param>
        public Quotation(ExchangeRate rates, SegmentTax segment, ulong amountToBy)
        {
            ExchangeRate = rates;
            Segment = segment;
            AmountToBuy = amountToBy;
        }

        /// <summary>
        /// Quotation
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="segment"></param>
        /// <param name="amountToBy"></param>
        /// <param name="total"></param>
        public Quotation(ExchangeRate rates, SegmentTax segment, ulong amountToBy, string total)
        {
            ExchangeRate = rates;
            Segment = segment;
            AmountToBuy = amountToBy;
            Total = total;
        }
    }
}