using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using ProfitOrder.Data;
using BarcodeScanning;
using Maui.PDFView;
namespace ProfitOrder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiPdfView()
                .UseFFImageLoading()
                .UseBarcodeScanning()   
                .ConfigureFonts(fonts =>
                {
                        
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Font Awesome 5 Brands-Regular-400.otf", "FontAwesomeBrandsReg");
                    fonts.AddFont("Font Awesome 5 Free-Regular-400.otf", "FontAwesomeFreeReg");
                    fonts.AddFont("Font Awesome 5 Free-Solid-900.otf", "FontAwesomeFreeSolid");
                    fonts.AddFont("Font Awesome 6 Pro-Light-300.otf", "FontAwesomePro6Light");
                    fonts.AddFont("Font Awesome 6 Pro-Regular-400.otf", "FontAwesomePro6Regular");
                    fonts.AddFont("Font Awesome 6 Pro-Solid-900.otf", "FontAwesomePro6Solid");
                    fonts.AddFont("Font Awesome 6 Pro-Thin-100.otf", "FontAwesomePro6Thin");
                });
            try
            {
                Batteries_V2.Init();
                System.Diagnostics.Debug.WriteLine("SQLite Batteries.Init OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SQLite Init FAILED: " + ex);
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // ✅ REQUIRED
            builder.Services.AddSingleton<ISoapService>(sp =>
            {
                var httpClient = new HttpClient();
                return new SoapService(httpClient);
            });

            // Optional manager
            builder.Services.AddSingleton<CommManager>();
           
            return builder.Build();
        }
    }
}
