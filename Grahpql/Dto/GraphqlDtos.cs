namespace GraphqlNamespace
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
    }

    public class AddBookDto
    {
        public string title { get; set; }
        public string author { get; set; }
    }
    public class BookInput
    {
        public AddBookDto input { get; set; }
    }
    public class BookResponse
    {
        public Book addBook { get; set; }
    }
    public class BooksListResponse
    {
        public List<Book> books { get; set; }
    }
}
