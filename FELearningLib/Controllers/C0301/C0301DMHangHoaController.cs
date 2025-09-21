
using FELearningLib.Services;
using Microsoft.AspNetCore.Mvc;

namespace FELearningLib.Controllers
{
    [Route("HangHoa")]
    public class C0301DMHangHoaController : Controller
    {
        //private string _maChucNang = "/HangHoa";
        //private IMemoryCachingServices _memoryCache;
        private readonly I0301DMHangHoa _hangHoaService;
        private readonly IS0301DMHoatChatService _hoatChatService;
        private readonly ILogger<C0301DMHangHoaController> _logger;

        public C0301DMHangHoaController(I0301DMHangHoa hangHoaService, ILogger<C0301DMHangHoaController> logger, IS0301DMHoatChatService hoatChatService /*, IMemoryCachingServices memoryCache*/)

        {
            _hangHoaService = hangHoaService;
            _logger = logger;
            _hoatChatService = hoatChatService;
        }

        public async Task<IActionResult> Index()
        {
            return View("~/Views/V0301DMHangHoa/Index.cshtml");
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(bool? status)
        {
            var hangHoas = await _hangHoaService.GetDanhSachHangHoaAsync();
            var hoatChats = await _hoatChatService.GetHoatChatListAsync();
            if (status.HasValue)
            {
                hangHoas = hangHoas.Where(h => h.Active == status.Value).ToList();
                hoatChats = hoatChats.Where(h => h.Active == status.Value).ToList();
            }
            return Ok(new
            {
                statusCode = 200,
                message = "Lấy danh sách hàng hóa thành công.",
                dataHangHoa = hangHoas,
                dataHoatChat = hoatChats
            });
        }
        [HttpGet("GetHoatChatChoHangHoa/{hangHoaId}")]
        public async Task<IActionResult> GetHoatChatChoHangHoa(long hangHoaId)
        {
            if (hangHoaId <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "ID hàng hóa không hợp lệ."
                });
            }

            try
            {
                var hoatChats = await _hoatChatService.GetHoatChatChoHangHoaAsync(hangHoaId);

                if (hoatChats == null || !hoatChats.Any())
                {
                    return Ok(new
                    {
                        statusCode = 200,
                        message = "Không tìm thấy hoạt chất nào cho hàng hóa này.",
                        data = new List<object>() 
                    });
                }

                return Ok(new
                {
                    statusCode = 200,
                    message = "Lấy danh sách hoạt chất thành công.",
                    data = hoatChats
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = "Lỗi máy chủ nội bộ."
                });
            }
        }


        public class UpdateHoatChatDto
        {
            public long HangHoaId { get; set; }
            public string HoatChatNoiChuoi { get; set; }
            public List<long> HoatChatIds { get; set; }
        }

        [HttpPost]
        [Route("UpdateHoatChat")]
        public async Task<IActionResult> UpdateHoatChat([FromBody] UpdateHoatChatDto model)
        {
            if (model == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            _logger.LogWarning($"HangHoaId = {model.HangHoaId}, HoatChatNoiChuoi = {model.HoatChatNoiChuoi}, model.HoatChatIds= {model.HoatChatIds.ToString} ");
            try
            {

                await _hangHoaService.CapNhatHoatChatChoHangHoa(
                    model.HangHoaId,
                    model.HoatChatNoiChuoi,
                    model.HoatChatIds
                );

                return Ok(new { message = "Cập nhật thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Cập nhật thất bại: " + ex.Message });
            }
        }

    }
}