namespace LMSBusinessLogicLayer.Exceptions
{
    public class BorrowLimitReachedException : Exception
    {
        public BorrowLimitReachedException(int count)
            :base($"Sorry!!! You cannot borrow now as you have reached your borrow limit of {count}.")
        {}
    }
}