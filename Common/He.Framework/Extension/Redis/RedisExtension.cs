using He.Framework.Common;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace He.Framework.Extension.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public static class RedisExtension
    {
        /// <summary>
        /// Redis服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddRedis(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            var setting = AppSettings.GetObject<RedisConfig>();
            ArgumentNullException.ThrowIfNull(setting);

            ConfigurationOptions config = new ConfigurationOptions { ClientName = setting.Name, Password = setting.Password, ConnectTimeout = setting.Timeout };
            setting.EndPoints.ForEach(P => { config.EndPoints.Add(P.Host, P.Port); });
            config.ResolveDns = true;

            services.AddSingleton<IConnectionMultiplexer>(P => ConnectionMultiplexer.Connect(config));
            services.AddTransient<IRedisManage, RedisManage>();
        }
    }
}
