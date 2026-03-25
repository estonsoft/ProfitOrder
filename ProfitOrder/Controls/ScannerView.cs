using Scandit.DataCapture.Barcode.Capture;
using Scandit.DataCapture.Core.Capture;
using Scandit.DataCapture.Core.UI.Viewfinder;

namespace ProfitOrder.Controls
{
    public class ScannerView : ContentView
    {
        public static readonly BindableProperty DataCaptureContextProperty = BindableProperty.Create(
            nameof(DataCaptureContext),
            typeof(DataCaptureContext),
            typeof(ScannerView),
            default(DataCaptureContext));

        public static readonly BindableProperty BarcodeCaptureProperty = BindableProperty.Create(
            nameof(BarcodeCapture),
            typeof(BarcodeCapture),
            typeof(ScannerView),
            default(BarcodeCapture));

        public static readonly BindableProperty ViewfinderProperty = BindableProperty.Create(
            nameof(Viewfinder),
            typeof(IViewfinder),
            typeof(ScannerView),
            default(IViewfinder));

        public DataCaptureContext DataCaptureContext
        {
            get => (DataCaptureContext)this.GetValue(DataCaptureContextProperty);
            set => this.SetValue(DataCaptureContextProperty, value);
        }

        public BarcodeCapture BarcodeCapture
        {
            get => (BarcodeCapture)this.GetValue(BarcodeCaptureProperty);
            set => this.SetValue(BarcodeCaptureProperty, value);
        }

        public IViewfinder Viewfinder
        {
            get => (IViewfinder)this.GetValue(ViewfinderProperty);
            set => this.SetValue(ViewfinderProperty, value);
        }
    }
}
