using System.Collections.Generic;
using LMSModels;

namespace LMSBusinessLogicLayer.Interfaces
{
    public interface IMemberService
    {
       public void addMember(Member member);
       public void viewAllMembers();
       public void searchMemberByMail(string email);
       public void searchMemberByPhno(string phno);
       public void DeactivateMember(int memberId);
       public void ActivateMember(int memberId);
    }
}