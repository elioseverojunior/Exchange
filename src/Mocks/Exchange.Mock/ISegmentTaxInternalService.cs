using System.Collections.Generic;
using Exchange.Core.Contracts.Segments;

namespace Exchange.Mock
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISegmentTaxInternalService
    {
        /// <summary>
        /// Get Customer Segment Tax
        /// </summary>
        /// <param name="segment"></param>
        SegmentTax GetCustomerSegmentTax(Segment segment);

        /// <summary>
        /// Get All Customer Segment Tax
        /// </summary>
        /// <returns></returns>
        IList<SegmentTax> GetAllCustomerSegmentTax();
    }
}