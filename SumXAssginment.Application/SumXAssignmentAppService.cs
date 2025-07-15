using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SumXAssginment.Application.Manager.Implementation;
using SumXAssginment.Application.Manager.Interface;
using SumXAssginment.Application.Services;

namespace SumXAssginment.Application
{
    public static class SumXAssignmentAppService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantManager, TenantManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<TokenService>();
            return services;
        }
    }
}
