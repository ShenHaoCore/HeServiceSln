using System.Text.Json.Serialization;

namespace He.WeChat.WeChatPay.V3.Model
{
    /// <summary>
    /// 加密证书信息
    /// </summary>
    public class EncryptCertificate
    {
        /// <summary>
        /// 加密算法类型
        /// </summary>
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; } = string.Empty;

        /// <summary>
        /// 随机串
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; } = string.Empty;

        /// <summary>
        /// 附加数据
        /// </summary>
        [JsonPropertyName("associated_data")]
        public string AssociatedData { get; set; } = string.Empty;

        /// <summary>
        /// 数据密文
        /// </summary>
        [JsonPropertyName("ciphertext")]
        public string Ciphertext { get; set; } = string.Empty;
    }
}
