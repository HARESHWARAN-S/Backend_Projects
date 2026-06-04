using System.Collections.Generic;
using LMSModels;
using LMSBusinessLogicLayer.Exceptions;
using LMSDataAccessLayer.Repositories;
using LMSDataAccessLayer.Interfaces;
using LMSBusinessLogicLayer.Interfaces;

namespace LMSBusinessLogicLayer.Services
{
    public class MemberService : IMemberService
    {
         private readonly MemberRepository _memberRepository = new MemberRepository();
        List<Member> memberlist;
        public MemberService(MemberRepository memberRepo)
        {
            _memberRepository = memberRepo;
            memberlist = _memberRepository.GetAll();
        }
        public void addMember(Member member)
        {
            //Console.WriteLine("1new");
            _memberRepository.Add(member);
            //Console.WriteLine("2");
        }
        public void viewAllMembers()
        {
            //Console.WriteLine("1srv");
            if(memberlist==null)
            {
                //Console.WriteLine("2srv");
                throw new MemberNotFoundException();
                //Console.WriteLine("3srv");
            }
            //Console.WriteLine("4srv");
            foreach(var member in memberlist)
            {
                Console.WriteLine(member);
                Console.WriteLine("-----------------------------");
            }
            //Console.WriteLine("5srv");
        }
        public void searchMemberByMail(string email)
        {
            int flag=0;
            foreach(var member in memberlist)
            {
                if(member.email_id==email)
                {
                    Console.WriteLine(member);
                    flag=1;
                }
            }
            if(flag==0)
            {
                throw new MemberNotFoundException();
            }
        }
        public void searchMemberByPhno(string phno)
        {
            int flag=0;
            foreach(var member in memberlist)
            {
                if(member.phone_no==phno)
                {
                    Console.WriteLine(member);
                    flag=1;
                }
            }
            if(flag==0)
            {
                throw new MemberNotFoundException();
            }
        }
        public void DeactivateMember(int memberId)
        {
            int flag=0;
            foreach(var member in memberlist)
            {
                if(member.member_id==memberId)
                {
                    member.member_Status=(MemberActiveStatus)0;
                    flag=1;
                }
            }
            if(flag==0)
            {
                throw new MemberNotFoundException();
            }
        }
        public void ActivateMember(int memberId)
        {
            int flag=0;
            foreach(var member in memberlist)
            {
                if(member.member_id==memberId)
                {
                    member.member_Status=(MemberActiveStatus)1;
                    flag=1;
                }
            }
            if(flag==0)
            {
                throw new MemberNotFoundException();
            }
        }
    }
}