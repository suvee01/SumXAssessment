using Microsoft.AspNetCore.Identity;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Infrastructure.Seed
{
    public static class UserSeed
    {
        //public static void User(AppDbContext context)
        //{
        //    var hasher = new PasswordHasher<EUser>();
        //    var user = new EUser()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = "admin@system.com",
        //        Email = "admin@system.com",
        //        EmailConfirmed = true,
        //        PhoneNumber = "1234567890",

        //    };
        //    user.PasswordHash = hasher.HashPassword(user, "Admin@123");

        //    context.User.Add(user);
        //    context.SaveChanges();
        //}
        //public static void Identity(AppDbContext context)
        //{
        //    var identityRole = new IdentityRole
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = "Admin",
        //        NormalizedName = "ADMIN"
        //    };
        //    context.IdentityRole.Add(user);
        //    context.SaveChanges();
        //}
        }
}
