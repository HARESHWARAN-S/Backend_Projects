namespace LMSBusinessLogicLayer.Exceptions
{
    public class InActiveMemberException : Exception
    {
        public InActiveMemberException()
            :base("Since the member(you) is(are) inactive you cannot borrow book")
        {}
    }
}