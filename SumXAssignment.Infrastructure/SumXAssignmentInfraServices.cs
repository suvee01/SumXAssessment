using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Domain.Interface.IQuery;
using SumXAssignment.Infrastructure.Repository;
using SumXAssignment.Infrastructure.Services.Command;
using SumXAssignment.Infrastructure.Services.Query;

namespace SumXAssignment.Infrastructure
{
    public static class SumXAssignmentInfraServices
    {
        public static IServiceCollection AddSumXAssignmentInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(configuration["ConnectionStrings:DbContext"]), ServiceLifetime.Scoped);
            services.AddScoped<ITenantCommand, TenantCommand>();
            services.AddScoped<IUserCommand, UserCommand>();
            services.AddScoped<ITenantQuery, TenantQuery>();

            return services;
        }
    }
}
