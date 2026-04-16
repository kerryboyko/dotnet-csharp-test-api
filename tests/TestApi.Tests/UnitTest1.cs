using Xunit;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

public class TenantTests
{
    [Fact]
    public async Task CanAddTenant()
    {
        // and this is why dependency injection is powerful
        // don't need to set up a seperate test DB, can use inmem.
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var db = new AppDbContext(options);

        db.Tenants.Add(new Tenant { Name = "Kerry Ann" });
        await db.SaveChangesAsync();

        var tenants = await db.Tenants.ToListAsync();

        Assert.Single(tenants);
        Assert.Equal("Kerry Ann", tenants[0].Name);
    }
    /* TODO: */
//     [Fact]
//     public async Task CannotAddTenantWithoutName() { }

//     [Fact]
//     public async Task ComplaintLinksToTenant() { }

//     [Fact]
//     public async Task GetComplaintsByStatus_ReturnsOnlyOpen() { }
}