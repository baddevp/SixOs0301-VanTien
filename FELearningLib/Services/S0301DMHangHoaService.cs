using FELearningLib.Models;
using FELearningLib.Models.M0301;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using static FELearningLib.Services.S0301DMHangHoaService;

namespace FELearningLib.Services
{
    public interface I0301DMHangHoa
    {
       
        Task<List<M0301DMHangHoaModel>> GetDanhSachHangHoaAsync();
        Task CapNhatHoatChatChoHangHoa(long idHangHoa, string hoatChatNoiChuoi, List<long> listIdHoatChat);

    }
    public class S0301DMHangHoaService : I0301DMHangHoa
    {
        private readonly ApplicationDbContext _dbContext;

        public S0301DMHangHoaService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<M0301DMHangHoaModel>> GetDanhSachHangHoaAsync()
        {
            return await _dbContext.HangHoas.OrderByDescending(h => h.ID)
            .Take(10)
            .ToListAsync();
        }
        public async Task CapNhatHoatChatChoHangHoa(long idHangHoa, string hoatChatNoiChuoi, List<long> listIdHoatChat)
        {
            try
            {
                var idListString = string.Join(",", listIdHoatChat);

                var pIdHangHoa = new SqlParameter("@IdHangHoa", idHangHoa);
                var pHoatChatNoiChuoi = new SqlParameter("@HoatChatNoiChuoi", hoatChatNoiChuoi);
                var pListIdHoatChat = new SqlParameter("@ListIdHoatChat", idListString);

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC S0301_CapNhatHoatChatChoHangHoa @IdHangHoa, @HoatChatNoiChuoi, @ListIdHoatChat",
                    pIdHangHoa,
                    pHoatChatNoiChuoi,
                    pListIdHoatChat
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật hoạt chất cho hàng hóa 11.", ex);
            }
        }
           
        
    }
}
