using FELearningLib.Models.M0301;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Collections.Generic;

namespace FELearningLib.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<M0301DMHoatChatModel> HoatChats { get; set; }
        public DbSet<M0301DMHangHoaModel> HangHoas { get; set; }
        public DbSet<M0301HoatChatChoHangHoaModel> HoatChatChoHangHoas { get; set; }
    }
}
