namespace LMSBusinessLogicLayer.Exceptions
{
    public class OverDueException : Exception
    {
        public OverDueException()
            :base("Can't borrow book as your due amount is above INR.500")
        {}
    }
}