using LibraryManagement.Api.DTOs;
using LibraryManagement.Api.Entities;
using LibraryManagement.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly LibraryDbContext _db;
    public MembersController(LibraryDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] AddMemberDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await _db.Members.AnyAsync(m => m.Email == dto.Email))
            return Conflict(new { message = "Email already registered" });

        var member = new Member
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };

        _db.Members.Add(member);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMember(int id)
    {
        var member = await _db.Members.FindAsync(id);
        if (member == null) return NotFound();

        return Ok(member);
    }
}
