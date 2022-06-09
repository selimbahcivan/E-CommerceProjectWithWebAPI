using Core.Utilities.Responses;
using Entities.DTOs.Auth;
using Entities.DTOs.User;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAPIWithCoreMVC.ApiServices.Interfaces;

namespace WebAPIWithCoreMVC.ApiServices
{
    public class AuthApiService : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiDataResponse<UserDTO>> LoginAsync(LoginDto loginDto)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("Auths/Login", loginDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var data = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiDataResponse<UserDTO>>(data);
                return await Task.FromResult(result);
            }
            return null;
        }
    }
}
