using System.Security.Cryptography;
using System.Text;

namespace EncodeDecode
{
    class AesOperation
    {
        public void Main()
        {
            try
            {
                Console.WriteLine("Enter text that needs to be encrypted..");
                string original = Console.ReadLine();
                string secretPhrase = "timmy's secret phrase is 32 char";

                string encrypted = EncryptString(secretPhrase, original);
                string decrypted = DecryptString(secretPhrase, encrypted);

                Console.WriteLine("Original: {0}", original);
                Console.WriteLine("encrypted : {0}", encrypted);
                Console.WriteLine("decrypted : {0}", decrypted);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
        public string EncryptString(string key, string toEncrypt)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(toEncrypt);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public string DecryptString(string key, string encrypted)
        {
            byte[] iv = new byte[16];
            byte[] toDecrypt = Convert.FromBase64String(encrypted);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(toDecrypt))
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
    }
    class RijndaelExample
    {
        public void Main()
        {
            try
            {
                Console.WriteLine("Enter text that needs to be encrypted..");
                string original = Console.ReadLine();
                string secretPhrase = "timmy's secret phrase is 32 char";

                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    myRijndael.GenerateIV();
                    string encrypted = EncryptStringToBytes(original, secretPhrase, myRijndael.IV);
                    string decrypted = DecryptStringFromBytes(encrypted, secretPhrase, myRijndael.IV);

                    Console.WriteLine("Original: {0}\n", original);
                    Console.WriteLine("encrypted : {0}\n", encrypted);
                    Console.WriteLine("decrypted : {0}", decrypted);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
        public string EncryptStringToBytes(string plainText, string secretPhrase, byte[] IV)
        {
            byte[] encrypted;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Encoding.UTF8.GetBytes(secretPhrase);
                rijAlg.IV = IV;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public string DecryptStringFromBytes(string encrypted, string secretPhrase, byte[] IV)
        {
            string plaintext = null;
            byte[] buffer = Convert.FromBase64String(encrypted);

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Encoding.UTF8.GetBytes(secretPhrase);
                rijAlg.IV = IV;
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(buffer))
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
