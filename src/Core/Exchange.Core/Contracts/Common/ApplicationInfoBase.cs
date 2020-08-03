using System;
using System.IO;
using System.Reflection;
using Exchange.Core.Interfaces;

namespace Exchange.Core.Contracts.Common
{
    /// <summary>
    /// Application Info
    /// </summary>
    public class ApplicationInfoBase : IApplicationInfo
    {
        /// <summary>
        /// Gets the title property
        /// </summary>
        public string Title => GetAttributeValue<AssemblyTitleAttribute>(a
            => a.Title, Path.GetFileNameWithoutExtension(GetType().Assembly.CodeBase));

        /// <summary>
        /// Gets the application's version
        /// </summary>
        public string Version => GetType().Assembly.GetName().Version?.ToString();

        /// <summary>
        /// Gets the description about the application.
        /// </summary>
        public string Description => GetAttributeValue<AssemblyDescriptionAttribute>(a
            => a.Description);

        /// <summary>
        ///  Gets the product's full name.
        /// </summary>
        public string Product => GetAttributeValue<AssemblyProductAttribute>(a
            => a.Product);

        /// <summary>
        /// Gets the copyright information for the product.
        /// </summary>
        public string Copyright => GetAttributeValue<AssemblyCopyrightAttribute>(a
            => a.Copyright);

        /// <summary>
        /// Gets the company information for the product.
        /// </summary>
        public string Company => GetAttributeValue<AssemblyCompanyAttribute>(a
            => a.Company);

        /// <summary>
        /// Gets the informational version for the product.
        /// </summary>
        public string InformationalVersion =>
            GetAttributeValue<AssemblyInformationalVersionAttribute>(a
                => a.InformationalVersion);

        /// <summary>
        /// Get AssemblyInfo Attribute
        /// </summary>
        /// <param name="resolveFunc"></param>
        /// <param name="defaultResult"></param>
        /// <typeparam name="TAttr"></typeparam>
        /// <returns></returns>
        public string GetAttributeValue<TAttr>(Func<TAttr,
            string> resolveFunc, string defaultResult = null) where TAttr : Attribute
        {
            var attributes = GetType().Assembly.GetCustomAttributes(typeof(TAttr), false);
            return attributes.Length > 0 ? resolveFunc((TAttr) attributes[0]) : defaultResult;
        }
    }
}