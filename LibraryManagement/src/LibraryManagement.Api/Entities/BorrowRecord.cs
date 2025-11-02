namespace LibraryManagement.Api.Entities;

public class BorrowRecord
{
    public int BorrowId { get; set; }
    public int MemberId { get; set; }
    public int BookId { get; set; }

    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; }

    public Member? Member { get; set; }
    public Book? Book { get; set; }
}
