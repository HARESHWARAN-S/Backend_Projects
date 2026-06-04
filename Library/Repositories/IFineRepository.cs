using System.Collections.Generic;
using Library.Models;

namespace Library.Repositories
{
    public interface IFineRepository
    {
        List<Fine>? GetAll();
        List<Fine> Add(Fine f);
        public void Update(Fine f);
    }
}