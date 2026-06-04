using System.Collections.Generic;
using System.Linq;
using Library.Models;
using LibraryContext;

namespace Library.Repositories
{
    public class FineRepository : IFineRepository
    {
        private readonly LibraryDbContext _context;

        public FineRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Fine> GetAll()
        {
            return _context.Fines.ToList();
        }

        public List<Fine> Add(Fine f)
        {
            _context.Fines.Add(f);
            _context.SaveChanges();

            return _context.Fines.ToList();
        }
        public void Update(Fine f)
        {
            _context.Fines.Update(f);
            _context.SaveChanges();
        }
    }
}