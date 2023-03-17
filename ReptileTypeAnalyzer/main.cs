using System.Globalization;

namespace ReptileTypeAnalyzerNamespace
{
    public class ReptileTypeAnalyzer
    {
        private static Dictionary<Reptile, ReptileType> reptileTypes = new Dictionary<Reptile, ReptileType>();

        static ReptileType GetReptileType(Reptile reptile)
        {

            if (reptileTypes.TryGetValue(reptile, out var reptileType))
            {
                return reptileType;
            }
            switch ((int)reptile)
            {
                case int value when value < 10:
                    reptileType = ReptileType.Snake;
                    break;

                case int value when value >= 10 && value <= 29:
                    reptileType = ReptileType.Lizard;
                    break;

                case int value when value >= 30 && value <= 39:
                    reptileType = ReptileType.Turtle;
                    break;

                default:
                    reptileType = ReptileType.Invalid;
                    break;
            }


            reptileTypes[reptile] = reptileType;
            return reptileType;
        }

        public void AnalyzeReptileTypes(List<int> values)
        {
            var reptilesByType = values
                .Select(value => (Reptile)value)
                .GroupBy(r => GetReptileType(r));

            foreach (var group in reptilesByType)
            {
                Console.WriteLine(group.Key + ": " + string.Join(", ", group));
            }
            Console.WriteLine();
            Console.WriteLine("###Comments###");

            foreach (var group in reptilesByType)
            {
                var undefinedCount = group.Count(value => !Enum.IsDefined(typeof(Reptile), value));
                var definedCount = group.Count(value => Enum.IsDefined(typeof(Reptile), value));

                PrintGroupSummary(group.Key.ToString(), definedCount, undefinedCount);
            }

        }
        private static void PrintGroupSummary(string groupName, int definedCount, int undefinedCount)
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            var definedText = definedCount > 0 ? $"{definedCount} {textInfo.ToLower(groupName)}(s)" : "0";
            var undefinedText = undefinedCount > 0 ? $" and {undefinedCount} unknown {textInfo.ToLower(groupName)}(s)" : "";

            var result = $"There are {definedText}{undefinedText} in {textInfo.ToTitleCase(groupName)}.";
            Console.WriteLine(result);
        }
    }
}
