using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SumXAssginment.Application.Manager.Implementation;
using SumXAssginment.Application.Manager.Interface;

namespace SumXAssginment.Application
{
    public static class SumXAssignmentAppService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantManager, TenantManager>();
            services.AddScoped<IUserRegistrationManager, UserRegistrationManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            return services;
        }
    }
}
