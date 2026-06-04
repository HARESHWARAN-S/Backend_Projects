namespace LMSBusinessLogicLayer.Exceptions
{
    public class AlreadyBorrowedException : Exception
    {
        public AlreadyBorrowedException()
            :base("You have already borrowed the book so can't borrow the same copy again before returning the previous copy.")
        {}
    }
}