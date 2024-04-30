using System.Security.Cryptography.X509Certificates;

namespace He.WeChat.WeChatPay.V3.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PlatformCertificate
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cert">证书</param>
        public PlatformCertificate(string mchid, string serialno, DateTime effectiveTime, DateTime expireTime, X509Certificate2 cert)
        {
            this.MchId = mchid;
            this.SerialNo = serialno;
            this.EffectiveTime = effectiveTime;
            this.ExpireTime = expireTime;
            this.Certificate = cert;
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; } = string.Empty;

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNo { get; set; } = string.Empty;

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectiveTime { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 证书
        /// </summary>
        public X509Certificate2 Certificate;
    }
}
