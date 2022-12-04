using System;
using System.IO;
using System.Security.Cryptography;

namespace Editor
{
    public static class FileEncrypter
    {
        public static byte[] Encrypy(string text, byte[] key)
        {
            if (text == null || text.Length <= 0) throw new ArgumentException(nameof(text));

            // AES-128の場合は、「128bit / 8 = 16byte」の鍵を用意する
            if (key.Length != 16) throw new ArgumentException(nameof(key));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                // 鍵の長さを128bitにする
                aesAlg.KeySize = 128;
                aesAlg.Key = key;

                // ModeをECBにする
                aesAlg.Mode = CipherMode.ECB;

                // Encryptorを生成
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // 暗号化
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static string Decrypy(byte[] cipherText, byte[] key)
        {
            if (cipherText == null || cipherText.Length <= 0) throw new ArgumentException(nameof(cipherText));

            // AES-128の場合は、「128bit / 8 = 16byte」の鍵を用意する
            if (key.Length != 16) throw new ArgumentException(nameof(key));

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 128;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Key = key;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
