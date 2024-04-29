using He.Framework.Attribute;
using He.Framework.Base;

namespace He.Framework.Extension.Redis
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigTag("Redis")]
    public class RedisConfig : ConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public List<RedisEndPoint> EndPoints { get; set; } = [];

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 超时
        /// </summary>
        public int Timeout { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RedisEndPoint
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 6379;
    }
}
