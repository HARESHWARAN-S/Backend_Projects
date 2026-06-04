using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Models
{
    public class Member
    {
        public int? member_id {get; set;}
        public string member_name {get; set;}=string.Empty;
        public string email_id {get; set;}=string.Empty;
        public string phone_no {get;set;}=string.Empty;
        public MemberActiveStatus member_Status {get; set;} 
        public MemberType member_type {get; set;} = MemberType.User;
        public List<Borrow>? Borrows { get; set; } = new List<Borrow>();

    public Member()
    {
        
    }

    public Member(string name, string email, string phno,int type)
    {
        member_name = name;
        email_id = email;
        phone_no = phno;
        member_type = (MemberType)type;
    }

    public override string ToString()
    {
        return $"Member Id : {member_id}\nMember Name : {member_name}\nEmail Id : {email_id}\nPhone No : {phone_no}\nMember Status : {member_Status}\nMember Type : {member_type}\n";
    }

    }
}