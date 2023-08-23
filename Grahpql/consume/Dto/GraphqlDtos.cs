namespace GraphqlNamespace
{
    public class MultipleBooksQueryResponse
    {
        public IEnumerable<Book> Books { get; set; }
    }
    public class SingleBookQueryResponse
    {
        public Book Book { get; set; }
    }
}
