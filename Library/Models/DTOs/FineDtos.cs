namespace Library.Models.DTOs
{
    public class FineReadDto
    {
        public int Borrow_id { get; set; }
        public int member_id { get; set; }
        public int amount { get; set; }
        public string status { get; set; }
    }

    public class FinePaymentDto
    {
        public int borrowId { get; set; }
    }
}