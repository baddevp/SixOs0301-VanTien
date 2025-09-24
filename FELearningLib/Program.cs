using FELearningLib.Models;
using FELearningLib.Services;
using Microsoft.EntityFrameworkCore;
using QuestPDF;
using QuestPDF.Infrastructure;

namespace FELearningLib
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
            builder.Services.AddScoped<S0301DynamicClassGeneratorService>();
            builder.Services.AddScoped<S0301StoreProceduceService>();

            builder.Services.AddScoped<IS0301DMHoatChatService, S0301DMHoatChatService>();
            builder.Services.AddScoped<I0301DMHangHoa, S0301DMHangHoaService>();
            Settings.License = LicenseType.Community;

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
