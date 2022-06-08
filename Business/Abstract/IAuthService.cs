using Core.Utilities.Responses;
using Entities.DTOs.Auth;
using Entities.DTOs.User;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<ApiDataResponse<UserDTO>> LoginAsync(LoginDto loginDto);
    }
}
