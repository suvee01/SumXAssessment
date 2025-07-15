namespace SumXAssginment.Application.DTOs.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string? TenantId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}