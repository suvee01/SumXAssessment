using System.Security.Claims;

namespace SumXAssessment.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetTenantId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("TenantId")?.Value;
        }

        public static string? GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string? GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Admin");
        }

        public static bool IsTenant(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Tenant");
        }
    }
}