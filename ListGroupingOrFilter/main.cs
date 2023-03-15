namespace ListGroupingNamespace
{
    public class ListFilter
    {
        public void Main(List<int> list)
        {
            var result = GetValuesGreaterThan100(list);
            Console.WriteLine(string.Join(", ", result));
        }

        private static IEnumerable<int> GetValuesGreaterThan100(List<int> list)
        {
            foreach (var value in list)
            {
                if (value > 100)
                    yield return value;
            }
        }
    }
}
