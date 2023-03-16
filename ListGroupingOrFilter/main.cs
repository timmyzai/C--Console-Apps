using Helpers;

namespace ListGroupingNamespace
{
    public class ListFilter
    {
        public void GetValuesGreaterThan100(List<int> list)
        {
            List<int> result = new List<int>();
            foreach (var value in list)
            {
                if (value > 100)
                    result.Add(value);
            }
            Console.WriteLine("Result:");
            Helper.PrintSingleList(result);
        }

        public void GetAllPossiblePairs(List<int> originalList, int target)
        {
            List<List<int>> allPossiblePairs = new List<List<int>>();
            GeneratePairs(originalList, 0, new List<int>(), allPossiblePairs);
            GetSums(allPossiblePairs, target, out List<int> result);

            // Console.WriteLine("All Possible Pairs:");
            // Helper.PrintMultipleLists(allPossiblePairs);
            Console.WriteLine("Result:");
            Helper.PrintSingleList(result);
        }
        private static void GeneratePairs(List<int> originalList, int index, List<int> currentPair, List<List<int>> allPossiblePairs)
        {
            if (index == originalList.Count)
            {
                allPossiblePairs.Add(new List<int>(currentPair));
            }
            else
            {
                // Recursive case: add the current element to the pair, and then generate pairs with the remaining elements
                currentPair.Add(originalList[index]);
                GeneratePairs(originalList, index + 1, currentPair, allPossiblePairs);
                currentPair.RemoveAt(currentPair.Count - 1);
                GeneratePairs(originalList, index + 1, currentPair, allPossiblePairs);
            }
        }

        private static void GetSums(List<List<int>> allPossiblePairs, int target, out List<int> result)
        {
            var pairs = new List<List<int>>();
            foreach (List<int> pair in allPossiblePairs)
            {
                var sum = pair.Sum();
                if (sum <= target)
                {
                    pairs.Add(pair);
                }
            }
            if (pairs.Count() == 0)
            {
                Environment.FailFast("Not enough fund.");
            }

            SortPairs(pairs);
            result = pairs.First();
        }
        private static void SortPairs(List<List<int>> pairs)
        {
            pairs.Sort((x, y) =>
            {
                int countComparison = y.Count.CompareTo(x.Count);
                if (countComparison == 0)
                {
                    return y.Sum().CompareTo(x.Sum());
                }
                return countComparison;
            });
        }
    }


}
