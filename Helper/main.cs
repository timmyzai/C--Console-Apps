namespace Helpers
{
    public class Helper
    {
        public static void PrintSingleList(List<int> list)
        {
            Console.WriteLine(string.Join(", ", list));
        }

        public static void PrintMultipleLists(List<List<int>> listlist)
        {
            foreach (List<int> pair in listlist)
            {
                Console.WriteLine(string.Join(",", pair));
            }
        }
    }
}
