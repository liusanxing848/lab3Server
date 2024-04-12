using System.Security.Cryptography;
using System.Text;

namespace lab3Server.Service
{
    public static class KeyExchange
    {
        public static RSA rsa;

        static KeyExchange()
        {
            rsa = RSA.Create(2048);
        }

        public static string GetPublicKey()
        {
            // Export the public key
            System.Security.Cryptography.RSAParameters publicKey = rsa.ExportParameters(false);
            return Convert.ToBase64String(rsa.ExportRSAPublicKey());
        }

        public static string GetPrivateKey()
        {
            // Export the private key
            System.Security.Cryptography.RSAParameters privateKey = rsa.ExportParameters(true);
            return Convert.ToBase64String(rsa.ExportRSAPrivateKey());
        }
        public static string EncryptData(string dataToEncrypt, string publicKey)
        {
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            byte[] byteData = Encoding.UTF8.GetBytes(dataToEncrypt);
            byte[] encryptedData = rsa.Encrypt(byteData, RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptData(string encryptedData, string privateKeyBase64)
        {
            // Initialize a new RSA instance for decryption to avoid conflicts with the existing rsa instance
            using (var rsaForDecryption = System.Security.Cryptography.RSA.Create())
            {
                // Import the private key for decryption
                rsaForDecryption.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);

                // Convert the encrypted data from Base64 to byte array
                byte[] byteData = Convert.FromBase64String(encryptedData);

                // Decrypt the data using the private key
                byte[] decryptedData = rsaForDecryption.Decrypt(byteData, RSAEncryptionPadding.OaepSHA256);

                // Convert the decrypted byte array back into a string
                return Encoding.UTF8.GetString(decryptedData);
            }

        }

    }
}
