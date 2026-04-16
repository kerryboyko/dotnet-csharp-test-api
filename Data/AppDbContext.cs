namespace TestApi.Data;

using Microsoft.EntityFrameworkCore;
using TestApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Complaint> Complaints => Set<Complaint>();
}