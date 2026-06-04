namespace LMSBusinessLogicLayer.Exceptions
{
    public class InvalidMemberTypeException : Exception
    {
        public InvalidMemberTypeException()
            :base("Invalid member type, must enter values 0,1,2")
        {}
    }
}