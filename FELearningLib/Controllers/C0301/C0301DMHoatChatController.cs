using FELearningLib.Models.M0301;
using FELearningLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace FELearningLib.Controllers
{
    [Route("HoatChat")]
    public class C0301DMHoatChatController : Controller
    {
        //private string _maChucNang = "/HoatChat";
        //private IMemoryCachingServices _memoryCache;
        private readonly IS0301DMHoatChatService _hoatChatService;
        private readonly ILogger<C0301DMHoatChatController> _logger;

        public C0301DMHoatChatController(IS0301DMHoatChatService hoatChatService, ILogger<C0301DMHoatChatController> logger /*, IMemoryCachingServices memoryCache*/)
        {
            _hoatChatService = hoatChatService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View("~/Views/V0301DMHoatChat/Index.cshtml");
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(bool? status)
        {
            var hoatChats = await _hoatChatService.GetHoatChatListAsync();
            if (status.HasValue)
            {
                hoatChats = hoatChats.Where(h => h.Active == status.Value).ToList();
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Lấy danh sách hoạt chất thành công.",
                data = hoatChats
            });
        }

        [HttpPost("AddHoatChat")]
        public async Task<IActionResult> AddHoatChat([FromBody] M0301DMHoatChatModel hoatChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Dữ liệu không hợp lệ."
                });
            }

            var newHoatChat = await _hoatChatService.AddHoatChatAsync(hoatChat);

            return Ok(new
            {
                statusCode = 200,
                message = "Thêm hoạt chất thành công.",
                data = newHoatChat
            });
        }

        [HttpPut("UpdateHoatChat/{id}")]
        public async Task<IActionResult> UpdateHoatChat(long id, [FromBody] M0301DMHoatChatModel hoatChat)
        {
            if (id != hoatChat.ID)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "ID không khớp."
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Dữ liệu không hợp lệ."
                });
            }

            var result = await _hoatChatService.UpdateHoatChatAsync(hoatChat);
            if (result == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Không tìm thấy hoạt chất để cập nhật."
                });
            }

            return Ok(new
            {
                statusCode = 200,
                message = "Cập nhật hoạt chất thành công."
            });
        }

        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateHoatChat(long id)
        {
            var result = await _hoatChatService.DeactivateHoatChatAsync(id);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Không tìm thấy hoạt chất để tạm dừng."
                });
            }

            return Ok(new
            {
                statusCode = 200,
                message = "Tạm dừng hoạt chất thành công."
            });
        }
        [HttpPut("Reactivate/{id}")]
        public async Task<IActionResult> ReactivateHoatChat(long id)
        {
            var result = await _hoatChatService.ReactivateHoatChatAsync(id);
            if (!result)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Không tìm thấy hoạt chất để tạm dừng."
                });
            }

            return Ok(new
            {
                statusCode = 200,
                message = "Khôi phục hoạt chất thành công."
            });
        }
    }
}