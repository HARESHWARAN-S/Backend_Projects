using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;

namespace Library.Services
{
    public interface IBorrowService
    {
        public BorrowReadDto? BorrowBook(int memberId,string bookId);
        public BorrowReadDto? ReturnBook(int borrowId);
        public void mostBorrowedBook(); //subject to changes
        public List<BorrowReadDto>? currentlyBorrowedBook(int memberId);
        public List<BorrowReadDto>? overdueBooks(int memberId);
        public List<BorrowReadDto>? BorrowingHistory(int memberId);
        public void BorrowingSummary(int memberId);
    }
}