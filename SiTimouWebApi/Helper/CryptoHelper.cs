using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace minahasa.sitimou.webapi.Helper
{
    public class CryptoHelper
    {
        private static readonly Random Random = new();

        #region === Auth ===

        public void CreatePassword(string passwordString, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
        }

        public bool VerifyPassword(string passwordString, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
            try
            {
                for (var i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i]) return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                //Helper.ShowError("Security", MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

            return true;
        }

        #endregion

        #region === Encrypt / Decrypt

        public static string SimpleEncrypt(string text)
        {
            const string ss = "b14Da0705XA44133MbcE2eX0380z1916";
            var iv = new byte[16];

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(ss);
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(text);
            }

            var array = memoryStream.ToArray();

            return Convert.ToBase64String(array);
        }


        public static string SimpleDecrypt(string text)
        {
            const string ss = "b14Da0705XA44133MbcE2eX0380z1916";
            var iv = new byte[16];
            var buffer = Convert.FromBase64String(text);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(ss);
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        #endregion

        #region === Generator ===

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        #endregion

    }
}
