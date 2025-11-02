using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReturnController : ControllerBase
{
    private readonly LibraryDbContext _db;
    public ReturnController(LibraryDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> ReturnBook([FromBody] ReturnDto dto)
    {
        var record = await _db.BorrowRecords
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.BorrowId == dto.BorrowId);

        if (record == null) return NotFound(new { message = "Record not found" });
        if (record.IsReturned) return BadRequest(new { message = "Already returned" });

        record.IsReturned = true;
        record.ReturnDate = DateTime.UtcNow;
        record.Book!.AvailableCopies += 1;

        await _db.SaveChangesAsync();

        return Ok(record);
    }
}
