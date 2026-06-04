using System.Collections.Generic;
using Library.Models;
namespace Library.Repositories
{
    public interface IBookRepository
    {
        Book? Get(string id);
        List<Book>? GetAll();
        List<Book> Add(Book b);
    }
}