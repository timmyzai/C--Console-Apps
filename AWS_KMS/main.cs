using System.Text;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace AWS_Features
{
    #region
    public class AWS_KMS
    {
        public enum EncodeMethod
        {
            Symmetric,
            Asymmetric
        }
        public void Test(string textToEncrypt, EncodeMethod method)
        {
            string encryptedText;
            string decryptedText;
            Console.WriteLine($"Text to Encrypt: {textToEncrypt}");
            switch (method)
            {
                case EncodeMethod.Symmetric:
                    var awsKmsSymmetric = new AWS_KMS_Symmetric();
                    encryptedText = awsKmsSymmetric.EncryptAsync(textToEncrypt).Result;
                    Console.WriteLine("Encrypted Text: " + encryptedText);
                    decryptedText = awsKmsSymmetric.DecryptAsync(encryptedText).Result;
                    Console.WriteLine("Decrypted Text: " + decryptedText); break;
                case EncodeMethod.Asymmetric:
                    var awsKmsAsymmetric = new AWS_KMS_Asymmetric();
                    encryptedText = awsKmsAsymmetric.EncryptAsync(textToEncrypt).Result;
                    Console.WriteLine("Encrypted Text: " + encryptedText);
                    decryptedText = awsKmsAsymmetric.DecryptAsync(encryptedText).Result;
                    Console.WriteLine("Decrypted Text: " + decryptedText); break;
                default:
                    throw new ArgumentException($"Unsupported encoding method: {method}", nameof(method));
            }
        }
    }
    #endregion
    #region Symmetric (Single Key)
    public class AWS_KMS_Symmetric
    {
        private AmazonKeyManagementServiceClient kmsClient;
        private readonly string keyId = "0e76fdd6-1ffa-497f-8f88-b19d6becb9c6";

        public AWS_KMS_Symmetric()
        {
            kmsClient = new AmazonKeyManagementServiceClient();
        }
        public async Task<string> EncryptAsync(string textToEncrypt)
        {
            try
            {
                var encryptRequest = new EncryptRequest
                {
                    KeyId = keyId,
                    Plaintext = new MemoryStream(Encoding.UTF8.GetBytes(textToEncrypt))
                };
                var encryptResponse = await kmsClient.EncryptAsync(encryptRequest);
                return Convert.ToBase64String(encryptResponse.CiphertextBlob.ToArray());
            }
            catch (AmazonKeyManagementServiceException ex)
            {
                Console.WriteLine($"AWS KMS Error: {ex.Message}");
                throw;
            }
        }
        public async Task<string> DecryptAsync(string encryptedText)
        {
            try
            {
                var decryptRequest = new DecryptRequest
                {
                    CiphertextBlob = new MemoryStream(Convert.FromBase64String(encryptedText))
                };
                var decryptResponse = await kmsClient.DecryptAsync(decryptRequest);
                return Encoding.UTF8.GetString(decryptResponse.Plaintext.ToArray());
            }
            catch (AmazonKeyManagementServiceException ex)
            {
                Console.WriteLine($"AWS KMS Error: {ex.Message}");
                throw;
            }
        }
    }
    #endregion
    #region Asymmetric (Public and Private Key Pair)
    public class AWS_KMS_Asymmetric
    {
        private AmazonKeyManagementServiceClient kmsClient;
        private readonly string keyId = "1d49fac2-184e-4e74-bada-a206c9e503a8";
        public AWS_KMS_Asymmetric()
        {
            kmsClient = new AmazonKeyManagementServiceClient();
        }
        public async Task<byte[]> GetPublicKeyAsync()
        {
            try
            {
                var request = new GetPublicKeyRequest { KeyId = keyId };
                var response = await kmsClient.GetPublicKeyAsync(request);
                return response.PublicKey.ToArray();
            }
            catch (AmazonKeyManagementServiceException ex)
            {
                Console.WriteLine($"AWS KMS Error: {ex.Message}");
                throw;
            }
        }
        public async Task<string> EncryptAsync(string plaintext)
        {
            try
            {
                var request = new EncryptRequest
                {
                    KeyId = keyId,
                    Plaintext = new MemoryStream(Encoding.UTF8.GetBytes(plaintext)),
                    EncryptionAlgorithm = EncryptionAlgorithmSpec.RSAES_OAEP_SHA_256
                };

                var response = await kmsClient.EncryptAsync(request);
                return Convert.ToBase64String(response.CiphertextBlob.ToArray());
            }
            catch (AmazonKeyManagementServiceException ex)
            {
                Console.WriteLine($"AWS KMS Error: {ex.Message}");
                throw;
            }
        }
        public async Task<string> DecryptAsync(string ciphertextBase64)
        {
            try
            {
                var request = new DecryptRequest
                {
                    KeyId = keyId,
                    CiphertextBlob = new MemoryStream(Convert.FromBase64String(ciphertextBase64)),
                    EncryptionAlgorithm = EncryptionAlgorithmSpec.RSAES_OAEP_SHA_256
                };

                var response = await kmsClient.DecryptAsync(request);
                return Encoding.UTF8.GetString(response.Plaintext.ToArray());
            }
            catch (AmazonKeyManagementServiceException ex)
            {
                Console.WriteLine($"AWS KMS Error: {ex.Message}");
                throw;
            }
        }
    }
    #endregion
}
