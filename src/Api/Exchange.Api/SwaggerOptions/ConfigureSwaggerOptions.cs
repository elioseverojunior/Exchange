using System.Diagnostics.CodeAnalysis;
using Exchange.Api.Properties;
using Exchange.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exchange.Api.SwaggerOptions
{
    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private static IApplicationInfo AppInfo => new ApplicationInfo();
        
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = AppInfo.Title,
                Version = description.ApiVersion.ToString(),
                Description = AppInfo.Description,
                Contact = new OpenApiContact
                {
                    Name = "Elio Severo Junior",
                    Email = "elioseverojunior@gmail.com"
                }
            };

            if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

            return info;
        }
    }
}