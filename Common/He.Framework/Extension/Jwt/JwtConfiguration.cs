using He.Framework.Attribute;
using He.Framework.Base;

namespace He.Framework.Extension.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigTag("Redis")]
    public class JwtConfig : ConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SecretKey { get; set; } = "9U2+YE/r+BwMNnn3UFrQz8Iye/DEd4ZLsCmNQ7JeGSM=";

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; } = "";
    }
}
