using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace LMSModels
{
    public class Borrow
    {
        public int borrow_id {get; set;}
        public int book_copyId {get; set;}
        public int member_id {get; set;} 
        public DateTime borrow_date {get; set;}
        public DateTime due_date {get; set;}
        public DateTime? return_date {get; set;}
        public BorrowStatus borrow_status =(BorrowStatus)0;

        public Borrow()
        {
            
        }

        public Borrow(int copyid, int memberid, DateTime nowdate, DateTime due)
        {
            book_copyId=copyid;
            member_id=memberid;
            borrow_date=nowdate;
            due_date=due;
        }

        public override string ToString()
        {
            return $"Borrow ID : {borrow_id}\nBook copy ID : {book_copyId}\nBorrow date : {borrow_date}\nDue date : {due_date}\n";
        } 
    }
}