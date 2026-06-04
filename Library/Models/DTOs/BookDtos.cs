namespace Library.Models.DTOs
{
    public class BookCreateDto
    {
        public string book_Id { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public string book_category { get; set; }
    }

    public class BookReadDto
    {
        public string book_Id { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public string book_category { get; set; }
    }

    public class BookSearchDto
    {
        public string book_Id { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public string book_category { get; set; }
    }
}