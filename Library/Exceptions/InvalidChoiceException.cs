namespace LMSBusinessLogicLayer.Exceptions
{
    public class InvalidChoiceException : Exception
    {
        public InvalidChoiceException(int a,int b)
            :base($"Invalid choice, Please enter choices from {a} to {b}.")
        {}
    }
}