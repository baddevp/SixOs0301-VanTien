using FELearningLib.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FELearningLib.Controllers.C0301
{
    [Route("GiayKhamSucKhoe")]
    public class C0301GiayKhamSucKhoeController : Controller
    {
        //private readonly S0301StoreProceduceService _spService;
        //private readonly S0301DynamicClassGeneratorService _classGenerator;
        //private readonly IWebHostEnvironment _env;

        //public C0301GiayKhamSucKhoeController(S0301StoreProceduceService spService, S0301DynamicClassGeneratorService classGenerator, IWebHostEnvironment env)
        //{
        //    _spService = spService;
        //    _classGenerator = classGenerator;
        //    _env = env;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/V0301GiayKhamSucKhoeLaiXe/Index.cshtml");
        }
    }
}

