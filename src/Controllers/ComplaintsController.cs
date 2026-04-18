using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;
using TestApi.Data;
using TestApi.Dtos;
using TestApi.Mappings;

namespace TestApi.Controllers;

[ApiController]
[Route("complaints")]
public class ComplaintsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ComplaintsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<Complaint>> Get(
        [FromQuery] ComplaintStatus? status,
        [FromQuery] PriorityLevel? priority,
        [FromQuery] int? tenantId
    )
    {
        var query = _db.Complaints
            .Include(c => c.Tenant)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(c => c.Status == status.Value);
        }
        if (priority.HasValue)
        {
            query = query.Where(c => c.Priority == priority.Value);
        }
        if (tenantId.HasValue)
        {
            query = query.Where(c => c.TenantId == tenantId.Value);
        }

        return await query.ToListAsync();
    }

    // Get By ID
    [HttpGet("{id}")] // this will create /complaints/:id
    public async Task<ActionResult<Complaint>> GetById(int id)
    {
        var complaint = await _db.Complaints
            .Include(c => c.Tenant)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (complaint == null)
        {
            return NotFound();
        }
        return complaint;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateComplaintDto dto)
    {
        var complaint = dto.ToEntity();
        _db.Complaints.Add(complaint);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = complaint.Id },
            complaint
        );
    }
}