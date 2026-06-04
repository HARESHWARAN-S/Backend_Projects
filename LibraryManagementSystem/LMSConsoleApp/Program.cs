using System.Collections.Generic;
using LMSModels;
using LMSBusinessLogicLayer.Exceptions;
using LMSBusinessLogicLayer.Interfaces;
using LMSBusinessLogicLayer.Services;
using LMSDataAccessLayer.Repositories;
using System.ComponentModel.Design.Serialization;

namespace LMSConsoleApp
{
    class Program
    {
        public static IBookService bookservice;
        public static IBorrowService borrowservice;
        public static IFineService fineservice;
        public static IMemberService memberservice;
        public static ICopyService copyservice;
        public static BookRepository bookRepo = new BookRepository();
        public static CopyRepository copyRepo = new CopyRepository();
        public static BorrowRepository borrowRepo = new BorrowRepository();
        public static FineRepository fineRepo = new FineRepository();
        public static MemberRepository memberRepo = new MemberRepository();

        public Program()
        {
            bookservice = new BookService(bookRepo,copyRepo);
            borrowservice = new BorrowService(borrowRepo, fineRepo, copyRepo, memberRepo);
            fineservice = new FineService(fineRepo);
            memberservice = new MemberService(memberRepo);
            copyservice = new CopyService(bookRepo,copyRepo);
        }
            public static  void MainMenu()
            {
                int main_choice;
                do
                {
                    Console.WriteLine("Welcome to MainMenu");
                    Console.WriteLine("1.Member Management");
                    Console.WriteLine("2.Book Management");
                    Console.WriteLine("3.Fine Management");
                    Console.WriteLine("4.Reports");
                    Console.WriteLine("5.Exit");

                    Console.WriteLine("Enter a valid choice:");
                    main_choice = Convert.ToInt32(Console.ReadLine() ?? "");
                    if((main_choice<=0) || (main_choice>5))
                    {
                        throw new InvalidChoiceException(1,5);
                    }
                    if(main_choice==1)
                    {
                        MemberManagement();
                    }
                    else if(main_choice==2)
                    {
                        BookManagement();
                    }
                    else if(main_choice==3)
                    {
                        FineManagement();
                    }
                    else if(main_choice==4)
                    {
                        Reports();
                    }
                }while(main_choice!=5);
            }

            public static void MemberManagement()
            {
                int member_choice;
                do
                {
                    Console.WriteLine("Welcome to Member Management");
                    Console.WriteLine("1.Add Member");
                    Console.WriteLine("2.View Members");
                    Console.WriteLine("3.Search by E-Mail");
                    Console.WriteLine("4.Search by Phone number");
                    Console.WriteLine("5.Deactivate Member");
                    Console.WriteLine("6.Activate Member");
                    Console.WriteLine("7.Exit to Main Menu");

                    Console.WriteLine("Enter a valid choice:");
                    member_choice= Convert.ToInt32(Console.ReadLine()??"");

                    if((member_choice<=0) || (member_choice>7))
                    {
                        throw new InvalidChoiceException(1,7);
                    }
                    if(member_choice==1)
                    {
                        //Console.WriteLine("1new");
                        Member m =MemberAdd();
                        //Console.WriteLine($"m is null: {m == null}");
                        //Console.WriteLine($"memberservice is null: {memberservice == null}");
                        //Console.WriteLine("2new");
                        if(m==null)
                        {
                            //Console.WriteLine("3new");
                            throw new MemberNotFoundException();
                        }
                        //Console.WriteLine("4new");
                        memberservice.addMember(m);
                        Console.WriteLine("Member Added Successfully");
                        //Console.WriteLine("5new");
                    }
                    else if(member_choice==2)
                    {
                        //Console.WriteLine("1prg");
                        memberservice.viewAllMembers();
                        //Console.WriteLine("2prg");
                    }
                    else if(member_choice==3)
                    {
                        Console.WriteLine("Please enter the email id:");
                        string email=Console.ReadLine()??"";
                        memberservice.searchMemberByMail(email);
                    }
                    else if(member_choice==4)
                    {
                        Console.WriteLine("Please enter the phone number:");
                        string phno=Console.ReadLine()??"";
                        memberservice.searchMemberByPhno(phno);
                    }
                    else if(member_choice==5)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        memberservice.DeactivateMember(memberId);
                    }
                    else if(member_choice==6)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        memberservice.ActivateMember(memberId);
                    }
                }while(member_choice!=7);
                
            }
            public static Member MemberAdd()
            {
                Console.WriteLine("Please enter the member name:");
                String Member_name=Console.ReadLine()??"";
                Console.WriteLine("Please enter the member Email Id:");
                String Member_mail=Console.ReadLine()??"";
                Console.WriteLine("Please enter the member Phone number:");
                String Member_phone=Console.ReadLine()??"";
                Console.WriteLine("Enter member type: 0-student, 1-user, 2-premium");
                int mtype = Convert.ToInt32(Console.ReadLine()??"");
                if((mtype<0) || (mtype>2))
                {
                    throw new InvalidMemberTypeException();
                }
                return new Member(Member_name, Member_mail,Member_phone,mtype);
            }
            public static void BookManagement()
            {
                int book_choice;
                do
                {
                    Console.WriteLine("Welcome to Book Management");
                    Console.WriteLine("1.Add Books");
                    Console.WriteLine("2.Add Book Copy");
                    Console.WriteLine("3.View available books");
                    Console.WriteLine("4.Search Books by category");
                    Console.WriteLine("5.Mark Book copy status");
                    Console.WriteLine("6.Borrow Book");
                    Console.WriteLine("7.Return Book");
                    Console.WriteLine("8.Exit to Main Menu");

                    Console.WriteLine("Enter a valid choice:");
                    book_choice= Convert.ToInt32(Console.ReadLine()??"");

                    if((book_choice<=0) || (book_choice>8))
                    {
                        throw new InvalidChoiceException(1,8);
                    }
                    if(book_choice==1)
                    {
                        Console.WriteLine("1prg");
                        Book b = BookAdd();
                        Console.WriteLine("2prg");
                        bookservice.addBook(b);
                        Console.WriteLine("3prg");
                        copyservice.addCopy(b.book_Id,true);
                        Console.WriteLine("4prg");
                    }
                    else if(book_choice==2)
                    {
                        Console.WriteLine("Please enter the book id whose copy to be added");
                        string bookid=Console.ReadLine()??"";
                        copyservice.addCopy(bookid,false);
                    }
                    else if(book_choice==3)
                    {
                        bookservice.ViewAvlBooks();
                    }
                    else if(book_choice==4)
                    {
                        Console.WriteLine("Please enter the book category:");
                        string category=Console.ReadLine()??"";
                        bookservice.searchBookByCategory(category);
                    }
                    else if(book_choice==5)
                    {
                        Console.WriteLine("Please enter the current status");
                        int status=Convert.ToInt32(Console.ReadLine()??"");
                        Console.WriteLine("Please enter the copy Id:");
                        int copyId=Convert.ToInt32(Console.ReadLine()??"");
                        copyservice.MarkCopyStatus(copyId,status);
                    }
                    else if(book_choice==6)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        Console.WriteLine("Please enter the book id");
                        string bookId=Console.ReadLine()??"";
                        borrowservice.BorrowBook(memberId,bookId);
                    }
                    else if(book_choice==7)
                    {
                        Console.WriteLine("Please enter the borrow id");
                        int borrowId=Convert.ToInt32(Console.ReadLine()??"");
                        borrowservice.ReturnBook(borrowId);
                    }
                }while(book_choice!=8);
                
            }
            public static Book BookAdd()
            {
                Console.WriteLine("Please enter the Book ISBN number:");
                string ISBN=Console.ReadLine()??"";
                Console.WriteLine("Please enter the book name:");
                string name=Console.ReadLine()??"";
                Console.WriteLine("Please enter the author name");
                string author=Console.ReadLine()??"";
                Console.WriteLine("Please enter the book category:");
                string category=Console.ReadLine()??"";
                return new Book(ISBN,name,author,category);
            }
            public static void FineManagement()
            {
                int fine_choice;
                do
                {
                    Console.WriteLine("Welcome to Fine Management");
                    Console.WriteLine("1.View Unpaid Fine");
                    Console.WriteLine("2.Pay Fine");
                    Console.WriteLine("3.Fine payment history");
                    Console.WriteLine("4.Exit to Main Menu");

                    Console.WriteLine("Enter a valid choice:");
                    fine_choice= Convert.ToInt32(Console.ReadLine()??"");

                    if((fine_choice<=0) || (fine_choice>4))
                    {
                        throw new InvalidChoiceException(1,4);
                    }
                    if(fine_choice==1)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        fineservice.viewUnpaidFine(memberId);
                    }
                    else if(fine_choice==2)
                    {
                        Console.WriteLine("Please enter the borrow id");
                        int borrowId=Convert.ToInt32(Console.ReadLine()??"");
                        fineservice.payFine(borrowId);
                    }
                    else if(fine_choice==3)
                    {
                        Console.WriteLine("Please enter the member id:");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        fineservice.finePaymentHistory(memberId);
                    }
                }while(fine_choice!=4);
            }
            public static void Reports()
            {
                int reports_choice;
                do
                {
                    Console.WriteLine("Welcome to Reports");
                    Console.WriteLine("1.Show curremtly borrowed books");
                    Console.WriteLine("2.Show over due books");
                    Console.WriteLine("3.Show pending fine report");
                    Console.WriteLine("4.Show Most borrowed books");
                    Console.WriteLine("5.Show Borrowing History");
                    Console.WriteLine("6.Show Borrowing Summary");
                    Console.WriteLine("7.Exit to Main Menu");

                    Console.WriteLine("Enter a valid choice:");
                    reports_choice= Convert.ToInt32(Console.ReadLine()??"");

                    if((reports_choice<=0) || (reports_choice>7))
                    {
                        throw new InvalidChoiceException(1,7);
                    }
                    if(reports_choice==1)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        borrowservice.currentlyBorrowedBook(memberId);
                    }
                    else if(reports_choice==2)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        borrowservice.overdueBooks(memberId);
                    }
                    else if(reports_choice==3)
                    {
                        fineservice.pendingFineList();
                    }
                    else if(reports_choice==4)
                    {
                        borrowservice.mostBorrowedBook();
                    }
                    else if(reports_choice==5)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        borrowservice.BorrowingHistory(memberId);
                    }
                    else if(reports_choice==6)
                    {
                        Console.WriteLine("Please enter the member id");
                        int memberId=Convert.ToInt32(Console.ReadLine()??"");
                        borrowservice.BorrowingSummary(memberId);
                    }
                }while(reports_choice!=7);
            }
            public static void Main(string[] args)
            {
                try
                {
                    new Program();
                    Program.MainMenu();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
    }
}