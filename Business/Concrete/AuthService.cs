using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Responses;
using Core.Utilities.Security.Token;
using Entities.DTOs.Auth;
using Entities.DTOs.User;
using System;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private IMapper _mapper;
        public AuthService(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<UserDTO>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.GetAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
            if (user.Data == null)
            {
                return new ErrorApiDataResponse<UserDTO>(null, Messages.UserNotFound);
            }
            else
            {
                if (user.Data.TokenExpireDate == null || String.IsNullOrEmpty(user.Data.Token))
                {
                    return await UpdateToken(user);
                }

                if (user.Data.TokenExpireDate < DateTime.Now)
                {
                    return await UpdateToken(user);
                }
            }
            return new SuccessApiDataResponse<UserDTO>(user.Data, Messages.SystemLoginSuccesful);
        }

        private async Task<ApiDataResponse<UserDTO>> UpdateToken(ApiDataResponse<UserDTO> user)
        {
            var accessToken = _tokenService.CreateToken(user.Data.Id, user.Data.UserName);
            var userUpdateDto = _mapper.Map<UserUpdateDTO>(user.Data);
            userUpdateDto.Token = accessToken.Token;
            userUpdateDto.TokenExpireDate = accessToken.Expiration;
            userUpdateDto.UpdatedUserId = user.Data.Id;
            var resultUserUpdateDto = await _userService.UpdateAsync(userUpdateDto);
            var userDto = _mapper.Map<UserDTO>(resultUserUpdateDto.Data);
            return new SuccessApiDataResponse<UserDTO>(userDto, Messages.SystemLoginSuccesful);
        }
    }
}
