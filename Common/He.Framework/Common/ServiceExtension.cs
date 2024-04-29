using Asp.Versioning;
using AspNetCoreRateLimit;
using He.Framework.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace He.Framework.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="prefixName">前缀</param>
        public static void AddController(this IServiceCollection services, string prefixName)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddControllers(option =>
            {
                option.Filters.Add<GlobalExceptionFilter>();
                option.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(prefixName)));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();     // 序列化时KEY为驼峰样式
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;                // 忽略循环引用
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";                            // 时间格式化
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });
        }

        /// <summary>
        /// 接口版本
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiVersion(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);       // 默认版本
                options.AssumeDefaultVersionWhenUnspecified = true;     // 如果没有指定版本用默认配置
                options.ReportApiVersions = true;                       // Response Header 指定可用版本
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-API-VERSION"),
                    new MediaTypeApiVersionReader("VER")
                );
            }).AddMvc().AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        /// <summary>
        /// 接口限速
        /// </summary>
        /// <param name="services"></param>
        public static void AddRateLimiting(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(AppSettings.Config);
            services.AddOptions();                                                                      // 添加配置选项服务
            services.AddMemoryCache();                                                                  // 添加内存缓存服务
            services.Configure<IpRateLimitOptions>(AppSettings.Config.GetSection("IpRateLimiting"));    // 配置IP限速选项，使用名为 "IpRateLimiting" 的配置节
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();                          // 注册单例模式的 IIpPolicyStore 服务，使用内存缓存实现
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();          // 注册单例模式的 IRateLimitCounterStore 服务，使用内存缓存实现
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();               // 注册单例模式的 IProcessingStrategy 服务，使用异步键锁处理策略实现
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();                   // 注册单例模式的 IRateLimitConfiguration 服务，使用 RateLimitConfiguration 实现
        }
    }
}
