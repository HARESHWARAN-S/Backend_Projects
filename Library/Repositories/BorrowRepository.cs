using System.Collections.Generic;
using System.Linq;
using Library.Models;
using LibraryContext;

namespace Library.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Borrow? Get(int id)
        {
            return _context.Borrows
                .FirstOrDefault(b => b.borrow_id == id);
        }

        public List<Borrow> GetAll()
        {
            return _context.Borrows.ToList();
        }

        public List<Borrow> Add(Borrow b)
        {
            _context.Borrows.Add(b);
            _context.SaveChanges(); 

            return _context.Borrows.ToList();
        }

        public void Update(Borrow b)
        {
            _context.Borrows.Update(b);
            _context.SaveChanges();
        }
    }
}