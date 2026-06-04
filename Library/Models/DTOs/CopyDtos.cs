namespace Library.Models.DTOs
{
    public class CopyCreateDto
    {
        public string bookid { get; set; }
        public bool Added { get; set; }
    }

    public class CopyReadDto
    {
        public int copyId { get; set; }
        public string book_Id { get; set; }
        public string status { get; set; }
    }

    public class CopyStatusUpdateDto
    {
        public int copyId { get; set; }
        public int status { get; set; }
    }
}