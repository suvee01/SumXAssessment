using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.DTOs.Response;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using SumXAssginment.Application.Services;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Domain.Interface.IQuery;
using System.Net;

namespace SumXAssginment.Application.Manager.Implementation
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserQuery _userQuery;
        private readonly IUserCommand _userCommand;
        private readonly TokenService _tokenService;

        public AuthManager(IUserQuery userQuery, IUserCommand userCommand, TokenService tokenService)
        {
            _userQuery = userQuery;
            _userCommand = userCommand;
            _tokenService = tokenService;
        }

        public async Task<ResponseStatus<LoginResponseDto>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQuery.GetUserByEmailAsync(loginDto.Email, cancellationToken);
                if (user == null)
                {
                    return new ResponseStatus<LoginResponseDto>
                    {
                        Status = (int)HttpStatusCode.Unauthorized,
                        Message = "Invalid email or password",
                        Data = null
                    };
                }

                var isPasswordValid = await _userQuery.CheckPasswordAsync(user, loginDto.Password, cancellationToken);
                if (!isPasswordValid)
                {
                    return new ResponseStatus<LoginResponseDto>
                    {
                        Status = (int)HttpStatusCode.Unauthorized,
                        Message = "Invalid email or password",
                        Data = null
                    };
                }

                var userRole = await _userQuery.GetUserRoleAsync(user.Id, cancellationToken);
                var token = _tokenService.GenerateToken(user, userRole ?? "User");

                var response = new LoginResponseDto
                {
                    Token = token,
                    UserId = user.Id,
                    Email = user.Email!,
                    Role = userRole ?? "User",
                    TenantId = user.TenantId,
                    ExpiresAt = DateTime.UtcNow.AddHours(24)
                };

                return new ResponseStatus<LoginResponseDto>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Login successful",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<LoginResponseDto>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred during login",
                    Data = null
                };
            }
        }

        public async Task<ResponseStatus<string>> RegisterUserAsync(RegisterUserDto registerDto, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userQuery.GetUserByEmailAsync(registerDto.Email, cancellationToken);
                if (existingUser != null)
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "User with this email already exists",
                        Data = string.Empty
                    };
                }

                var user = new EUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    EmailConfirmed = true,
                    TenantId = tenantId
                };

                var userId = await _userCommand.CreateUserWithPasswordAsync(user, registerDto.Password, cancellationToken);
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Failed to create user",
                        Data = string.Empty
                    };
                }

                // Assign Tenant role
                var roleId = await _userCommand.GetRoleIdByNameAsync("Tenant", cancellationToken);
                if (string.IsNullOrEmpty(roleId))
                {
                    roleId = await _userCommand.AddTenantRoleAsync("Tenant", cancellationToken);
                }

                await _userCommand.AddUserRoleAsync(userId, roleId, cancellationToken);

                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "User registered successfully",
                    Data = userId
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred during registration",
                    Data = string.Empty
                };
            }
        }
    }
}