using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Entities;
using LibraryManagement.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowController : ControllerBase
{
    private readonly LibraryDbContext _db;
    public BorrowController(LibraryDbContext db) => _db = db;

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow([FromBody] BorrowDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var book = await _db.Books.FindAsync(dto.BookId);
        if (book == null) return NotFound(new { message = "Book not found" });

        if (book.AvailableCopies <= 0)
            return BadRequest(new { message = "No available copies" });

        var member = await _db.Members.FindAsync(dto.MemberId);
        if (member == null) return NotFound(new { message = "Member not found" });

        using var tx = await _db.Database.BeginTransactionAsync();

        try
        {
            book.AvailableCopies -= 1;

            var borrow = new BorrowRecord
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                BorrowDate = DateTime.UtcNow,
                IsReturned = false
            };

            _db.BorrowRecords.Add(borrow);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            return Ok(borrow);
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    [HttpGet("member/{memberId:int}")]
    public async Task<IActionResult> GetBorrowByMember(int memberId)
    {
        var records = await _db.BorrowRecords
            .Where(b => b.MemberId == memberId && !b.IsReturned)
            .Include(b => b.Book)
            .ToListAsync();

        return Ok(records);
    }
}
