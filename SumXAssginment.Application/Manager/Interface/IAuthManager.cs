using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.DTOs.Response;
using SumXAssginment.Application.Helper;

namespace SumXAssginment.Application.Manager.Interface
{
    public interface IAuthManager
    {
        Task<ResponseStatus<LoginResponseDto>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
        Task<ResponseStatus<string>> RegisterUserAsync(RegisterUserDto registerDto, string tenantId, CancellationToken cancellationToken);
    }
}