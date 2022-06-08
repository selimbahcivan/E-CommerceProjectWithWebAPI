using Microsoft.AspNetCore.Mvc;

namespace WebAPIWithCoreMVC.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
