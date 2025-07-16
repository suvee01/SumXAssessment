using Microsoft.AspNetCore.Identity;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Domain.Interface.IQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Manager.Implementation
{
    public class UserRegistrationManager : IUserRegistrationManager
    {
        private readonly IUserCommand _userCommand;
        private readonly IUserQuery _userQuery;
        private readonly UserManager<EUser> _identityUserManager;

        public UserRegistrationManager(
            IUserCommand userCommand,
            IUserQuery userQuery,
            UserManager<EUser> identityUserManager)
        {
            _userCommand = userCommand;
            _userQuery = userQuery;
            _identityUserManager = identityUserManager;
        }

        public async Task<ResponseStatus<string>> RegisterUserAsync(UserRegistrationDto request, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.FullName))
                {
                    return Response(false, "All fields are required.", "");
                }

                // Check if tenant exists
                var tenant = await _userQuery.GetTenantByIdAsync(tenantId, cancellationToken);
                if (tenant == null)
                {
                    return Response(false, "Tenant not found.", "");
                }

                // Check if user already exists
                var existingUser = await _userQuery.UserExistsAsync(request.Email, cancellationToken);
                if (existingUser)
                {
                    return Response(false, "User with this email already exists.", "");
                }

                // Create user entity
                var user = new EUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = request.Email,
                    Email = request.Email,
                    EmailConfirmed = true,
                    TenantId = tenantId,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                // Create user with identity
                var result = await _identityUserManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Response(false, $"Failed to create user: {errors}", "");
                }

                // Assign "Tenant" role to the user
                await _identityUserManager.AddToRoleAsync(user, "Tenant");

                return Response(true, "User registered successfully.", user.Id);
            }
            catch (Exception ex)
            {
                return Response(false, "Something went wrong during user registration.", "");
            }
        }

        private ResponseStatus<string> Response(bool isSuccess, string message, string data)
        {
            return new ResponseStatus<string>
            {
                Status = isSuccess ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                Data = isSuccess ? data : "",
                Message = message
            };
        }
    }
}