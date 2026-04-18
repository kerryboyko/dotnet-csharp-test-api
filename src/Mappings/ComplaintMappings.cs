using TestApi.Dtos;
using TestApi.Models;

namespace TestApi.Mappings;

public static class ComplaintMappings
{
    public static Complaint ToEntity(this CreateComplaintDto dto)
    {
        return new Complaint
        {
            TenantId = dto.TenantId,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority
        };
    }
}