//using System;
using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using System.Threading.Tasks;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface IBorrowRepository
    {
        Borrow? Get(int id);
        List<Borrow>? GetAll();
        void Add(Borrow b);
    }
}