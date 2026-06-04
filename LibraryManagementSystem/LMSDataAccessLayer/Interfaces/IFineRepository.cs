/*using System;
using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using System.Threading.Tasks;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface IFineRepository
    {
        List<Fine>? GetAll();
        void Add(Fine f);
    }
}*/

using System.Collections.Generic;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface IFineRepository
    {
        List<Fine>? GetAll();
        void Add(Fine f);
    }
}