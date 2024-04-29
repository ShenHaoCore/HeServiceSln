using He.Framework.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace He.Framework.Extension.Cors
{
    /// <summary>
    /// 跨域扩展
    /// </summary>
    public static class CorsExtension
    {
        /// <summary>
        /// 添加CORS策略
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            bool isEnable = false; if (!isEnable) { return; }
            var setting = AppSettings.GetObject<CorsConfig>();
            ArgumentNullException.ThrowIfNull(setting);
            if (!setting.Enable) { return; }

            string[] origins = setting.Origins is null ? [] : [.. setting.Origins];
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
                options.AddPolicy("WithOrigins", builder => { builder.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        /// <summary>
        /// 使用CORS策略
        /// </summary>
        /// <param name="app"></param>
        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);
            bool isEnable = false; if (!isEnable) { return; }
            var setting = AppSettings.GetObject<CorsConfig>();
            ArgumentNullException.ThrowIfNull(setting);
        }
    }
}
