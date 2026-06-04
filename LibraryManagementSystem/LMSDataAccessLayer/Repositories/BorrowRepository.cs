//using System;
using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using System.Threading.Tasks;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        public List<Borrow> borrows =new List<Borrow>();
        public Borrow? Get(int id)
        {
            foreach(var borrow in borrows)
            {
                if(borrow.borrow_id==id)
                {
                    return borrow;
                }
            }
            return null;
        }
        public List<Borrow>? GetAll()
        {
            return borrows;
        }
        public void Add(Borrow b)
        {
            b.borrow_id=borrows.Count+1;
            borrows.Add(b);
        }
    }
}