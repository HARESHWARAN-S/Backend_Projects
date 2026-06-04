using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;
using LMSBusinessLogicLayer.Exceptions;
using Library.Repositories;

namespace Library.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IFineRepository _fineRepository;
        private readonly ICopyRepository _copyRepository;
        private readonly IMemberRepository _memberRepository;

        public BorrowService(
            IBorrowRepository borrowRepo,
            IFineRepository fineRepo,
            ICopyRepository copyRepo,
            IMemberRepository memRepo)
        {
            _borrowRepository = borrowRepo;
            _fineRepository = fineRepo;
            _copyRepository = copyRepo;
            _memberRepository = memRepo;
        }

        public BorrowReadDto? BorrowBook(int memberId,string bookId)
        {
            var copylist = _copyRepository.GetAll();
            var borrowlist = _borrowRepository.GetAll();
            var finelist = _fineRepository.GetAll();
            //var members = _memberRepository.GetAll();
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
                    //Console.WriteLine("Copylistnull loop");
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
                    DateTime returnDate = DateTime.UtcNow;
                    DateTime currDate = DateTime.UtcNow;
                    if(memberType==0)
                    {
                        returnDate = returnDate.AddDays(10);
                    }
                    else if(memberType==1)
                    {
                        returnDate = returnDate.AddDays(7);
                    }
                    else 
                    {
                        returnDate = returnDate.AddDays(15);
                    }
                    //memberObject = _memberRepository.Get(memberId);
                    Borrow b =new Borrow(c.copyId,memberId,currDate,returnDate);
                    _borrowRepository.Add(b);
                    _copyRepository.Update(c);
                    return new BorrowReadDto
                    {
                        borrow_id = b.borrow_id,
                        book_copyId = b.book_copyId,
                        member_id = b.member_id,
                        borrow_date = b.borrow_date,
                        due_date = b.due_date,
                        status = b.borrow_status.ToString()
                    };

                }
            }
            throw new NoBookFoundException();
        }
        
        public BorrowReadDto? ReturnBook(int borrowId)
        {
            var copylist = _copyRepository.GetAll();
            var borrowlist = _borrowRepository.GetAll();
            var borrowObject = _borrowRepository.Get(borrowId);
            int borrowedCopy=0;
            int lateDays=0;
            int memberId=0;
            Borrow ReturnBook = new Borrow();
            int Return=0;
            foreach(var borrow in borrowlist)
            {
                if(borrow.borrow_id==borrowId)
                {
                    borrow.return_date= DateTime.UtcNow;
                    borrow.borrow_status=(BorrowStatus)1;
                    _borrowRepository.Update(borrow);
                    borrowedCopy=borrow.book_copyId;
                    lateDays=(int)((borrow.return_date-borrow.due_date)?.TotalDays ?? 0);
                    memberId=borrow.member_id;
                    ReturnBook = borrow;
                    Return=1;
                    break;
                }
            }
            if(Return==0)
            {
                throw new InvalidBorrowIdException();
            }
            foreach(var copy in copylist)
            {
                if(copy.copyId==borrowedCopy)
                {
                    copy.book_copy_Status=(CopyStatus)0;
                    _copyRepository.Update(copy);
                }
            }
            if(lateDays>0)
            {
                _fineRepository.Add(new Fine(borrowId,memberId,lateDays*10,0));
            }
            
            return new BorrowReadDto
            {
                borrow_id = ReturnBook.borrow_id,
                book_copyId = ReturnBook.book_copyId,
                member_id = ReturnBook.member_id,
                borrow_date = ReturnBook.borrow_date,
                due_date = ReturnBook.due_date,
                status = ReturnBook.borrow_status.ToString()
            };
            
        }
        public void mostBorrowedBook()
        {}
        public List<BorrowReadDto>? currentlyBorrowedBook(int memberId)
        {
            var borrowlist = _borrowRepository.GetAll();
            Member m = _memberRepository.Get(memberId);
            if(m==null)
            {
                throw new MemberNotFoundException();
            }
            List<BorrowReadDto> current_borrowed = new List<BorrowReadDto>();
            foreach(var borrow in borrowlist)
            {
                if((borrow.member_id==memberId) && (borrow.borrow_status==0))
                {
                    current_borrowed.Add(new BorrowReadDto
                    {
                        borrow_id = borrow.borrow_id,
                        book_copyId = borrow.book_copyId,
                        member_id = borrow.member_id,
                        borrow_date = borrow.borrow_date,
                        due_date = borrow.due_date,
                        status = borrow.borrow_status.ToString()
                    });
                }
            }
            return current_borrowed;
        }
        public List<BorrowReadDto>? overdueBooks(int memberId)
        {
            var borrowlist = _borrowRepository.GetAll();
            Member m = _memberRepository.Get(memberId);
            if(m==null)
            {
                throw new MemberNotFoundException();
            }
            List<BorrowReadDto> due = new List<BorrowReadDto>();
            DateTime currdate = DateTime.UtcNow;
            foreach(var borrow in borrowlist)
            {
                if((borrow.member_id==memberId) && (borrow.borrow_status==0) && (borrow.due_date<currdate))
                {
                    due.Add(new BorrowReadDto
                    {
                        borrow_id = borrow.borrow_id,
                        book_copyId = borrow.book_copyId,
                        member_id = borrow.member_id,
                        borrow_date = borrow.borrow_date,
                        due_date = borrow.due_date,
                        status = borrow.borrow_status.ToString()
                    });
                }
            }
            return due;
        }
        public List<BorrowReadDto>? BorrowingHistory(int memberId)
        {
            var borrowlist = _borrowRepository.GetAll();
            Member m = _memberRepository.Get(memberId);
            if(m==null)
            {
                throw new MemberNotFoundException();
            }
            List<BorrowReadDto> borrows = new List<BorrowReadDto>();
            foreach(var borrow in borrowlist)
            {
                if(borrow.member_id==memberId)
                {
                    borrows.Add(new BorrowReadDto
                    {
                        borrow_id = borrow.borrow_id,
                        book_copyId = borrow.book_copyId,
                        member_id = borrow.member_id,
                        borrow_date = borrow.borrow_date,
                        due_date = borrow.due_date,
                        status = borrow.borrow_status.ToString()
                    });
                }
            }
            return borrows;
        }
        public void BorrowingSummary(int memberId)
        {
            var borrowlist = _borrowRepository.GetAll();
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