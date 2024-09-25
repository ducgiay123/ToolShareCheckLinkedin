using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinCheckerTools.Utils
{
    public class CryptorUtils
    {
        public static string EncryptStringToBytes_AesECB(string plainText, string key)
        {
            // Chuyển chuỗi về dạng byte array
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7; // Hoặc PaddingMode.None nếu không cần padding

                // Tạo đối tượng mã hóa
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, null))
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                    // Trả về chuỗi base64
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
        public static string DecryptStringFromBytes_AesECB(string encryptedTextBase64, string key)
        {
            // Chuyển Base64 thành byte array
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedTextBase64);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            //Console.WriteLine(string.Join("|", keyBytes.ToList()));
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7; // Hoặc PaddingMode.None nếu không có padding

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, null))
                {
                    byte[] resultArray = decryptor.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);
                    return Encoding.UTF8.GetString(resultArray);
                }
            }
        }
    }
}
