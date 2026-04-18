using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using TestApi.Data;
using TestApi.Controllers;
using TestApi.Dtos;
using TestApi.Models;


public class ComplaintControllerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        var db = new AppDbContext(options);

        return db;
    }
    private Tenant AddTenantToDb(AppDbContext db)
    {
        var tenant = new Tenant
        {
            Name = "Test Tenant"
        };

        db.Tenants.Add(tenant);
        db.SaveChanges();

        return tenant;
    }


    [Fact]
    public async Task Create_Should_Add_Complaint()
    {
        var db = GetDbContext();
        var tenant = AddTenantToDb(db);

        var controller = new ComplaintsController(db);

        var dto = new CreateComplaintDto
        {
            TenantId = tenant.Id,
            Title = "Leaky faucet",
            Description = "Drip drip",
            Status = ComplaintStatus.Open,
            Priority = PriorityLevel.Medium
        };

        var result = await controller.Create(dto);

        var complaints = db.Complaints.ToList();

        Assert.Single(complaints);
        Assert.Equal("Leaky faucet", complaints[0].Title);
    }

    [Fact]
    public async Task Get_Should_Filter_By_Status()
    {
        var db = GetDbContext();
        var tenant = AddTenantToDb(db);

        db.Complaints.AddRange(
            new Complaint
            {
                TenantId = tenant.Id,
                Title = "A",
                Description = "Test",
                Status = ComplaintStatus.Open,
                Priority = PriorityLevel.Medium
            },
            new Complaint
            {
                TenantId = tenant.Id,
                Title = "B",
                Description = "Test",
                Status = ComplaintStatus.Closed,
                Priority = PriorityLevel.Medium
            }
        );

        await db.SaveChangesAsync();

        var controller = new ComplaintsController(db);

        var result = await controller.Get(ComplaintStatus.Open, null, null);

        Assert.Single(result);
        Assert.Equal("A", result.First().Title);
    }
    [Fact]
    public async Task GetById_Should_Return_Complaint()
    {
        var db = GetDbContext();
        var tenant = AddTenantToDb(db);

        var complaint = new Complaint
        {
            TenantId = tenant.Id,
            Title = "A",
            Description = "Test",
            Status = ComplaintStatus.Open,
            Priority = PriorityLevel.Medium
        };

        db.Complaints.Add(complaint);
        await db.SaveChangesAsync();

        var controller = new ComplaintsController(db);

        var result = await controller.GetById(complaint.Id);

        var value = Assert.IsType<Complaint>(result.Value);
        Assert.Equal("A", value.Title);
        Assert.Equal("Test", value.Description);

    }
    [Fact]
    public async Task GetById_Should_Return_NotFound()
    {
        var db = GetDbContext();
        var controller = new ComplaintsController(db);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}