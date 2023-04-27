namespace CryptoApisNamespace
{
    public class Secrets
    {
        public string api_key { get; set; }
        public string base_url { get; set; }
        public string walletId { get; set; }
    }
    public class CreateTransactionFromAddressInput
    {
        public string amount { get; set; }
        public string feePriority { get; set; } = "standard"; // slow standard fast
        public string recipientAddress { get; set; }
    }

    public class CreateTransactionFromWalletInput
    {
        public string feePriority { get; set; } = "standard";
        public Recipient[] recipients { get; set; }
    }

    public class Recipient
    {
        public string address { get; set; }
        public string amount { get; set; }
    }
}
