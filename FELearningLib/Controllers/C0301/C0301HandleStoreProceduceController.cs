using FELearningLib.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FELearningLib.Controllers.C0301
{
    [Route("HandleStore")]
    public class C0301HandleStoreProceduceController : Controller
    {
        private readonly S0301StoreProceduceService _spService;
        private readonly S0301DynamicClassGeneratorService _classGenerator;
        private readonly IWebHostEnvironment _env;

        public C0301HandleStoreProceduceController(S0301StoreProceduceService spService, S0301DynamicClassGeneratorService classGenerator, IWebHostEnvironment env)
        {
            _spService = spService;
            _classGenerator = classGenerator;
            _env = env;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/V0301HandleStore/Index.cshtml");
        }

        [HttpPost("create-and-get")]
        public async Task<IActionResult> CreateAndGet([FromBody] CreateStoredProcedureRequest request)
        {
            var creationResult = await _spService.CreateDynamicStoredProcedure(request.SpName, request.PropertyNames);
            if (!creationResult.Contains("thành công"))
            {
                return BadRequest(new { message = creationResult });
            }
            var solutionName = Assembly.GetEntryAssembly().GetName().Name; //Tên namespace hiện tại
            //var solutionDirectory = _env.ContentRootPath; // đường dẫn project
            //var generationResult = await _classGenerator.GenerateClassFileFromStoredProcedure(request.SpName, solutionDirectory, solutionName);

            var pathCreate = "E://SixOs_Project//SixOsTemplateFE//FELearningLib//ABC";
            var generationResult = await _classGenerator.GenerateClassFileFromStoredProcedure(request.SpName, pathCreate, solutionName);

            return Ok(new
            {
                message = "Tạo store và class thành công!",
                data = generationResult
            });
        }
    }

    public class CreateStoredProcedureRequest
    {
        public string SpName { get; set; }
        public List<string> PropertyNames { get; set; }
    }
}

