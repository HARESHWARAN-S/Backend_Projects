using System.Collections.Generic;
using Library.Models;

namespace Library.Repositories
{
    public interface ICopyRepository
    {
        List<Copy>? GetAll();
        List<Copy> Add(Copy c);
        public void Update(Copy c);
    }
}