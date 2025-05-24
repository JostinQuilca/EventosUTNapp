using Microsoft.AspNetCore.Mvc;

namespace EventosUTNapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();  // Muestra Views/Home/Index.cshtml
        }
    }
}
