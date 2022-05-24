using Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
