namespace LMSBusinessLogicLayer.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException()
            :base($"The given state is invalid")
        {}
    }
}