using System.Collections.Generic;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class CopyRepository :ICopyRepository
    {
        public List<Copy> copies =new List<Copy>();
        public List<Copy>? GetAll()
        {
            return copies;
        }

        public void Add(Copy c)
        {
            c.copyId=copies.Count+1;
            copies.Add(c);
        }
    }
}