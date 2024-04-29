using He.Framework.Attribute;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace He.Framework.Common
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration? Config { get; set; }

        /// <summary>
        /// 配置帮助类
        /// </summary>
        /// <param name="configuration"></param>
        public AppSettings(IConfiguration configuration)
        {
            Config = configuration;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            ArgumentNullException.ThrowIfNull(Config);
            return Config.GetConnectionString(key) ?? string.Empty;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetObject<T>(params string[] key) where T : class
        {
            ArgumentNullException.ThrowIfNull(Config);
            return Config.GetSection(string.Join(":", key)).Get<T>();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? GetObject<T>() where T : class
        {
            ArgumentNullException.ThrowIfNull(Config);
            var tag = typeof(T).GetCustomAttribute<ConfigTagAttribute>();
            ArgumentNullException.ThrowIfNull(tag);
            return Config.GetSection(tag.Key).Get<T>();
        }
    }
}
