using Microsoft.AspNetCore.Mvc;

namespace FELearningLib.Controllers
{
    public class ToastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
