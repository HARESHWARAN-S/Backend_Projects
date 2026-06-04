namespace LMSBusinessLogicLayer.Exceptions
{
    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException(int id)
            :base($"Invalid memberId, no member exists with memberId : {id}.")
        {}
        public MemberNotFoundException()
            :base("No member is found.")
        {}
    }
}