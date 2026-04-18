using System.ComponentModel.DataAnnotations;
using TestApi.Models;
namespace TestApi.Dtos;



public class CreateComplaintDto
{
    [Required]
    public int TenantId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public ComplaintStatus Status { get; set; } = ComplaintStatus.Open;

    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
}