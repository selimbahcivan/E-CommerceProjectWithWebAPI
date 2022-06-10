using Microsoft.AspNetCore.Mvc;

namespace WebAPIWithCoreMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ErrorController : Controller
    {
        public IActionResult MyStatusCode(int code)
        {
            if (code == 404)
            {
                ViewBag.ErrorMessage = "Page Not Found!";
                return RedirectToAction("Error404", "Error", new { area = "Admin" });
            }
            if (code == 500)
            {
                ViewBag.ErrorMessage = "Server Error!";
                return RedirectToAction("InternalServerError500", "Error", new { area = "Admin" });
            }
            if (code == 401)
            {
                ViewBag.ErrorMessage = "Unauthorized Access!";
                return RedirectToAction("Unauthorize401", "Error", new { area = "Admin" });
            }
            if (code == 400)
            {
                ViewBag.ErrorMessage = "Invalid Request!";
                return RedirectToAction("BadRequest400", "Error", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occurred while processing your transaction.!";
            }
            ViewBag.ErrorStatusCode = code;
            return View();
        }
        public IActionResult Error404(int code)
        {
            return View();
        }
        public IActionResult InternalServerError500(int code)
        {
            return View();
        }
        public IActionResult Unauthorize401(int code)
        {
            return View();
        }
        public IActionResult BadRequest400(int code)
        {
            return View();
        }
    }
}
