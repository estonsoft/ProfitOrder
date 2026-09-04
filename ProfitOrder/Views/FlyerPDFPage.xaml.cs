namespace ProfitOrder.Views
{
    public partial class FlyerPDFPage : ContentPage
    {
        public FlyerPDFPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("InitializeComponent Error " + Environment.NewLine + ex.ToString() + Environment.NewLine + ex.StackTrace);
            }

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            App.g_CurrentPage = "FlyerPDFPage";

            try
            {
                pdfViewerControl.Uri = App.g_FlyerFilename;
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Pitco Foods", ex.Message, "Ok");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        // private async void pdfViewerControl_Tapped(object sender, Syncfusion.Maui.PdfViewer.GestureEventArgs e)
        // {
        //     try
        //     {
        //         if (await App.g_db.GetFlyerItemCount() <= 1)
        //         {
        //             return;
        //         }

        //         App.g_IsMonthlyAdPDFClick = true;
        //         App.g_MonthlyAdPage = e.PageNumber;

        //         if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        //         {
        //             App.g_MonthlyAdX = (int)(e.Position.X * 1.33);
        //             App.g_MonthlyAdY = (int)(e.Position.Y * 1.33);
        //         }
        //         else // Android 
        //         {
        //             App.g_MonthlyAdX = (int)(e.Position.X);
        //             App.g_MonthlyAdY = (int)(e.Position.Y);
        //         }

        //         List<Item> lstItems = await App.g_db.SearchItemsMonthlyAdClick(App.g_MonthlyAdPage, App.g_MonthlyAdX, App.g_MonthlyAdY);

        //         if (lstItems.Count > 0)
        //         {
        //             App.g_SearchFromPage = "FlyerPDFPage";
        //             await App.g_Shell.GoToItemSearch();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         await DisplayAlertAsync("Profit Order", ex.Message, "Ok");
        //     }
        // }
    }
}