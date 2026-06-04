namespace LMSBusinessLogicLayer.Exceptions
{
    public class BookAlreadyPresentException : Exception
    {
        public BookAlreadyPresentException(string id)
            :base($"Invalid book entry, The book with ISBN number {id} is already present so you cannot add a book but you can add a copy.")
        {}
    }
}