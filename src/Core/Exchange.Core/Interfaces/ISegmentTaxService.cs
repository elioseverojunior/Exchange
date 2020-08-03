using System.Collections.Generic;
using System.Threading.Tasks;
using Exchange.Core.Contracts.Segments;

namespace Exchange.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISegmentTaxService
    {
        /// <summary>
        /// Get Customer Segment Tax
        /// </summary>
        /// <param name="segment"></param>
        Task<SegmentTax> GetCustomerSegmentTax(Segment segment);

        /// <summary>
        /// Get All Customer Segment Tax
        /// </summary>
        /// <returns></returns>
        IList<SegmentTax> GetAllCustomerSegmentTax();
    }
}