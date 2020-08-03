using System;
using System.Collections.Generic;
using Exchange.Core.Helpers;
using Newtonsoft.Json;

namespace Exchange.Core.Contracts.ExchangeRates
{
    /// <summary>
    /// Exchange Rates
    /// </summary>
    public class ExchangeRate
    {
        /// <summary>
        /// Rates getted from https://exchangeratesapi.io
        /// </summary>
        [JsonProperty("rates", Required = Required.Always)]
        public Dictionary<string, double> Rates { get; set; }

        /// <summary>
        /// Currency Base used on Request
        /// </summary>
        [JsonProperty("base", Required = Required.Always)]
        public string Base { get; set; }

        /// <summary>
        /// Date of Exchange Rate
        /// </summary>
        [JsonProperty("date", Required = Required.Always)]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Transform Json to this Object
        /// </summary>
        /// <param name="json">Json String</param>
        /// <returns></returns>
        public static ExchangeRate FromJson(string json) =>
            JsonConvert.DeserializeObject<ExchangeRate>(json, Converter.Settings);
    }
}