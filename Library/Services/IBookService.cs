using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;

namespace Library.Services
{
    public interface IBookService
    {
        List<BookReadDto>? addBook(BookCreateDto book);
        List<BookReadDto>? ViewAvlBooks();
        List<BookReadDto>? searchBookByCategory(string category);
    }
}