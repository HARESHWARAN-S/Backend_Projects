namespace Library.Models;

public enum MemberType
{
    Student = 0,
    User = 1,
    Premium = 2
}

public enum CopyStatus
{
    Available = 0,
    Damaged = 1,
    Unavailable = 2
}

public enum BorrowStatus
{
    NotReturned = 0,
    Returned = 1
}

public enum MemberActiveStatus
{
    active=1,
    inactive=0
}

public enum DueStatus
{
    Paid=1,
    NotPaid=0
}