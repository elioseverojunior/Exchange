using Exchange.Core.Interfaces;
using Newtonsoft.Json;

namespace Exchange.Core.Configurations
{
    /// <summary>
    /// Api Configurations Settings Implementation
    /// </summary>
    public class ApiConfigurationSettings : IApiConfigurationSettings
    {
        /// <summary>
        /// Base Api Url
        /// </summary>
        [JsonProperty("BaseUrl", Required = Required.Always)]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Request Uri
        /// </summary>
        [JsonProperty("RequestUri", Required = Required.Always)]
        public string RequestUri { get; set; }

        /// <summary>
        /// Timeout In MilliSeconds
        /// </summary>
        [JsonProperty("TimeoutInMS", Required = Required.Always)]
        public long TimeoutInMs { get; set; }

        /// <summary>
        /// Liveness
        /// </summary>
        [JsonProperty("Liveness")]
        public string Liveness { get; set; }

        /// <summary>
        /// Readiness
        /// </summary>
        [JsonProperty("Readiness")]
        public string Readiness { get; set; }
    }
}