/*using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface IBookRepository
    {
        Book? Get(string id);
        List<Book>? GetAll();
        void Add(Book b);
    }
}*/

using System.Collections.Generic;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface IBookRepository
    {
        Book? Get(string id);
        List<Book>? GetAll();
        void Add(Book b);
    }
}