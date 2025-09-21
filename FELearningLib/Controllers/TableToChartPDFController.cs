using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FELearningLib.Controllers
{
    public class TableToChartPDFController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportPDF()
        {
            var data = new List<(string Name, int Age)>
            {
                ("Nam", 25),
                ("Lan", 23),
                ("Minh", 30),
                ("Hoa", 28)
            };

            // Vẽ chart với SkiaSharp
            byte[] chartBytes;
            using (var chartStream = new MemoryStream())
            {
                int width = 600, height = 300;
                using (var surface = SKSurface.Create(new SKImageInfo(width, height)))
                {
                    var canvas = surface.Canvas;
                    canvas.Clear(SKColors.White);

                    var paintAxis = new SKPaint { Color = SKColors.Black, StrokeWidth = 2 };
                    canvas.DrawLine(50, height - 50, width - 20, height - 50, paintAxis); // X
                    canvas.DrawLine(50, height - 50, 50, 20, paintAxis); // Y

                    var paintLine = new SKPaint { Color = SKColors.Blue, StrokeWidth = 3, IsAntialias = true };

                    var points = data.Select((d, i) => new SKPoint(50 + i * 100, height - 50 - d.Age * 5)).ToArray();
                    for (int i = 0; i < points.Length - 1; i++)
                        canvas.DrawLine(points[i], points[i + 1], paintLine);

                    var paintPoint = new SKPaint { Color = SKColors.Red, Style = SKPaintStyle.Fill };
                    foreach (var p in points)
                        canvas.DrawCircle(p, 5, paintPoint);

                    using var image = surface.Snapshot();
                    using var dataImg = image.Encode(SKEncodedImageFormat.Png, 100);
                    dataImg.SaveTo(chartStream);
                }
                chartBytes = chartStream.ToArray();
            }

            // Tạo PDF với QuestPDF
            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header().Text("Bảng và Biểu đồ tuổi").Bold().FontSize(18);

                    page.Content().Column(col =>
                    {
                        // Bảng dữ liệu
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(200);
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Tên").Bold();
                                header.Cell().Text("Tuổi").Bold();
                            });

                            foreach (var item in data)
                            {
                                table.Cell().Text(item.Name);
                                table.Cell().Text(item.Age.ToString());
                            }
                        });

                        col.Item().PaddingVertical(20);

                        // Biểu đồ
                        col.Item().Image(chartBytes);
                    });
                });
            }).GeneratePdf();

            return File(pdfBytes, "application/pdf", "Bang_BieuDo.pdf");
        }
    }
}
