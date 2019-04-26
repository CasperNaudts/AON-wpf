using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SuperChat.Business
{
    public static class Hash
    {
        public static string HashInput(string text, string salt)
        {
            string saltAndPwd = text + salt;

            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256Hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(saltAndPwd));

            return JsonConvert.SerializeObject(hashedDataBytes);
        }
    }
}