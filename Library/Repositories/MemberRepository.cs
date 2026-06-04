using System.Collections.Generic;
using System.Linq;
using Library.Models;
using LibraryContext;

namespace Library.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public List<Member> Add(Member m)
        {
            _context.Members.Add(m);
            _context.SaveChanges();

            return _context.Members.ToList();
        }

        public Member? Get(int id)
        {
            return _context.Members
                .FirstOrDefault(m => m.member_id == id);
        }

        public Member? Activate(int id)
        {
            var member = _context.Members.FirstOrDefault(m => m.member_id == id);

            if (member == null)
                return null;

            member.member_Status = MemberActiveStatus.active;

            _context.SaveChanges();
            return member;
        }

        public Member? Deactivate(int id)
        {
            var member = _context.Members.FirstOrDefault(m => m.member_id == id);

            if (member == null)
                return null;

            member.member_Status = MemberActiveStatus.inactive;

            _context.SaveChanges();
            return member;
        }
        public void Update(Member m)
        {
            _context.Members.Update(m);
            _context.SaveChanges();
        }
    }
}


/*
C:\Users\HP\Desktop\Presidio\Genspark_Training\C#\Day12\LibraryManagementSystem\L
MSDataAccessLayer\Repositories\MemberRepository.cs(11,37): error CS0246: The type
 or namespace name 'IMemberRepository' could not be found (are you missing a usin
g directive or an assembly reference?) [C:\Users\HP\Desktop\Presidio\Genspark_Tra
ining\C#\Day12\LibraryManagementSystem\LMSDataAccessLayer\LMSDataAccessLayer.cspr
oj]
*/

//if we get this error remove the unwanted using statements like "using System.Linq;"