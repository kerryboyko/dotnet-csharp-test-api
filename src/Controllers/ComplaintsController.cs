using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;
using TestApi.Data;

namespace TestApi.Controllers;

[ApiController]
[Route("complaints")]
// Is the ":" syntax a way to say "extends"?
public class ComplaintsController : ControllerBase
{
    private readonly AppDbContext _db;

    // Is this our constructor? Seems like it. 
    public ComplaintsController(AppDbContext db)
    {
        // not sure if this is idiomatic.
        _db = db;
    }

    // I'm wondering if the bracket syntax is some sort of decorator?
    [HttpGet]
    public async Task<IEnumerable<Complaint>> Get()
    {
        return await _db.Complaints
            .Include(c => c.Tenant)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Complaint complaint)
    {
        _db.Complaints.Add(complaint);
        await _db.SaveChangesAsync();
        // is Ok a keyword? 
        return Ok(complaint);
    }
}