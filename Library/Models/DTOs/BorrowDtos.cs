using System;

namespace Library.Models.DTOs
{
    public class BorrowCreateDto
    {
        public int memberId { get; set; }
        public string bookId { get; set; }
    }

    public class BorrowReturnDto
    {
        public int borrowId { get; set; }
    }

    public class BorrowReadDto
    {
        public int borrow_id { get; set; }
        public int book_copyId { get; set; }
        public int member_id { get; set; }
        public DateTime borrow_date { get; set; }
        public DateTime due_date { get; set; }
        public string status { get; set; }
    }
}