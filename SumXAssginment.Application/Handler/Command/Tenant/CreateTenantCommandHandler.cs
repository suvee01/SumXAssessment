using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Handler.Command.Tenant
{
    internal class CreateTenantCommandHandler {  //: IRequestHandler<CreateTenantCommand, string>
    //{
    //    private readonly  _context;
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly RoleManager<IdentityRole> _roleManager;

    //    public CreateTenantCommandHandler(
    //        ApplicationDbContext context,
    //        UserManager<ApplicationUser> userManager,
    //        RoleManager<IdentityRole> roleManager)
    //    {
    //        _context = context;
    //        _userManager = userManager;
    //        _roleManager = roleManager;
    //    }

    //    public async Task<string> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    //    {
    //        // Generate next TenantId like T1, T2...
    //        var lastTenant = await _context.Tenants
    //            .OrderByDescending(t => t.TenantId)
    //            .FirstOrDefaultAsync(cancellationToken);

    //        int nextNumber = 1;
    //        if (lastTenant != null && int.TryParse(lastTenant.TenantId[1..], out var lastNum))
    //            nextNumber = lastNum + 1;

    //        var generatedTenantId = $"T{nextNumber}";

    //        var tenant = new Tenant
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Name = request.Name,
    //            EmailAddress = request.EmailAddress,
    //            TenantId = generatedTenantId
    //        };

    //        _context.Tenants.Add(tenant);
    //        await _context.SaveChangesAsync(cancellationToken);

    //        // Ensure "Tenant" role exists
    //        if (!await _roleManager.RoleExistsAsync("Tenant"))
    //        {
    //            await _roleManager.CreateAsync(new IdentityRole("Tenant"));
    //        }

    //        // Create default tenant user
    //        var defaultUser = new ApplicationUser
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            UserName = tenant.EmailAddress,
    //            Email = tenant.EmailAddress,
    //            EmailConfirmed = true,
    //            TenantId = tenant.Id,
    //            FullName = "Default Tenant Admin"
    //        };

    //        string defaultPassword = "Tenant" + tenant.TenantId;

    //        var result = await _userManager.CreateAsync(defaultUser, defaultPassword);
    //        if (!result.Succeeded)
    //        {
    //            throw new Exception("Failed to create default tenant user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    //        }

    //        await _userManager.AddToRoleAsync(defaultUser, "Tenant");

    //        return tenant.Id;
        }
    //}
}
