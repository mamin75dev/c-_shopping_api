using Microsoft.AspNetCore.Mvc;

namespace shopping.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}