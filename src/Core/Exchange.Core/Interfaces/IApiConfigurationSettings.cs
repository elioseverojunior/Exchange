namespace Exchange.Core.Interfaces
{
    /// <summary>
    /// Api Configuration Settings
    /// <remarks>
    /// This Class is a Abstraction for
    /// Settings Sections in AppSettings 
    /// </remarks>
    /// </summary>
    public interface IApiConfigurationSettings
    {
        /// <summary>
        /// Base Api Url
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Request Uri
        /// </summary>
        string RequestUri { get; set; }

        /// <summary>
        /// TimeoutInMS
        /// </summary>
        long TimeoutInMs { get; set; }
    }
}