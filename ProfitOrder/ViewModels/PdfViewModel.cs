using Maui.PDFView.DataSources;

namespace ProfitOrder.ViewModels
{
    public class PdfViewModel : BaseViewModel
    {
        private bool _isBusy;

        private string _filePath;
        public string filePath
        {
            get => _filePath;
            set { _filePath = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public async Task LoadPdfFromBase64()
        {
            IsBusy = true; // Show loading

            try
            {
                string fileName = "OrderID" + App.g_OrderNo + ".pdf"; // Added .pdf extension for the viewer
                filePath = Path.Combine(FileSystem.Current.CacheDirectory, fileName);
            }
            finally
            {
                IsBusy = false; // Hide loading
            }
        }
    }
}
