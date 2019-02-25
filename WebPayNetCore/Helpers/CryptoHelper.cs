using System;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WebPayNetCore.Helpers
{
    internal static class CryptoHelper
    {
        internal static string DownloadPublicKey(string publicKeyUrl)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    return wc.DownloadString(publicKeyUrl);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to download public key file :" + ex.Message, ex);
                }
            }
        }

        internal static byte[] GetRawDataFromKeyData(string keyFileContent)
        {
            var startCertificateMark = "-----BEGIN CERTIFICATE-----";
            var endCertificateMark = "-----END CERTIFICATE-----";
            var startCertificateIndex = keyFileContent.IndexOf(startCertificateMark, StringComparison.Ordinal);
            var endCertificateIndex = keyFileContent.IndexOf(endCertificateMark, StringComparison.Ordinal);
            var publicKeyBase64 = keyFileContent.Substring(startCertificateIndex + startCertificateMark.Length,
                endCertificateIndex - startCertificateIndex - endCertificateMark.Length - 2);
            publicKeyBase64 = publicKeyBase64.Trim();
            var publicKeyRawData = Convert.FromBase64String(publicKeyBase64);
            return publicKeyRawData;
        }

        internal static bool IsValidSs2(string dataBase64, byte[] ss2, byte[] publicKeyRawData)
        {
            var c = new X509Certificate2(publicKeyRawData);
            var rseTest = (RSA)c.PublicKey.Key;
            var valid = rseTest.VerifyData(Encoding.UTF8.GetBytes(dataBase64), ss2, HashAlgorithmName.SHA1,
                RSASignaturePadding.Pkcs1);
            return valid;
        }
    }
}