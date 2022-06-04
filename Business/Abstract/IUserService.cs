using Core.Utilities.Responses;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<ApiDataResponse<IEnumerable<UserDetailDTO>>> GetListAsync(Expression<Func<User, bool>> filter = null);

        Task<ApiDataResponse<UserDTO>> GetAsync(Expression<Func<User, bool>> filter);

        Task<ApiDataResponse<UserDTO>> GetByIdAsync(int id);

        Task<ApiDataResponse<UserDTO>> AddAsync(UserAddDTO userAddDTO);

        Task<ApiDataResponse<UserUpdateDTO>> UpdateAsync(UserUpdateDTO userUpdateDTO);

        Task<ApiDataResponse<bool>> DeleteAsync(int id);

        //Task<ApiDataResponse<AccessToken>> Authenticate(UserForLoginDto userForLoginDto);
    }
}