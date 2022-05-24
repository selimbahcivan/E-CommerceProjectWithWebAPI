using Business.Abstract;
using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetListAsync();
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add([FromBody] UserAddDTO userAddDTO)
        {
            var result = await _userService.AddAsync(userAddDTO);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDTO userUpdateDTO)
        {
            var result = await _userService.UpdateAsync(userUpdateDTO);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result)
                return Ok(true);
            return BadRequest(false);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Authenticate([FromBody] UserForLoginDto userForLoginDto)
        {
            var result = await _userService.Authenticate(userForLoginDto);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

    }
}
