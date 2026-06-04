using System.Collections.Generic;
using LMSModels;

namespace LMSDataAccessLayer.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetAll();
        void Add(Member m);
        Member? Get(int id);
    }
}