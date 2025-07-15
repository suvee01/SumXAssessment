using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SumXAssignment.Infrastructure.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Infrastructure.Repository
{
    public class DbMigrations
    {
        public static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = new AppDbContext(serviceScope.ServiceProvider.GetService<
                           DbContextOptions<AppDbContext>>()))
                {
                    context.Database.Migrate();
                   // UserSeed.User(context);
                }
            }
        }
    }
}
