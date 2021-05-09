using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace gen.common
{
    public static class Cryptograph
    {
        private const int KeySize = 128;

        private const int DerivationIterations = 1000;

        public static string Encrypt(string stringToEncrypt, string secureStage)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(stringToEncrypt);
            using (var password = new Rfc2898DeriveBytes(secureStage, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(KeySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Encrypt(SecureString secureString, string secureStage) => Encrypt(GetInsecureString(secureString), secureStage);

        public static byte[] EncryptToBytes(string stringToEncrypt, string secureStage) => Encoding.UTF8.GetBytes(Encrypt(stringToEncrypt, secureStage));
        public static byte[] EncryptToBytes(SecureString stringToEncrypt, string secureStage) => Encoding.UTF8.GetBytes(Encrypt(stringToEncrypt, secureStage));

        public static byte[] HashPassword(string password)
            => new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(password));

        public static byte[] HashPassword(SecureString secureString)
            => new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(GetInsecureString(secureString)));

        public static string Decrypt(string stringToDecrypt, string secureStage)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(stringToDecrypt);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KeySize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8).Take(KeySize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8 * 2).Take(cipherTextBytesWithSaltAndIv.Length - KeySize / 8 * 2).ToArray();

            using (var password = new Rfc2898DeriveBytes(secureStage, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(KeySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(byte[] bytesToDecrypt, string secureStage) => Decrypt(Encoding.UTF8.GetString(bytesToDecrypt), secureStage);

        public static SecureString GetSecureString(string input)
        {
            var secureString = new SecureString();
            foreach (var c in input)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }

        public static string GetInsecureString(SecureString inputString)
        {
            if (inputString == null)
            {
                return null;
            }

            var ptr = Marshal.SecureStringToBSTR(inputString);

            try
            {
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16]; // 32 Bytes will give us 256 bits or 16 will give us 128 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
