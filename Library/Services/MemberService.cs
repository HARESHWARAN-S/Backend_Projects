using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;
using LMSBusinessLogicLayer.Exceptions;
using Library.Repositories;

namespace Library.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepo)
        {
            _memberRepository = memberRepo;
        }
        public List<MemberReadDto>? addMember(MemberCreateDto dto)
        {
            //Console.WriteLine("1new");
            _memberRepository.Add(new Member
            {
                member_name=dto.member_name,
                email_id = dto.email_id,
                phone_no = dto.phone_no,
                member_type = (MemberType)dto.member_type,
                member_Status = MemberActiveStatus.active
            });
            return _memberRepository.GetAll()
            .Select(m => new MemberReadDto
            {
                member_id = m.member_id ?? 0,
                member_name = m.member_name,
                email_id = m.email_id,
                phone_no = m.phone_no,
                member_Status = m.member_Status.ToString(),
                member_type = m.member_type.ToString()
            })
            .ToList();
            //Console.WriteLine("2");
        }
        public List<MemberReadDto>? viewAllMembers()
        {
            //Console.WriteLine("1srv");
            var memberlist=_memberRepository.GetAll();
            if(memberlist==null)
            {
                //Console.WriteLine("2srv");
                throw new MemberNotFoundException();
                //Console.WriteLine("3srv");
            }
            //Console.WriteLine("4srv");
            return _memberRepository.GetAll()
            .Select(m => new MemberReadDto
            {
                member_id = m.member_id ?? 0,
                member_name = m.member_name,
                email_id = m.email_id,
                phone_no = m.phone_no,
                member_Status = m.member_Status.ToString(),
                member_type = m.member_type.ToString()
            })
            .ToList();
            //Console.WriteLine("5srv");
        }
        public MemberReadDto? searchMemberByMail(string email)
        {
            var memberlist=_memberRepository.GetAll();
            foreach(var member in memberlist)
            {
                if(member.email_id==email)
                {
                    return new MemberReadDto
                    {
                        member_id = member.member_id ?? 0,
                        member_name = member.member_name,
                        email_id = member.email_id,
                        phone_no = member.phone_no,
                        member_Status = member.member_Status.ToString(),
                        member_type = member.member_type.ToString()
                    };
                }
            }
            throw new MemberNotFoundException();
            
        }
        public MemberReadDto? searchMemberByPhno(string phno)
        {
            var memberlist=_memberRepository.GetAll();
            foreach(var member in memberlist)
            {
                if(member.phone_no==phno)
                {
                    return new MemberReadDto
                    {
                        member_id = member.member_id ?? 0,
                        member_name = member.member_name,
                        email_id = member.email_id,
                        phone_no = member.phone_no,
                        member_Status = member.member_Status.ToString(),
                        member_type = member.member_type.ToString()
                    };
                }
            }
            throw new MemberNotFoundException();
            
        }
        public MemberReadDto? DeactivateMember(int memberId)
        {
            Member? deactivate = _memberRepository.Deactivate(memberId);
            if(deactivate==null)
            {
                throw new MemberNotFoundException();
            }
            deactivate.member_Status=(MemberActiveStatus)0;
            _memberRepository.Update(deactivate);
            return new MemberReadDto
            {
                member_id = deactivate.member_id ?? 0,
                member_name = deactivate.member_name,
                email_id = deactivate.email_id,
                phone_no = deactivate.phone_no,
                member_Status = deactivate.member_Status.ToString(),
                member_type = deactivate.member_type.ToString()
            };
            
        }
        public MemberReadDto? ActivateMember(int memberId)
        {
            Member? activate = _memberRepository.Activate(memberId);
            if(activate==null)
            {
                throw new MemberNotFoundException();
            }
            activate.member_Status=(MemberActiveStatus)1;
            _memberRepository.Update(activate);
            return new MemberReadDto
            {
                member_id = activate.member_id ?? 0,
                member_name = activate.member_name,
                email_id = activate.email_id,
                phone_no = activate.phone_no,
                member_Status = activate.member_Status.ToString(),
                member_type = activate.member_type.ToString()
            };
        }
    }
}