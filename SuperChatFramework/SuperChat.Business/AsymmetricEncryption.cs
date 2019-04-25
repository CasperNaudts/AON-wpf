using System;
using System.Security.Cryptography;
using SuperChat.Domain;

namespace SuperChat.Business
{
    public class AsymmetricEncryption
    {

        public static byte[] Encrypt(byte[] dataToEncrypt, User recipientUser)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(recipientUser.PublicKey);

            return rsa.Encrypt(dataToEncrypt,false);
        }

        public static string ExportOptions()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            return rsa.ToXmlString(true);
        }

        public static byte[] Decrypt(byte[] encryptedData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsa.ExportParameters(true));

            return rsa.Decrypt(encryptedData, false);
        }

        public static byte[] RsaEncrypt(byte[] dataToEncrypt, RSAParameters rsaKeyInfo, bool doOaepPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    rsa.ImportParameters(rsaKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = rsa.Encrypt(dataToEncrypt, doOaepPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static byte[] RsaDecrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo, bool doOaepPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    rsa.ImportParameters(rsaKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = rsa.Decrypt(dataToDecrypt, doOaepPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}