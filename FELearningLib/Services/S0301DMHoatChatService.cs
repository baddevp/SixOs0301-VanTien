using FELearningLib.Models;
using FELearningLib.Models.M0301;
using Microsoft.EntityFrameworkCore;


namespace FELearningLib.Services
{
   
        public interface IS0301DMHoatChatService
        {
            Task<M0301DMHoatChatModel> AddHoatChatAsync(M0301DMHoatChatModel hoatChat);
            Task<M0301DMHoatChatModel> UpdateHoatChatAsync(M0301DMHoatChatModel hoatChat);
            Task<bool> DeactivateHoatChatAsync(long hoatChatId);
            Task<bool> ReactivateHoatChatAsync(long hoatChatId);
            Task<List<M0301DMHoatChatModel>> GetHoatChatListAsync();
            Task<IEnumerable<M0301DMHoatChatModel>> GetHoatChatChoHangHoaAsync(long hangHoaId);
    }
    public class S0301DMHoatChatService : IS0301DMHoatChatService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<S0301DMHoatChatService> _logger;
        public S0301DMHoatChatService(ApplicationDbContext context, ILogger<S0301DMHoatChatService>  logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<M0301DMHoatChatModel> AddHoatChatAsync(M0301DMHoatChatModel hoatChat)
        {

            _context.HoatChats.Add(hoatChat);
            await _context.SaveChangesAsync();
            return hoatChat;
        }

        public async Task<M0301DMHoatChatModel> UpdateHoatChatAsync(M0301DMHoatChatModel hoatChat)
        {


            _context.Entry(hoatChat).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return hoatChat;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.HoatChats.Any(e => e.ID == hoatChat.ID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeactivateHoatChatAsync(long hoatChatId)
        {
            var hoatChat = await _context.HoatChats.FindAsync(hoatChatId);
            if (hoatChat == null)
            {
                return false;
            }

            hoatChat.Active = false;
            _context.HoatChats.Update(hoatChat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactivateHoatChatAsync(long hoatChatId)
        {
            var hoatChat = await _context.HoatChats.FindAsync(hoatChatId);
            if (hoatChat == null)
            {
                return false;
            }

            hoatChat.Active = true;
            _context.HoatChats.Update(hoatChat);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<M0301DMHoatChatModel>> GetHoatChatListAsync()
        {
            return await _context.HoatChats.OrderBy(h => h.TenHoatChat).ToListAsync();
        }
        public async Task<IEnumerable<M0301DMHoatChatModel>> GetHoatChatChoHangHoaAsync(long hangHoaId)
        {
            var hoatChats = await _context.HoatChats
                                          .FromSqlRaw("EXEC S0301_GetHoatChatChoHangHoa @p0", hangHoaId)
                                          .ToListAsync();

            return hoatChats;
        }
    }
}
