using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Entities;
using LibraryManagement.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryDbContext _db;
    public BooksController(LibraryDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] AddBookDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _db.Books.AnyAsync(b => b.ISBN == dto.ISBN))
            return Conflict(new { message = "ISBN already exists" });

        if (dto.AvailableCopies < 0)
            return BadRequest(new { message = "AvailableCopies cannot be negative" });

        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            PublishedYear = dto.PublishedYear,
            AvailableCopies = dto.AvailableCopies
        };

        _db.Books.Add(book);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _db.Books
            .Select(b => new {
                b.BookId, b.Title, b.Author, b.ISBN, b.PublishedYear, b.AvailableCopies,
                IsAvailable = b.AvailableCopies > 0
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null) return NotFound();

        return Ok(book);
    }
}
