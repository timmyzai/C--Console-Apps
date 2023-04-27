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
                string secretPhrase = "timmy's secret phrase is 32 char";
                while (true)
                {
                    Console.WriteLine("\nEnter 'e' to encrypt, 'd' to decrypt, or 'x' to exit:");
                    string option = Console.ReadLine().ToLower();

                    if (option == "e")
                    {
                        Console.WriteLine("\nEnter text that needs to be encrypted:");
                        string original = Console.ReadLine();
                        string encrypted = EncryptString(secretPhrase, original);
                        Console.WriteLine("\nEncrypted text: {0}", encrypted);
                    }
                    else if (option == "d")
                    {
                        Console.WriteLine("\nEnter text that needs to be decrypted:");
                        string encrypted = Console.ReadLine();
                        string decrypted = DecryptString(secretPhrase, encrypted);
                        Console.WriteLine("\nDecrypted text: {0}", decrypted);
                    }
                    else if (option == "x")
                    {
                        Console.WriteLine("\nThank you for using the encryption/decryption app!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid option! Please enter 'e', 'd', or 'x'.");
                    }
                }
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
                string secretPhrase = "timmy's secret phrase is 32 char";
                while (true)
                {
                    Console.WriteLine("\nEnter 'e' to encrypt, 'd' to decrypt, or 'x' to exit:");
                    string option = Console.ReadLine().ToLower();
                    if (option == "e")
                    {
                        Console.WriteLine("\nEnter text that needs to be encrypted:");
                        string original = Console.ReadLine();
                        using (RijndaelManaged myRijndael = new RijndaelManaged())
                        {
                            myRijndael.GenerateIV();
                            string encrypted = EncryptStringToBytes(secretPhrase, original, myRijndael.IV);
                            Console.WriteLine("\nEncrypted text: {0}", encrypted);
                        }
                    }
                    else if (option == "d")
                    {
                        Console.WriteLine("\nEnter text that needs to be decrypted:");
                        string encrypted = Console.ReadLine();
                        Console.WriteLine("\nEnter text that needs to be encrypted:");
                        string original = Console.ReadLine();
                        using (RijndaelManaged myRijndael = new RijndaelManaged())
                        {
                            string decrypted = DecryptStringFromBytes(secretPhrase, encrypted, myRijndael.IV);
                            Console.WriteLine("\nDecrypted text: {0}", decrypted);
                        }
                    }
                    else if (option == "x")
                    {
                        Console.WriteLine("\nThank you for using the encryption/decryption app!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid option! Please enter 'e', 'd', or 'x'.");
                    }
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
