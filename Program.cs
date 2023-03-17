
using AnimalShelterManagerNamespace;
using EncodeDecode;
using ListGroupingNamespace;
using ReptileTypeAnalyzerNamespace;
using TestNamespace;

internal class Program
{
    public static void Main(string[] args)
    {
        #region EncodeDecode
        // new AesOperation().Main();
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
    }
}
