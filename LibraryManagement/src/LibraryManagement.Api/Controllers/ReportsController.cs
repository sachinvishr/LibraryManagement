using LibraryManagement.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly LibraryDbContext _db;
    public ReportsController(LibraryDbContext db) => _db = db;

    [HttpGet("top-borrowed")]
    public async Task<IActionResult> TopBorrowed()
    {
        var top = await _db.BorrowRecords
            .GroupBy(b => b.BookId)
            .Select(g => new { BookId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Join(_db.Books,
                br => br.BookId,
                b => b.BookId,
                (br, b) => new { b.BookId, b.Title, b.Author, br.Count })
            .ToListAsync();

        return Ok(top);
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> Overdue()
    {
        var cutoff = DateTime.UtcNow.AddDays(-14);

        var overdue = await _db.BorrowRecords
            .Where(r => !r.IsReturned && r.BorrowDate <= cutoff)
            .Include(r => r.Member)
            .Include(r => r.Book)
            .Select(r => new {
                r.BorrowId,
                r.BorrowDate,
                MemberName = r.Member!.Name,
                MemberEmail = r.Member!.Email,
                BookTitle = r.Book!.Title
            })
            .ToListAsync();

        return Ok(overdue);
    }
}
