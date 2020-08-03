// ReSharper disable UnusedMember.Global
#pragma warning disable 1591
// ReSharper disable InconsistentNaming
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exchange.Core.Contracts.ExchangeRates
{
    /// <summary>
    /// Global Currency Codes
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExchangeRateCurrencyCodes
    {
        /// <summary>
        /// USD - US Dollar US$
        /// </summary>
        [Description("USD")]
        USD,
        
        /// <summary>
        /// BRL - Brazilian Real R$
        /// </summary>
        [Description("BRL")]
        BRL,
        
        /// <summary>
        /// USD - Australian Dollar US$
        /// </summary>
        [Description("AUD")]
        AUD,
        [Description("BGN")]
        BGN,
        [Description("CAD")]
        CAD,
        [Description("CHF")]
        CHF,
        [Description("CNY")]
        CNY,
        [Description("CZK")]
        CZK,
        [Description("DKK")]
        DKK,
        [Description("GBP")]
        GBP,
        [Description("HKD")]
        HKD,
        [Description("HRK")]
        HRK,
        [Description("HUF")]
        HUF,
        [Description("IDR")]
        IDR,
        [Description("ILS")]
        ILS,
        [Description("INR")]
        INR,
        [Description("ISK")]
        ISK,
        [Description("JPY")]
        JPY,
        [Description("KRW")]
        KRW,
        [Description("MXN")]
        MXN,
        [Description("MYR")]
        MYR,
        [Description("NOK")]
        NOK,
        [Description("NZD")]
        NZD,
        [Description("PHP")]
        PHP,
        [Description("PLN")]
        PLN,
        [Description("RON")]
        RON,
        [Description("RUB")]
        RUB,
        [Description("SEK")]
        SEK,
        [Description("SGD")]
        SGD,
        [Description("THB")]
        THB,
        [Description("TRY")]
        TRY,
        [Description("ZAR")]
        ZAR
    }
}