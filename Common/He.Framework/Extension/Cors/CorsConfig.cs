using He.Framework.Attribute;
using He.Framework.Base;

namespace He.Framework.Extension.Cors
{
    /// <summary>
    /// CORS配置
    /// </summary>
    [ConfigTag("Cors")]
    public class CorsConfig : ConfigModel
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 是否允许所有
        /// </summary>
        public bool AllowAnyone { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public List<string> Origins { get; set; } = [];
    }
}
