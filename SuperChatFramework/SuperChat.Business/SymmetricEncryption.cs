using System.IO;
using System.Security.Cryptography;

namespace SuperChat.Business
{
    public class SymmetricEncryption
    {
        public static string DecryptStringFromBytes_Aes(byte[] cipherText, Aes aesAlg)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}