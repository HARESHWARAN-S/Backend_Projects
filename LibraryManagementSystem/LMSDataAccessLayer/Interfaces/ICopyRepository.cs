using System.Collections.Generic;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface ICopyRepository
    {
        List<Copy>? GetAll();
        void Add(Copy c);
    }
}