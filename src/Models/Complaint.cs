namespace TestApi.Models;
public class Complaint
{
    public int Id { get; set; }
    public int TenantId { get; set; }

    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    public string Status { get; set; } = "Open";
    public string Priority { get; set; } = "Normal";

    public Tenant? Tenant { get; set; }
}