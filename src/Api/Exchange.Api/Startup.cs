using System;
using System.IO;
using System.Reflection;
using Exchange.Api.SwaggerOptions;
using Exchange.Core;
using Exchange.Core.Configurations;
using Exchange.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Exchange.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ExchangeSettings = Configuration.GetSection("ExchangeSettings").Get<ApiConfigurationSettings>();
            TaxSettings = Configuration.GetSection("TaxSettings").Get<ApiConfigurationSettings>();
        }

        private IApiConfigurationSettings ExchangeSettings { get; }
        private IApiConfigurationSettings TaxSettings { get; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options => 
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            
            services.AddOptions();
            
            services.AddMiddleware()
                .AddAppSettings(Configuration);
            
            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });
            
            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>(_ =>
                {
                    _.BaseAddress = new Uri(ExchangeSettings.BaseUrl);
                })
                .SetHandlerLifetime(TimeSpan.FromMilliseconds(ExchangeSettings.TimeoutInMs));
            services.AddHttpClient<ISegmentTaxService, SegmentTaxService>(_ =>
                {
                    _.BaseAddress = new Uri(TaxSettings.BaseUrl);
                })
                .SetHandlerLifetime(TimeSpan.FromMilliseconds(TaxSettings.TimeoutInMs));
            services.AddScoped<IApiConfigurationSettings, ApiConfigurationSettings>();
            services.AddScoped<IQuotationService, QuotationService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                options.DocExpansion(DocExpansion.List);
            });

            app.UseRouting();
        }
    }
}
