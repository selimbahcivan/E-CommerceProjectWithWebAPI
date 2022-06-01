using Business.Abstract;
using Business.Constants;
using Core.Utilities.Responses;
using Core.Utilities.Security.Token;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        #region DI
        private readonly IUserDal _userDal;
        private readonly AppSettings _appSettings;
        public UserService(IUserDal userDal, IOptions<AppSettings> appSettings)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
        } 
        #endregion
        public async Task<ApiDataResponse<IEnumerable<UserDetailDTO>>> GetListAsync()
        {
            List<UserDetailDTO> userDetailDTOs = new List<UserDetailDTO>();
            var response = await _userDal.GetListAsync();
            foreach (var item in response.ToList())
            {
                userDetailDTOs.Add(new UserDetailDTO()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender == true ? "Erkek" : "Kadın",
                    DateOfBirth = item.DateOfBirth,
                    UserName = item.UserName,
                    Address = item.Address,
                    Email = item.Email,
                    Id = item.Id,
                });
            }
            return new SuccessApiDataResponse<IEnumerable<UserDetailDTO>>(userDetailDTOs, Messages.Listed);
        }
        public async Task<ApiDataResponse<UserDTO>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user != null)
            {
                UserDTO userDTO = new UserDTO()
                {
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Gender = user.Gender,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName,

                };
                return new SuccessApiDataResponse<UserDTO>(userDTO, Messages.Listed);
            }
            return new ErrorApiDataResponse<UserDTO>(null, Messages.NotListed);
        }
        public async Task<ApiDataResponse<UserDTO>> AddAsync(UserAddDTO userAddDTO)
        {
            User user = new User()
            {
                LastName = userAddDTO.LastName,
                Address = userAddDTO.Address,
                //Todo : CreatedDate ve CreatedUserId düzenlenecek
                CreatedDate = DateTime.Now,
                CreatedUserId = 1,
                DateOfBirth = userAddDTO.DateOfBirth,
                Email = userAddDTO.Email,
                FirstName = userAddDTO.FirstName,
                Gender = userAddDTO.Gender,
                Password = userAddDTO.Password,
                UserName = userAddDTO.UserName
            };
            var userAdd = await _userDal.AddAsync(user);

            UserDTO userDTO = new UserDTO()
            {
                LastName = userAdd.LastName,
                Address = userAdd.Address,
                DateOfBirth = userAdd.DateOfBirth,
                Email = userAdd.Email,
                FirstName = userAdd.FirstName,
                Gender = userAdd.Gender,
                UserName = userAdd.UserName,
                Id = userAdd.Id
            };
            return new SuccessApiDataResponse<UserDTO>(userDTO, Messages.Added);
        }
        public async Task<ApiDataResponse<UserUpdateDTO>> UpdateAsync(UserUpdateDTO userUpdateDTO)
        {
            var getUser = await _userDal.GetAsync(x => x.Id == userUpdateDTO.Id);
            User user = new User()
            {
                LastName = userUpdateDTO.LastName,
                Address = userUpdateDTO.Address,
                DateOfBirth = userUpdateDTO.DateOfBirth,
                Email = userUpdateDTO.Email,
                FirstName = userUpdateDTO.FirstName,
                Gender = userUpdateDTO.Gender,
                UserName = userUpdateDTO.UserName,
                Id = userUpdateDTO.Id,
                CreatedDate = getUser.CreatedDate,
                CreatedUserId = getUser.CreatedUserId,
                Password = userUpdateDTO.Password,
                UpdatedTime = DateTime.Now,
                UpdatedUserId = 1
            };
            var userUpdate = await _userDal.UpdateAsync(user);
            UserUpdateDTO newUserUpdateDTO = new UserUpdateDTO()
            {
                LastName = userUpdate.LastName,
                Address = userUpdate.Address,
                DateOfBirth = userUpdate.DateOfBirth,
                Email = userUpdate.Email,
                FirstName = userUpdate.FirstName,
                Gender = userUpdate.Gender,
                UserName = userUpdate.UserName,
                Id = userUpdate.Id,
                Password = userUpdate.Password,
            };
            return new SuccessApiDataResponse<UserUpdateDTO>(newUserUpdateDTO, Messages.Updated);
        }
        public async Task<ApiDataResponse<bool>> DeleteAsync(int id)
        {
            var isDelete = await _userDal.DeleteAsync(id);
            return new SuccessApiDataResponse<bool>(isDelete, Messages.Deleted);
        }

        //public async Task<ApiDataResponse<AccessToken>> Authenticate(UserForLoginDto userForLoginDto)
        //{
        //    var user = await _userDal.GetAsync(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);
        //    if (user == null)
        //        return null;
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    AccessToken accessToken = new AccessToken()
        //    {
        //        Token = tokenHandler.WriteToken(token),
        //        UserName = user.UserName,
        //        Expiration = (DateTime)tokenDescriptor.Expires,
        //        UserId = user.Id
        //    };

        //    return await Task.Run(() => accessToken);
        //}
    }
}

