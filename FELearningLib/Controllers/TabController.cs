using Microsoft.AspNetCore.Mvc;

namespace FELearningLib.Controllers
{
    public class TabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
