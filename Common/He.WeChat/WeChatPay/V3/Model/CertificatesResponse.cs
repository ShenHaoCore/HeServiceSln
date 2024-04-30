using System.Text.Json.Serialization;

namespace He.WeChat.WeChatPay.V3.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CertificatesResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("data")]
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
