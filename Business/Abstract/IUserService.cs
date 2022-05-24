using Core.Helpers.JWT;
using Entities.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailDTO>> GetListAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> AddAsync(UserAddDTO userAddDTO);
        Task<UserUpdateDTO> UpdateAsync(UserUpdateDTO userUpdateDTO);
        Task<bool> DeleteAsync(int id);
        Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto);

    }
}
