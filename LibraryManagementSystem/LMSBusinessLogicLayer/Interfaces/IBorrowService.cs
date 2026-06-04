using System.Collections.Generic;
using LMSModels;

namespace LMSBusinessLogicLayer.Interfaces
{
    public interface IBorrowService
    {
        public void BorrowBook(int memberId,string bookId);
        public void ReturnBook(int borrowId);
        public void mostBorrowedBook(); //subject to changes
        public void currentlyBorrowedBook(int memberId);
        public void overdueBooks(int memberId);
        public void BorrowingHistory(int memberId);
        public void BorrowingSummary(int memberId);
    }
}