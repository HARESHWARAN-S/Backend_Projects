/*using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class BookRepository :IBookRepository
    {
        public List<Book> books =new List<Book>();
        public Book? Get(string id)
        {
            foreach(var book in books)
            {
                if(book.book_Id==id)
                {
                    return book;
                }
            }
            return null;
        }
        public List<Book>? GetAll()
        {
            return books;
        }
        public void Add(Book b)
        {
            books.Add(b);
        }
    }
}*/

using System.Collections.Generic;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class BookRepository : IBookRepository
    {
        public List<Book> books = new List<Book>();

        public Book? Get(string id)
        {
            foreach (var book in books)
            {
                if (book.book_Id == id)
                {
                    return book;
                }
            }
            return null;
        }

        public List<Book>? GetAll()
        {
            return books;
        }

        public void Add(Book b)
        {
            Console.WriteLine("1repo");
            books.Add(b);
            Console.WriteLine("2repo");
        }
    }
}