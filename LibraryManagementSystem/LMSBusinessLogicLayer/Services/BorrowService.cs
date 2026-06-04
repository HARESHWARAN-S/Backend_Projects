using System.Collections.Generic;
using LMSModels;
using LMSBusinessLogicLayer.Exceptions;
using LMSDataAccessLayer.Repositories;
using LMSDataAccessLayer.Interfaces;
using LMSBusinessLogicLayer.Interfaces;

namespace LMSBusinessLogicLayer.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly BorrowRepository _borrowRepository;
        private readonly FineRepository _fineRepository;
        private readonly CopyRepository _copyRepository;
        private readonly MemberRepository _memberRepository;
        List<Fine>? finelist;
        List<Borrow>? borrowlist;
        List<Copy>? copylist;
        public BorrowService(BorrowRepository borrowRepo, FineRepository fineRepo, CopyRepository copyRepo, MemberRepository memRepo)
        {
            _borrowRepository = borrowRepo;
            _fineRepository = fineRepo;
            _copyRepository = copyRepo;
            _memberRepository = memRepo;
            finelist = _fineRepository.GetAll();
            borrowlist = _borrowRepository.GetAll();
            copylist = _copyRepository.GetAll();
        }

        public void BorrowBook(int memberId,string bookId)
        {
            Member memberObject = _memberRepository.Get(memberId);
            if (copylist == null)
            {
                //Console.WriteLine("Copylistnull");
                throw new NoBookFoundException();
            }
            //checking memberId validity
            if (memberObject == null)
            {
                //Console.WriteLine("MemberObjnull");
                throw new MemberNotFoundException(memberId);
            }
            
            //checking for active member
            if (memberObject.member_Status == 0)
            {
                //Console.WriteLine("memberinactive");
                throw new InActiveMemberException();
            }

            //current due amount
            int current_due = 0;
            
            if (finelist != null)
            {
                foreach (var fine in finelist)
                {
                    if (fine.member_id == memberId)
                    {
                        current_due += fine.amount;
                    }
                }
            }
            if (current_due > 500)
            {
                //Console.WriteLine("fine above 500");
                throw new OverDueException();
            }

            //checking same member already borrowed same book and not returned
            
            if (borrowlist != null)
            {
                foreach (var borrows in borrowlist)
                {
                    if (borrows.member_id == memberId)
                    {
                        foreach (var copy in copylist)
                        {
                            if((copy.copyId == borrows.book_copyId) && (copy.book_Id == bookId) && (borrows.borrow_status==(BorrowStatus)0))
                            {
                                //Console.WriteLine("borrowed already");
                                throw new AlreadyBorrowedException();
                            }
                        }
                    }
                }
            }

            //checking copy availability
            
            if (copylist == null)
            {
                //Console.WriteLine("Copylistnull again");
                throw new NoBookFoundException();
            }
            else
            {
                int copyavl_flag = 0;
                foreach (var copy in copylist)
                {   //Console.WriteLine(copy);
                    if ((copy.book_Id == bookId) && (copy.book_copy_Status == 0))
                    {
                        copyavl_flag = 1;
                        break;
                    }
                }
                if (copyavl_flag == 0)
                {
                    Console.WriteLine("Copylistnull loop");
                    throw new NoBookFoundException();
                }
            }

            //checking borrow limit
            int duebooks=0;
            int memberType=-1;
            if (borrowlist != null)
            {
                foreach (var borrow in borrowlist)
                {
                    if ((borrow.member_id == memberId) && (borrow.borrow_status == 0))
                    {
                        duebooks += 1;
                        }
                }
            }

            if (memberObject.member_type ==(MemberType)0)//student 3books-10days due
            {
                memberType=0;
                if (duebooks == 3)
                {
                    throw new BorrowLimitReachedException(3);
                }
            }
            else if (memberObject.member_type ==(MemberType)1)//user 2books-7days due
            {
                memberType=1;
                if (duebooks == 2)
                {
                    throw new BorrowLimitReachedException(2);
                }
            }
            else if (memberObject.member_type ==(MemberType)2)//user 5books-15days due
            {
                memberType=2;
                if (duebooks == 5)
                {
                    throw new BorrowLimitReachedException(5);
                }
            }

            //borrow action
            foreach (var c in copylist)
            {
                if (c.book_Id == bookId && c.book_copy_Status == 0)
                {
                    c.book_copy_Status =(CopyStatus)2;
                    DateTime returnDate = DateTime.Now;
                    DateTime currDate = DateTime.Now;
                    if(memberType==0)
                    {
                        returnDate.AddDays(10);
                    }
                    else if(memberType==1)
                    {
                        returnDate.AddDays(7);
                    }
                    else 
                    {
                        returnDate.AddDays(15);
                    }
                    //memberObject = _memberRepository.Get(memberId);

                    _borrowRepository.Add(new Borrow(c.copyId,memberId,currDate,returnDate));
                }
            }
        }
        
        public void ReturnBook(int borrowId)
        {
            var borrowObject = _borrowRepository.Get(borrowId);
            int borrowedCopy=0;
            int lateDays=0;
            int memberId=0;
            foreach(var borrow in borrowlist)
            {
                if(borrow.borrow_id==borrowId)
                {
                    borrow.return_date= DateTime.Now;
                    borrow.borrow_status=(BorrowStatus)1;
                    borrowedCopy=borrow.book_copyId;
                    lateDays=(int)((borrow.return_date-borrow.due_date)?.TotalDays ?? 0);
                    memberId=borrow.member_id;
                    break;
                }
            }
            foreach(var copy in copylist)
            {
                if(copy.copyId==borrowedCopy)
                {
                    copy.book_copy_Status=0;
                }
            }
            if(lateDays>0)
            {
                _fineRepository.Add(new Fine(borrowId,memberId,lateDays*10,0));
            }
        }
        public void mostBorrowedBook()
        {}
        public void currentlyBorrowedBook(int memberId)
        {
            foreach(var borrow in borrowlist)
            {
                if((borrow.member_id==memberId) && (borrow.borrow_status==0))
                {
                    Console.WriteLine(borrow);
                    Console.WriteLine("-----------------------------");
                }
            }
        }
        public void overdueBooks(int memberId)
        {
            DateTime currdate = DateTime.Now;
            foreach(var borrow in borrowlist)
            {
                if((borrow.member_id==memberId) && (borrow.borrow_status==0) && (borrow.due_date<currdate))
                {
                    Console.WriteLine(borrow);
                    Console.WriteLine("-----------------------------");
                }
            }
        }
        public void BorrowingHistory(int memberId)
        {
            foreach(var borrow in borrowlist)
            {
                if(borrow.member_id==memberId)
                {
                    Console.WriteLine(borrow);
                    Console.WriteLine("-----------------------------");
                }
            }
        }
        public void BorrowingSummary(int memberId)
        {
            int totalborrow=0;
            int returnedCount=0;
            int dueCount=0;
            foreach(var borrow in borrowlist)
            {
                if(borrow.member_id==memberId)
                {
                    if(borrow.borrow_status==0)
                    {
                        dueCount+=1;
                    }
                    else
                    {
                        returnedCount+=1;
                    }
                    totalborrow+=1;
                }
            }
            Console.WriteLine($"---------Borrow Summary of Member ID : {memberId}--------");
            Console.WriteLine($" Total books borrowed : {totalborrow}");
            Console.WriteLine($" Total books returned : {returnedCount}");
            Console.WriteLine($" Total books yet to be returned  : {dueCount}");
        }
    }
}