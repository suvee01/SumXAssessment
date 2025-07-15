using Microsoft.AspNetCore.Identity;

namespace SumXAssignment.Domain.Entities;

public class EUser : IdentityUser
{
    public string? TenantId { get; set; }
    public ETenant? Tenant { get; set; }
}