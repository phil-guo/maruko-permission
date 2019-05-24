using Microsoft.AspNetCore.Mvc;

namespace Maruko.Permission.Host.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect($"http://{Request.Host}/document");
        }
    }
}
