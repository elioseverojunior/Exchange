using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exchange.Core.Contracts.Segments
{
    /// <summary>
    /// Customer Segmentation
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Segment
    {
        /// <summary>
        /// Segmento Varejo
        /// </summary>
        [Description("Varejo")]
        Varejo = 0,

        /// <summary>
        /// Segmento Personnalite
        /// </summary>
        [Description("Personnalite")]
        Personnalite = 1,

        /// <summary>
        /// Segmento Private
        /// </summary>
        [Description("Private")]
        Private = 2
    }
}