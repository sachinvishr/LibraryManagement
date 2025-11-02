using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.DTOs;

public class AddMemberDto
{
    [Required]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    public string Phone { get; set; } = "";
}
