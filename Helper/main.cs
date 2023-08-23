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

    public static class TxtEditor
    {
        public static void WriteTxt(string content, string filePath = @"./Result.txt", bool isAppend = false)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, isAppend))
                {
                    streamWriter.Write(content);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, filePath);
            }
        }
        public static string ReadTxt(string filePath = @"./Result.txt")
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, filePath);
            }
            return null;
        }
        public static void DeleteTxt(string filePath = @"./Result.txt")
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                HandleException(ex, filePath);
            }
        }
        private static void HandleException(Exception ex, string filePath)
        {
            if (ex is FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            else if (ex is UnauthorizedAccessException)
            {
                Console.WriteLine($"Access to file denied: {filePath}");
            }
            else if (ex is IOException)
            {
                Console.WriteLine($"Error accessing file: {ex.Message}");
            }
        }
    }
}
