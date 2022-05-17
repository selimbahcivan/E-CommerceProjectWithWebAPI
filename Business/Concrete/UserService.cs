using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        public async Task<IEnumerable<UserDetailDTO>> GetListAsync()
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
            return userDetailDTOs;
        }
        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            UserDTO userDTO = new UserDTO()
            {
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                Gender = user.Gender,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName
            };
            return userDTO;
        }    
        public async Task<UserDTO> AddAsync(UserAddDTO userAddDTO)
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
            return userDTO;
        }
        public async Task<UserUpdateDTO> UpdateAsync(UserUpdateDTO userUpdateDTO)
        {
            var getUser = await _userDal.GetAsync(x=>x.Id==userUpdateDTO.Id);
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
            return newUserUpdateDTO; 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userDal.DeleteAsync(id);
        }


    }
}
