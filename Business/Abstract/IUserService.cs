using Core.Utilities.Responses;
using Entities.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<ApiDataResponse<IEnumerable<UserDetailDTO>>> GetListAsync();
        Task<ApiDataResponse<UserDTO>> GetByIdAsync(int id);
        Task<ApiDataResponse<UserDTO>> AddAsync(UserAddDTO userAddDTO);
        Task<ApiDataResponse<UserUpdateDTO>> UpdateAsync(UserUpdateDTO userUpdateDTO);
        Task<ApiDataResponse<bool>> DeleteAsync(int id);
        //Task<ApiDataResponse<AccessToken>> Authenticate(UserForLoginDto userForLoginDto);

    }
}
