using He.Framework.Attribute;
using He.Framework.Base;

namespace He.Framework.Extension.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigTag("RabbitMQ")]
    public class RabbitMQConfig : ConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
