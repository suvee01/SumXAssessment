using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SumXAssignment.Domain.Entities;
using System.Reflection.Emit;

namespace SumXAssignment.Infrastructure.Repository;

public class AppDbContext : IdentityDbContext<EUser>
{
    public DbSet<ETenant> Tenants { get; set; }
    public DbSet<EEmployee> Employees { get; set; }
    public DbSet<EUser> User { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DbContext");

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ETenant>().HasIndex(t => t.TenantId).IsUnique();

        var adminRoleId = "1";
        var adminUserId = Guid.NewGuid().ToString();

        var hasher = new PasswordHasher<EUser>();
        var admin = new EUser
        {
            Id = adminUserId,
            UserName = "admin@system.com",
            NormalizedUserName = "ADMIN@SYSTEM.COM",
            Email = "admin@system.com",
            NormalizedEmail = "ADMIN@SYSTEM.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        );
        modelBuilder.Entity<EUser>().HasData(admin);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = admin.Id
            }
        );

        modelBuilder.Entity<EUser>()
               .HasOne(u => u.Tenant)
               .WithMany(t => t.Users)
               .HasForeignKey(u => u.TenantId);

        modelBuilder.Entity<EEmployee>()
               .HasOne(e => e.Tenant)
               .WithMany()
               .HasForeignKey(e => e.TenantId);
    }
}