using System;

namespace Exchange.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationInfo
    {
        /// <summary>
        /// Gets the title property
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the application's version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the description about the application.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///  Gets the product's full name.
        /// </summary>
        string Product { get; }

        /// <summary>
        /// Gets the copyright information for the product.
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// Gets the company information for the product.
        /// </summary>
        string Company { get; }

        /// <summary>
        /// Gets the informational version for the product.
        /// </summary>
        string InformationalVersion { get; }

        /// <summary>
        /// Get AssemblyInfo Attribute
        /// </summary>
        /// <param name="resolveFunc"></param>
        /// <param name="defaultResult"></param>
        /// <typeparam name="TAttr"></typeparam>
        /// <returns></returns>
        string GetAttributeValue<TAttr>(Func<TAttr,
            string> resolveFunc, string defaultResult = null) where TAttr : Attribute;
    }
}