using He.Common.Extension;
using He.WeChat.Security;
using He.WeChat.WeChatPay.V3.Model;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Collections.Concurrent;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace He.WeChat.WeChatPay.V3
{
    /// <summary>
    /// 
    /// </summary>
    public class WeChatClient : IWeChatClient
    {
        private readonly ILogger<WeChatClient> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeChatClient(ILogger<WeChatClient> logger)
        {
            this.logger = logger;
        }

        private const string V3_CERTIFICATE = "https://api.mch.weixin.qq.com/v3/certificates";
        private const string PAY2_SHA256_RSA2048 = "WECHATPAY2-SHA256-RSA2048";

        private readonly string _accept = "application/json";
        private readonly string _useragent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

        private readonly ConcurrentDictionary<string, PlatformCertificate> _certs = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialno">序列号</param>
        /// <returns></returns>
        public async Task<PlatformCertificate?> GetCertificateAsync(string serialno)
        {
            WeChatConfig wechart = new WeChatConfig();
            PlatformCertificate? platformCert = null;
            if (_certs.TryGetValue(serialno, out platformCert)) { return platformCert; }        // 如果证书序列号已缓存，则直接使用缓存的证书
            RestClient client = new RestClient(V3_CERTIFICATE);
            RestRequest request = new RestRequest();
            string token = WeChatHelper.GenerateToken(V3_CERTIFICATE, "GET", "", wechart.PrivateKey, wechart.MchId, wechart.SerialNo);
            request.AddHeader("Authorization", $"{PAY2_SHA256_RSA2048} {token}");
            request.AddHeader("Accept", _accept);                                               // 如果缺少这句代码就会导致下单接口请求失败，报400错误（Bad Request）
            request.AddHeader("User-Agent", _useragent);                                        // 如果缺少这句代码就会导致下单接口请求失败，报400错误（Bad Request）
            logger.LogDebug($"微信V3获取证书 TOKEN：{PAY2_SHA256_RSA2048} {token}");
            RestResponse response = await client.GetAsync(request);
            logger.LogDebug($"微信V3获取证书：{response}");
            if (response is null || response.StatusCode != HttpStatusCode.OK) { return null; }
            var certResponse = response.Content.ToObject<CertificatesResponse>();
            if (certResponse == null) { return platformCert; }
            foreach (Certificate certificate in certResponse.Certificates)
            {
                if (_certs.ContainsKey(certificate.SerialNo)) { continue; }
                string plaintext = AEAD_AES_256_GCM.Decrypt(certificate.EncryptCertificate.Nonce, certificate.EncryptCertificate.Ciphertext, certificate.EncryptCertificate.AssociatedData, wechart.APIv3Key);
                X509KeyStorageFlags flags = X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable;
                byte[] rawData = Encoding.ASCII.GetBytes(plaintext);
                PlatformCertificate cert = new(wechart.MchId, certificate.SerialNo, certificate.EffectiveTime, certificate.ExpireTime, new(rawData, string.Empty, flags));
                _certs.TryAdd(certificate.SerialNo, cert);
            }
            if (_certs.TryGetValue(serialno, out platformCert)) { return platformCert; }        // 重新从缓存获取证书
            return platformCert;
        }
    }
}
