using Core.Utilities.Responses;
using Entities.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<ApiDataResponse<UserDTO>> LoginAsync(LoginDto loginDto);
    }
}
