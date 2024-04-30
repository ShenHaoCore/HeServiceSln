using System.Text.Json.Serialization;

namespace He.WeChat.WeChatPay.V3.Model
{
    /// <summary>
    /// 平台证书信息
    /// </summary>
    public class Certificate
    {
        /// <summary>
        /// 序列号
        /// </summary>
        [JsonPropertyName("serial_no")]
        public string SerialNo { get; set; } = string.Empty;

        /// <summary>
        /// 生效时间
        /// </summary>
        [JsonPropertyName("effective_time")]
        public DateTime EffectiveTime { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        [JsonPropertyName("expire_time")]
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 加密证书
        /// </summary>
        [JsonPropertyName("encrypt_certificate")]
        public EncryptCertificate EncryptCertificate { get; set; } = new();
    }
}
