using System.Collections.Generic;
using System.Linq;
using Library.Models;
using LibraryContext;

namespace Library.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Book? Get(string id)
        {
            return _context.Books
                .FirstOrDefault(b => b.book_Id == id);
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public List<Book> Add(Book b)
        {
            _context.Books.Add(b);
            _context.SaveChanges();  

            return _context.Books.ToList();
        }
    }
}