using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace He.Framework.Extension.Swagger
{
    /// <summary>
    /// Swagger启动服务
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Swagger注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="xmlNames"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle</remarks>
        public static void AddApiSwagger(this IServiceCollection services, List<string> xmlNames)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                xmlNames.ForEach(name => { if (File.Exists(Path.Combine(AppContext.BaseDirectory, name))) { options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, name), true); } });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }, new List<string>() } });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { Description = "JWT授权 Bearer {Token}（注意两者之间是一个空格）", Name = "Authorization", In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey });
                options.OperationFilter<SwaggerApiFilter>();
            });
        }

        /// <summary>
        /// 使用Swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseApiSwagger(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                app.DescribeApiVersions().ToList().ForEach(description => { options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant()); });
                options.DocExpansion(DocExpansion.None);    // 设置为 None 可折叠所有方法
                options.DefaultModelsExpandDepth(0);        // 设置为 -1 可不显示Models
                options.DisplayRequestDuration();           // 设置持续时间的显示（以毫秒为单位）
            });
        }
    }
}
