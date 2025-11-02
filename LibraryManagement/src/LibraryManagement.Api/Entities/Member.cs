namespace LibraryManagement.Api.Entities;

public class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
}
