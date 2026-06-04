using System.Collections.Generic;
using Library.Models;

namespace Library.Repositories
{
    public interface IBorrowRepository
    {
        Borrow? Get(int id);
        List<Borrow>? GetAll();
        List<Borrow> Add(Borrow b);
        public void Update(Borrow b);
    }
}