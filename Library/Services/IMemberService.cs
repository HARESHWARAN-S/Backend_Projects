using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;

namespace Library.Services
{
    public interface IMemberService
    {
       public List<MemberReadDto>? addMember(MemberCreateDto member);
       public List<MemberReadDto>? viewAllMembers();
       public MemberReadDto? searchMemberByMail(string email);
       public MemberReadDto? searchMemberByPhno(string phno);
       public MemberReadDto? DeactivateMember(int memberId);
       public MemberReadDto? ActivateMember(int memberId);
    }
}