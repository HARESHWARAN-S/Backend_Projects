using System.Collections.Generic;
using Library.Models;
using LibraryContext;
using System.Linq;

namespace Library.Repositories
{
    public class CopyRepository : ICopyRepository
    {
        private readonly LibraryDbContext _context;

        public CopyRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Copy> GetAll()
        {
            return _context.Copies.ToList();
        }

        public List<Copy> Add(Copy c)
        {
            _context.Copies.Add(c);
            _context.SaveChanges();

            return _context.Copies.ToList();
        }

        public void Update(Copy c)
        {
            _context.Copies.Update(c);
            _context.SaveChanges();
        }
    }
}