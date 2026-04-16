using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;
using TestApi.Data;

[ApiController]
[Route("tenants")]
public class TenantsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TenantsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<Tenant>> Get()
    {
        return await _db.Tenants.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tenant tenant)
    {
        _db.Tenants.Add(tenant);
        await _db.SaveChangesAsync();
        return Ok(tenant);
    }
}