
using AnimalShelterManagerNamespace;
using CryptoApisNamespace;
using EncodeDecode;
using ListGroupingNamespace;
using PDFAnalyzerNamespace;
using ReptileTypeAnalyzerNamespace;
using TestNamespace;

internal class Program
{
    public static void Main(string[] args)
    {
        #region EncodeDecode
        // new AesOperation().Main();
        // new RijndaelExample().Main();
        #endregion
        #region ReptileTypeAnalyzer
        // new ReptileTypeAnalyzer().AnalyzeReptileTypes(new List<int> { 1, 13,2, 5,10, 11, 30, 39, 40,9});
        #endregion
        #region ListFilter
        // new ListFilter().GetValuesGreaterThan100(new List<int>() { 10, 101, 210, 3, 310, 10010 });
        // new ListFilter().GetAllPossiblePairs(new List<int>() { 1, 5, 10, 60, 30, 16, 38, 101 }, 100);
        #endregion
        #region AnimalShelterManager
        // new AnimalShelterManager().Main();
        #endregion
        #region Test (Git Ignored)
        // new Test().Main("asd","asddfasdf",3);
        #endregion
        #region PdfContent
        // PdfContent content = new PDFAnalyzer().ReadPdfContent();
        // new PDFAnalyzer().WriteNewPdfFile(content);
        // new PDFAnalyzer().WriteNewPdfFile(content);
        #endregion
        #region CryptoApis
        new CryptoApis().GetDepositAddress();
        // new CryptoApis().CreateTransactionFromAddress("0x97b5a89f327bc3a15615fbda6f7f19df7874dd12", "0xBf49A48a8d14DEb3582f01f897A3F24B0710F6a9", (decimal)0.1997, "binance-smart-chain", "testnet");
        // new CryptoApis().CreateTransactionFromWallet("0xBf49A48a8d14DEb3582f01f897A3F24B0710F6a9", (decimal)0.02);
        // new CryptoApis().CreateTransactionFromWallet("0xBf49A48a8d14DEb3582f01f897A3F24B0710F6a9", (decimal)0.19979, "binance-smart-chain", "testnet");
        #endregion

    }
}
