namespace GraphqlNamespace
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
