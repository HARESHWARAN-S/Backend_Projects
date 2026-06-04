namespace Library.Models.DTOs
{
    public class MemberCreateDto
    {
        public string member_name { get; set; }
        public string email_id { get; set; }
        public string phone_no { get; set; }
        public int member_type { get; set; }
    }

    public class MemberReadDto
    {
        public int member_id { get; set; }
        public string member_name { get; set; }
        public string email_id { get; set; }
        public string phone_no { get; set; }
        public string member_Status { get; set; }
        public string member_type { get; set; }
    }

    public class MemberSearchDto
    {
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class MemberStatusUpdateDto
    {
        public int memberId { get; set; }
    }
}