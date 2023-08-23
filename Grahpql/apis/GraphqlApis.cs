namespace GraphqlNamespace
{
    public class BookQuery
    {
        private readonly List<Book> bookData;

        public BookQuery(List<Book> bookData)
        {
            this.bookData = bookData;
        }

        public Book GetBook(int id)
        {
            return bookData.FirstOrDefault(book => book.Id == id);
        }

        public Book GetBook(string title)
        {
            return bookData.FirstOrDefault(book => book.Title == title);
        }
        public IEnumerable<Book> GetBooks()
        {
            return bookData;
        }

        public IEnumerable<Book> GetBooks(string author)
        {
            return bookData.Where(book => book.Author == author);
        }
    }

    public class BookMutation
    {
        private readonly List<Book> bookData;
        public BookMutation(List<Book> bookData)
        {
            this.bookData = bookData;
        }
        public IEnumerable<Book> AddBook(AddBookDto input)
        {
            var newBook = new Book();
            newBook.Id = bookData.Max(book => book.Id) + 1;
            newBook.Author = input.Author;
            newBook.Title = input.Title;
            bookData.Add(newBook);
            return bookData;
        }
    }
}
