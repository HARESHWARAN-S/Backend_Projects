using System.Collections.Generic;
using Library.Models;

namespace Library.Repositories
{
    public interface IMemberRepository
    {
        List<Member> GetAll();
        List<Member> Add(Member m);
        Member? Get(int id);
        Member? Activate (int id);
        Member? Deactivate (int id);
        void Update(Member m);
    }
}