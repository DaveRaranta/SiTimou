using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace gov.minahasa.sitimou.Helper
{
    internal class CryptoHelper
    {
        //[DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        //private static extern bool ZeroMemory(IntPtr destination, int length);

        //private const int Iterations = 1042; // Recommendation is >= 1000.
        //private const string Str0 = "dzZC7NpF5ZdWvVAG20CgeQWUctR474B5";
        //private const string Str1 = "lczZCi0UZdEQoRSKQdlMTrBD0kdE7QVB";
        //private const string Str2 = "qBB8pQIcdrY8LcJwqKypYqUOlgOyNz8H";

        #region === Password ===
        public void CreatePasswordHash(string passwordString, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
            }
        }

        public bool VerifyPasswordHash(string passwordString, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
                try
                {
                    for (int i = 0; i < computeHash.Length; i++)
                    {
                        if (computeHash[i] != passwordHash[i]) return false;
                    }
                }
                catch (Exception ex)
                {
                    DebugHelper.ShowError("CryptoHelper", "Security", MethodBase.GetCurrentMethod()!.Name, ex);
                    return false;
                }

            }

            return true;
        }
        #endregion

        #region === Encrypt / Decrypt String ===

        private const int SB_KEY_SIZE = 256;
        private const int SB_DERIVATION = 1000;

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        private static string SecretKey()
        {
            SecureString ss = new SecureString();

            ss.AppendChar((char)67);
            ss.AppendChar((char)70);
            ss.AppendChar((char)69);
            ss.AppendChar((char)57);
            ss.AppendChar((char)111);
            ss.AppendChar((char)77);
            ss.AppendChar((char)35);
            ss.AppendChar((char)49);
            ss.AppendChar((char)51);
            ss.AppendChar((char)90);
            ss.AppendChar((char)112);
            ss.AppendChar((char)86);
            ss.AppendChar((char)38);
            ss.AppendChar((char)43);
            ss.AppendChar((char)56);
            ss.AppendChar((char)97);
            ss.AppendChar((char)114);
            ss.AppendChar((char)101);
            ss.AppendChar((char)67);
            ss.AppendChar((char)35);
            ss.AppendChar((char)77);
            ss.AppendChar((char)121);
            ss.MakeReadOnly();

            IntPtr pointer = Marshal.SecureStringToBSTR(ss);
            string sKey = Marshal.PtrToStringBSTR(pointer);

            Marshal.ZeroFreeBSTR(pointer);

            return sKey;

        }

        public static string EncryptString(string text)
        {
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(text);

            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(SecretKey(), saltStringBytes, SB_DERIVATION))
            {
                var keyBytes = password.GetBytes(SB_KEY_SIZE / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
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

        public static string DecryptString(string cipher)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipher);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(SB_KEY_SIZE / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(SB_KEY_SIZE / 8).Take(SB_KEY_SIZE / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((SB_KEY_SIZE / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((SB_KEY_SIZE / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(SecretKey(), saltStringBytes, SB_DERIVATION))
            {
                var keyBytes = password.GetBytes(SB_KEY_SIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
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

        public static string SimpleEncrypt(string text)
        {
            const string ss = "b14Da0705M4e4133bbcE2eX0380a1916";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ss);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string SimpleDecrypt(string text)
        {
            const string ss = "b14Da0705M4e4133bbcE2eX0380a1916";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ss);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
