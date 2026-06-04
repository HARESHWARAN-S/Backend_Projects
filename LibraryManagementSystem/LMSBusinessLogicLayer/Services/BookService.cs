using System.Collections.Generic;
using LMSModels;
using LMSBusinessLogicLayer.Exceptions;
using LMSDataAccessLayer.Repositories;
using LMSDataAccessLayer.Interfaces;
using LMSBusinessLogicLayer.Interfaces;
using System;

namespace LMSBusinessLogicLayer.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _bookRepository;
        private readonly CopyRepository _copyRepository;
        public BookService(BookRepository bookRepo,CopyRepository copyRepo)
        {
            _bookRepository = bookRepo;
            _copyRepository = copyRepo;
        }
        public void addBook(Book book)
        {
            Console.WriteLine("1srv");
            Console.WriteLine($"book is null? {book == null}");
            Console.WriteLine($"_bookRepository is null? {_bookRepository == null}");
            if(_bookRepository.Get(book.book_Id) != null)
            {
                Console.WriteLine("2srv");
                throw new BookAlreadyPresentException(book.book_Id);
                Console.WriteLine("3srv");
            }
            Console.WriteLine("4srv");
            _bookRepository.Add(book);
            Console.WriteLine("5srv");
            //_copyRepository.Add(new Copy(book.book_Id));
            Console.WriteLine($"\nBook {book.book_name} added successfully");
        }
        public void ViewAvlBooks()
        {
            var books = _bookRepository.GetAll();
            var copies = _copyRepository.GetAll();
            HashSet<string> BookID = new HashSet<string>();
            if((books == null) || (copies == null))
            {
                throw new NoBookFoundException();
            }
            foreach(var copy in copies)
            {
                BookID.Add(copy.book_Id);
            }
            Console.WriteLine("-----Available Books-----");
            foreach(var book in books)
            {
                if(BookID.Contains(book.book_Id))
                {
                    Console.WriteLine(book);
                    Console.WriteLine("------------------------");
                }
            }
        }
        public void searchBookByCategory(string category)
        {
            var books = _bookRepository.GetAll();
            if(books == null)
            {
                throw new NoBookFoundException();
            }
            Console.WriteLine("-----Available Books-----");
            foreach(var book in books)
            {
                if(book.book_category==category)
                {
                    Console.WriteLine(book);
                    Console.WriteLine("------------------------");
                }
            }
        }
    }
}