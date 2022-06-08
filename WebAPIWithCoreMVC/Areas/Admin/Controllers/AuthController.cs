using Microsoft.AspNetCore.Mvc;

namespace WebAPIWithCoreMVC.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
