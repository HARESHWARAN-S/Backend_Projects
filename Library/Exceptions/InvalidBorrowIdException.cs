namespace LMSBusinessLogicLayer.Exceptions
{
    public class InvalidBorrowIdException : Exception
    {
        public InvalidBorrowIdException()
            :base("Payment failed !!! The borrow Id is not present (or) there are no dues")
        {}
    }
}