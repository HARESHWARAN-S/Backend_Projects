using System.Collections.Generic;
using LMSModels;
using LMSDataAccessLayer.Interfaces;

namespace LMSDataAccessLayer.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public List<Member> members =new List<Member>();
        public List<Member> GetAll()
        {
            return members;
        }
        public void Add(Member m)
        {
            //Console.WriteLine("1repo");
            m.member_id=members.Count+1;
            members.Add(m);
            //Console.WriteLine("2repo");
        }
        public Member? Get(int id)
        {
            foreach(var mem in members)
            {
                if(mem.member_id==id)
                {
                    return mem;
                }
            }
            return null;
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