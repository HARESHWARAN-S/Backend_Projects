using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace LMSModels
{
    public class Fine
    {
        public int Borrow_id {get; set;}
        public int member_id {get; set;}
        public int amount {get; set;}
        public DueStatus due_status =(DueStatus)0;

        public Fine()
        {
            
        }

        public Fine(int bid, int mid, int amt, int status)
        {
            Borrow_id=bid;
            member_id=mid;
            amount=amt;
            due_status =(DueStatus)status;
        }

        public override string ToString()
        {
            return $"Borrow Id : {Borrow_id}\nMember Id : {member_id}\nDue amount : {amount}\n";
        }
    }
}