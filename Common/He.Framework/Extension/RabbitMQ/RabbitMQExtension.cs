using He.Framework.Common;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace He.Framework.Extension.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    public static class RabbitMQExtension
    {
        /// <summary>
        /// RabbitMQ服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddRabbitMQ(this IServiceCollection services)
        {
            bool isEnable = false; if (!isEnable) { return; }
            ArgumentNullException.ThrowIfNull(services);
            var setting = AppSettings.GetObject<RabbitMQConfig>();
            ArgumentNullException.ThrowIfNull(setting);

            ConnectionFactory factory = new ConnectionFactory { HostName = setting.HostName, Port = 5672, UserName = setting.UserName, Password = setting.Password };
            services.AddSingleton(provider => factory.CreateConnection());
            services.AddTransient<IRabbitMQManage, RabbitMQManage>();
        }
    }
}
