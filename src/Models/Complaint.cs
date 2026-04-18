namespace TestApi.Models;
using TestApi.Dtos;
public enum ComplaintStatus
{
    Open,
    Closed,
    Pending
}

public enum PriorityLevel
{
    Low,
    Medium,
    High
}
public class Complaint
{
    public int Id { get; set; }
    public int TenantId { get; set; }

    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    public ComplaintStatus Status { get; set; } = ComplaintStatus.Open;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;

    public Tenant? Tenant { get; set; }
}