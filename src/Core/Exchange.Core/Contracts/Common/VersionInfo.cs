using Exchange.Core.Interfaces;

namespace Exchange.Core.Contracts.Common
{
    /// <summary>
    /// Provides Information about Version
    /// </summary>
    public class VersionInfo
    {
        private static IApplicationInfo AppInfo => new ApplicationInfoBase();

        /// <summary>
        /// Current Application Version
        /// </summary>
        public string Version => AppInfo.Version;
    }
}