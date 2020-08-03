using Exchange.Core.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exchange.Core.Contracts.Segments
{
    /// <summary>
    /// Segmentation Tax
    /// </summary>
    public class SegmentTax
    {
        /// <summary>
        /// Customer Segmentation
        /// </summary>
        [JsonProperty("Segment", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Segment Segment { get; set; }

        /// <summary>
        /// Tax of Segment
        /// </summary>
        [JsonProperty("Tax", Required = Required.Always)]
        public double Tax { get; set; }

        /// <summary>
        /// Default Segment Tax Constructor
        /// </summary>
        public SegmentTax()
        {
        }

        /// <summary>
        /// Deserialize Json to Object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static SegmentTax FromJson(string json) =>
            JsonConvert.DeserializeObject<SegmentTax>(json, Converter.Settings);
    }
}
