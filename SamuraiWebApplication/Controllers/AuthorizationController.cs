using Microsoft.AspNetCore.Mvc;

namespace SamuraiWebApplication.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}