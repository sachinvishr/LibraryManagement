using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.DTOs;

public class AddBookDto
{
    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Author { get; set; } = "";

    [Required]
    public string ISBN { get; set; } = "";

    public int PublishedYear { get; set; }

    [Range(0, int.MaxValue)]
    public int AvailableCopies { get; set; }
}
