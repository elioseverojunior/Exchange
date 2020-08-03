using Exchange.Core.Contracts.ExchangeRates;
using Exchange.Core.Contracts.Quotations;
using Exchange.Core.Contracts.Segments;

namespace Exchange.Core.Interfaces
{
    /// <summary>
    /// Interface of Quotation Service
    /// </summary>
    public interface IQuotationService
    {
        /// <summary>
        /// Calculate Quotation
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="segment"></param>
        /// <param name="amountToBy"></param>
        /// <returns></returns>
        Quotation Calculate(ExchangeRate rates, SegmentTax segment, ulong amountToBy);
    }
}