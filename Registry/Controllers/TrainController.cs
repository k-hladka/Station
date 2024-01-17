using Microsoft.AspNetCore.Mvc;

namespace Registry.Controllers
{
    public class TrainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
