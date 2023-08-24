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
            return bookData.FirstOrDefault(book => book.id == id);
        }

        public Book GetBook(string title)
        {
            return bookData.FirstOrDefault(book => book.title == title);
        }
        public IEnumerable<Book> GetBooks()
        {
            return bookData;
        }

        public IEnumerable<Book> GetBooks(string author)
        {
            return bookData.Where(book => book.author == author);
        }
    }

    public class BookMutation
    {
        private readonly List<Book> bookData;
        public BookMutation(List<Book> bookData)
        {
            this.bookData = bookData;
        }
        public Book AddBook(AddBookDto input)
        {
            var newBook = new Book();
            newBook.id = bookData.Max(book => book.id) + 1;
            newBook.author = input.author;
            newBook.title = input.title;
            bookData.Add(newBook);
            return newBook;
        }
    }
}
