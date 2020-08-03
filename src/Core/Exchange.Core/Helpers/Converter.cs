using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exchange.Core.Helpers
{
    /// <summary>
    /// Json Converter Helper
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Json Serializer Settings
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal },
                new StringEnumConverter { AllowIntegerValues = true }
            },
        };
    }
    
}