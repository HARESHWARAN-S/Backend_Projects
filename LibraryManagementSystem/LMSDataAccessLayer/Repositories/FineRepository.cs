/*using System;
using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using System.Threading.Tasks;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class FineRepository : IFineRepository
    {
        public List<Fine> fines =new List<Fine>();
        public List<Fine>? GetAll()
        {
            return fines;
        }

        public void Add(Fine f)
        {
            fines.Add(f);
        }
    }
}*/

using System.Collections.Generic;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class FineRepository : IFineRepository
    {
        public List<Fine> fines = new List<Fine>();

        public List<Fine>? GetAll()
        {
            return fines;
        }

        public void Add(Fine f)
        {
            fines.Add(f);
        }
    }
}