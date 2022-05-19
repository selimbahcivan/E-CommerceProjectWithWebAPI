using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAPIWithCoreMVC.ViewModels;

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
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.GenderList = GenderFill();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDTO userAddDTO = new UserAddDTO()
            {
                FirstName = userAddViewModel.FirstName,
                Gender = userAddViewModel.GenderID == 1 ? true : false,
                LastName = userAddViewModel.LastName,
                Address = userAddViewModel.Address,
                DateOfBirth = userAddViewModel.DateOfBirth,
                Email = userAddViewModel.Email,
                Password = userAddViewModel.Password,
                UserName = userAddViewModel.UserName

            };
            HttpResponseMessage responseMessage = await _httpClient
                .PostAsJsonAsync(url + "Users/Add", userAddDTO);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDTO>(url + "Users/GetById/" + id);

            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel()
            {
                FirstName = user.FirstName,
                GenderID = user.Gender == true ? 1 : 2,
                LastName = user.LastName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName
            };
            ViewBag.GenderList = GenderFill();

            return View(userUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UserUpdateViewModel userUpdateViewModel)
        {
            UserUpdateDTO userUpdateDTO = new UserUpdateDTO()
            {
                FirstName = userUpdateViewModel.FirstName,
                Gender = userUpdateViewModel.GenderID == 1 ? true : false,
                LastName = userUpdateViewModel.LastName,
                Address = userUpdateViewModel.Address,
                DateOfBirth = userUpdateViewModel.DateOfBirth,
                Email = userUpdateViewModel.Email,
                Password = userUpdateViewModel.Password,
                UserName = userUpdateViewModel.UserName,
                Id = id
            };
            HttpResponseMessage httpResponseMessage = await _httpClient
                .PutAsJsonAsync(url + "Users/Update", userUpdateDTO);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDTO>(url + "Users/GetById/" + id);

            UserDeleteViewModel userDeleteViewModel = new UserDeleteViewModel()
            {
                FirstName = user.FirstName,
                GenderName = user.Gender == true ? "Man" : "Woman",
                LastName = user.LastName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName
            };
            ViewBag.GenderList = GenderFill();

            return View(userDeleteViewModel);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var isDelete = await _httpClient.DeleteAsync(url + "Users/Delete/" + id);
            if (isDelete.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        private dynamic GenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender() { Id = 1, GenderName = "Man" });
            genders.Add(new Gender() { Id = 2, GenderName = "Woman" });
            return genders;
        }
        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }



    }
}
