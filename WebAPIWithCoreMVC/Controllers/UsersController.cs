using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebAPIWithCoreMVC.Controllers
{
    public class UsersController : Controller
    {
        #region Defines
        private readonly HttpClient _httpClient;
        private string url = "http://localhost:63510/api/";
        #endregion

        #region Ctor
        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        } 
        #endregion

        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<UserDetailDTO>>(url + "Users/GetList");
            return View(users);
        }
    }
}
