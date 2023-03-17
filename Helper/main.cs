using System.ComponentModel;

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

        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
