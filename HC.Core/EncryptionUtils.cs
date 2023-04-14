using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core
{
    public class EncryptionUtils
    {
        private const string _initVector = "donationcoordina"; //donationcoordinator

        private const int _keysize = 256;
        public static DateTime hashUpdate = Convert.ToDateTime(SiteKeys.PasswordHashUpdateDate);
        private static DateTime oldHashExpiry = Convert.ToDateTime(SiteKeys.PasswordOldHashExpiry);
        public static string Encrypt(string plainText, string _saltValue)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(_initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(_saltValue, null);
            byte[] keyBytes = password.GetBytes(_keysize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string cipherText, string _saltValue)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(_initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(_saltValue, null);
                byte[] keyBytes = password.GetBytes(_keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception) { }

            return String.Empty;
        }

        public static string HashPassword(string password, string salt, DateTime? lastLogin)
        {
            if (lastLogin.HasValue ? lastLogin.Value.CompareTo(hashUpdate) >= 0 || oldHashExpiry.CompareTo(DateTime.Today) <= 0 : false)
            {
                PasswordDeriveBytes sb = new PasswordDeriveBytes(salt, null);
                byte[] saltBytes = sb.GetBytes(_keysize / 8);
                var bytes = KeyDerivation.Pbkdf2(password, saltBytes, KeyDerivationPrf.HMACSHA256, 10000, 16);
                return Convert.ToBase64String(bytes);
            }
            else
                return Encrypt(password, salt);
        }

        public static bool updateStoredPassword(DateTime? lastLogin)
        {
            return lastLogin.HasValue ? lastLogin.Value.CompareTo(hashUpdate) <= 0 && DateTime.Now.CompareTo(hashUpdate) >= 0 : false;
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
