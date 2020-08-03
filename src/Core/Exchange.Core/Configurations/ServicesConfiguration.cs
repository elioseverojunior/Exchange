﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Exchange.Core.Configurations
{
    /// <summary>
    /// Services Configurations
    /// </summary>
    public static class ServicesConfiguration
    {

        /// <summary>
        /// Add Middleware
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        /// <summary>
        /// Add Logging
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Trace)
            );
            return services;
        }
        
        /// <summary>
        /// Add Json AppSettings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(_ => configuration.GetSection("AppSettings").Bind(_));
            return services;
        }
    }
}