using Core.Utilities.Responses;
using Entities.DTOs.Auth;
using Entities.DTOs.User;
using System.Threading.Tasks;

namespace WebAPIWithCoreMVC.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
        Task<ApiDataResponse<UserDTO>> LoginAsync(LoginDto loginDto);
    }
}
