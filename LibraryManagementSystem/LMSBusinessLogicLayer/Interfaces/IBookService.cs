using System.Collections.Generic;
using LMSModels;

namespace LMSBusinessLogicLayer.Interfaces
{
    public interface IBookService
    {
        void addBook(Book book);
        void ViewAvlBooks();
        void searchBookByCategory(string category);
    }
}