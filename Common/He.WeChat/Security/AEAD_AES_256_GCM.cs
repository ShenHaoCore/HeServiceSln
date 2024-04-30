using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace He.WeChat.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class AEAD_AES_256_GCM
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="ciphertext"></param>
        /// <param name="associatedData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string nonce, string ciphertext, string associatedData, string key)
        {
            GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            AeadParameters aeadParameters = new AeadParameters(new KeyParameter(Encoding.UTF8.GetBytes(key)), 128, Encoding.UTF8.GetBytes(nonce), Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);
            byte[] data = Convert.FromBase64String(ciphertext);
            byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return Encoding.UTF8.GetString(plaintext);
        }
    }
}
