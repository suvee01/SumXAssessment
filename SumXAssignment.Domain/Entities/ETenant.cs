namespace SumXAssignment.Domain.Entities;

public class ETenant
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string TenantId { get; set; } = default!;
    public ICollection<EUser>? Users { get; set; }
}