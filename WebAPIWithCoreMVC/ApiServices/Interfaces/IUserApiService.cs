using Entities.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPIWithCoreMVC.ApiServices.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserDetailDTO>> GetListAsync(); 
    }
}
