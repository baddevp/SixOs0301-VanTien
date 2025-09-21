using Microsoft.AspNetCore.Mvc;

namespace FELearningLib.Controllers
{
    public class BasicTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
