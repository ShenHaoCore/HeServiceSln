using Asp.Versioning.ApiExplorer;
using He.Framework.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace He.Framework.Extension.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    /// <param name="provider"></param>
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider = provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            var setting = AppSettings.GetObject<ServiceConfig>();
            ArgumentNullException.ThrowIfNull(setting);

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                {
                    Title = $"{setting.PrefixName} API",
                    Version = description.ApiVersion.ToString(),
                    Description = $"{setting.PrefixName} {description.ApiVersion} 版本"
                });
            }
        }
    }
}
