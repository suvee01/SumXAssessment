namespace SumXAssignment.Domain.Entities;

public class EEmployee
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FullName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string TenantId { get; set; } = default!;
    public ETenant? Tenant { get; set; }
}