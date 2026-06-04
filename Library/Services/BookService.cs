using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;
using LMSBusinessLogicLayer.Exceptions;
using Library.Repositories;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICopyRepository _copyRepository;

        public BookService(IBookRepository bookRepo, ICopyRepository copyRepo)
        {
            _bookRepository = bookRepo;
            _copyRepository = copyRepo;
        }
        public List<BookReadDto> addBook(BookCreateDto book)
        {
            //Console.WriteLine("1srv");
            //Console.WriteLine($"book is null? {book == null}");
            //Console.WriteLine($"_bookRepository is null? {_bookRepository == null}");
            if(_bookRepository.Get(book.book_Id) != null)
            {
                //Console.WriteLine("2srv");
                throw new BookAlreadyPresentException(book.book_Id);
                //Console.WriteLine("3srv");
            }
            //Console.WriteLine("4srv");
            var bookEntity = new Book
            {
                book_Id = book.book_Id,
                book_name = book.book_name,
                book_author = book.book_author,
                book_category = book.book_category

            };
            _bookRepository.Add(bookEntity);
            //Console.WriteLine("5srv");
            //_copyRepository.Add(new Copy(book.book_Id));
            //Console.WriteLine($"\nBook {book.book_name} added successfully");
            var result = _bookRepository.GetAll()
            .Select(b => new BookReadDto
            {
                book_Id = b.book_Id,
                book_name = b.book_name,
                book_author = b.book_author,
                book_category = b.book_category
            })
            .ToList();
            return result;
        }
        public List<BookReadDto> ViewAvlBooks()
        {
            List<Book> books = _bookRepository.GetAll();
            List<Copy> copies = _copyRepository.GetAll();
            HashSet<string> BookID = new HashSet<string>();
            if((books == null) || (copies == null))
            {
                throw new NoBookFoundException();
            }
            foreach(var copy in copies)
            {
                BookID.Add(copy.book_Id);
            }
            List<BookReadDto> res=new List<BookReadDto>();
            //Console.WriteLine("-----Available Books-----");
            foreach(var book in books)
            {
                if(BookID.Contains(book.book_Id))
                {
                    res.Add(new BookReadDto
                    {
                        book_Id= book.book_Id,
                        book_name=book.book_name,
                        book_author = book.book_author,
                        book_category=book.book_category
                    });
                }
            }
            return res;
        }
        public List<BookReadDto>? searchBookByCategory(string category)
        {
            var books = _bookRepository.GetAll();
            if(books == null)
            {
                throw new NoBookFoundException();
            }
            List<BookReadDto> res=new List<BookReadDto>();
            foreach(var book in books)
            {
                if(book.book_category==category)
                {
                    res.Add(new BookReadDto
                    {
                        book_Id= book.book_Id,
                        book_name=book.book_name,
                        book_author = book.book_author,
                        book_category=book.book_category
                    });
                }
            }
            if(res.Count==0)
            {
                throw new NoBookFoundException();
            }
            return res;
        }
    }
}