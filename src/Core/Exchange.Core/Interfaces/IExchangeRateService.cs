using System.Threading.Tasks;
using Exchange.Core.Contracts.ExchangeRates;

namespace Exchange.Core.Interfaces
{
    /// <summary>
    /// Exchange Rate Service Interface
    /// </summary>
    public interface IExchangeRateService
    {
        /// <summary>
        /// Get Latest Rates
        /// </summary>
        /// <returns></returns>
        Task<ExchangeRate> GetLatestRates();
        
        /// <summary>
        /// Get Exchange Rate By Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<ExchangeRate> GetExchangeRateByDate(string date);
    }
}