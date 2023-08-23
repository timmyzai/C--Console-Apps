using Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoApisNamespace
{
    public class CryptoApis
    {
        private const string blockchain = "ethereum";
        private const string network = "goerli";
        private string api_key;
        private string base_url;
        private string walletId;
        public CryptoApis()
        {
            string json = File.ReadAllText("CryptoApis/secret.json");
            Secrets secrets = JsonConvert.DeserializeObject<Secrets>(json);

            this.api_key = secrets.api_key;
            this.base_url = secrets.base_url;
            this.walletId = secrets.walletId;
        }
        public async void GenerateDepositAddress(string blockchain = blockchain, string network = network)
        {
            try
            {
                var apiUrl = $"{base_url}/wallet-as-a-service/wallets/{walletId}/{blockchain}/{network}/addresses";
                var client = new RestClient(new Uri(apiUrl));
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("X-API-Key", api_key);
                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async void GetDepositAddress(string blockchain = blockchain, string network = network)
        {
            try
            {
                var apiUrl = $"{base_url}/wallet-as-a-service/wallets/{walletId}/{blockchain}/{network}/addresses?limit=50&offset=0";

                var client = new RestClient(new Uri(apiUrl));
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("X-API-Key", api_key);
                RestResponse response = client.Execute(request);

                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async void GetWalletAssetDetail(string blockchain = blockchain, string network = network)
        {
            try
            {
                var apiUrl = $"{base_url}/wallet-as-a-service/wallets/{walletId}/{blockchain}/{network}";

                var client = new RestClient(new Uri(apiUrl));
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("X-API-Key", api_key);
                RestResponse response = client.Execute(request);

                TxtEditor.WriteTxt(response.Content, "CryptoApis/Result.txt", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async void CreateTransactionFromWallet(string recipientAddress, decimal amount, string blockchain = blockchain, string network = network)
        {
            try
            {
                var apiUrl = $"{base_url}/wallet-as-a-service/wallets/{walletId}/{blockchain}/{network}/transaction-requests";

                var client = new RestClient(new Uri(apiUrl));
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("X-API-Key", api_key);
                var itemData = new CreateTransactionFromWalletInput
                {
                    recipients = new[] {
                        new Recipient {
                            address = recipientAddress,
                            amount = amount.ToString()
                        }
                    }
                };
                var dataJson = JsonConvert.SerializeObject(itemData);
                var json = $@"{{""data"": {{""item"": {dataJson}}}}}";
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                TxtEditor.WriteTxt(response.Content, Environment.CurrentDirectory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async void CreateTransactionFromAddress(string senderAddress, string recipientAddress, decimal amount, string blockchain = blockchain, string network = network)
        {
            try
            {
                var apiUrl = $"{base_url}/wallet-as-a-service/wallets/{walletId}/{blockchain}/{network}/addresses/{senderAddress}/transaction-requests";

                var client = new RestClient(new Uri(apiUrl));
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("X-API-Key", api_key);
                var itemData = new CreateTransactionFromAddressInput
                {
                    amount = amount.ToString(),
                    recipientAddress = recipientAddress
                };
                var dataJson = JsonConvert.SerializeObject(itemData);
                var json = $@"{{""data"": {{""item"": {dataJson}}}}}";
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}