using AutoMapper;
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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        #region DI

        private readonly IUserDal _userDal;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(IUserDal userDal, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        #endregion DI

        public async Task<ApiDataResponse<IEnumerable<UserDetailDTO>>> GetListAsync(Expression<Func<User, bool>> filter==null)
        {
            if (filter == null)
            {
                var response = await _userDal.GetListAsync();
                var userDetailDTOs = _mapper.Map<IEnumerable<UserDetailDTO>>(response);
                return new SuccessApiDataResponse<IEnumerable<UserDetailDTO>>(userDetailDTOs, Messages.Listed);
            }
            else
            {
                var response = await _userDal.GetListAsync(filter);
                var userDetailDTOs = _mapper.Map<IEnumerable<UserDetailDTO>>(response);
                return new SuccessApiDataResponse<IEnumerable<UserDetailDTO>>(userDetailDTOs, Messages.Listed);
            }
        }

        public async Task<ApiDataResponse<UserDTO>> GetAsync(Expression<Func<User, bool>> filter)
        {
            var user = await _userDal.GetAsync(filter);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDTO>(user);
                return new SuccessApiDataResponse<UserDTO>(userDto, Messages.Listed);
            }
            return new ErrorApiDataResponse<UserDTO>(null, Messages.NotListed);
        }

        public async Task<ApiDataResponse<UserDTO>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDTO>(user);
                return new SuccessApiDataResponse<UserDTO>(userDto, Messages.Listed);
            }
            return new ErrorApiDataResponse<UserDTO>(null, Messages.NotListed);
        }

        public async Task<ApiDataResponse<UserDTO>> AddAsync(UserAddDTO userAddDTO)
        {
            var user = _mapper.Map<User>(userAddDTO);
            user.CreatedDate = DateTime.Now;
            user.CreatedUserId = 1;
            var userAdd = await _userDal.AddAsync(user);
            var userDto = _mapper.Map<UserDTO>(userAdd);

            return new SuccessApiDataResponse<UserDTO>(userDto, Messages.Added);
        }

        public async Task<ApiDataResponse<UserUpdateDTO>> UpdateAsync(UserUpdateDTO userUpdateDTO)
        {
            var getUser = await _userDal.GetAsync(x => x.Id == userUpdateDTO.Id);
            var user = _mapper.Map<User>(userUpdateDTO);
            user.CreatedDate = getUser.CreatedDate;
            user.CreatedUserId = getUser.CreatedUserId;
            user.UpdatedTime = getUser.UpdatedTime;
            user.UpdatedUserId = getUser.UpdatedUserId;
            user.Token = userUpdateDTO.Token;
            user.TokenExpireDate = userUpdateDTO.TokenExpireDate;
            var resultUpdate = await _userDal.UpdateAsync(user);
            var userUpdateMap = _mapper.Map<UserUpdateDTO>(resultUpdate);

            return new SuccessApiDataResponse<UserUpdateDTO>(userUpdateMap, Messages.Updated);
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