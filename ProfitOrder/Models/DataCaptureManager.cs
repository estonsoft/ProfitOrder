using Scandit.DataCapture.Barcode.Capture;
using Scandit.DataCapture.Barcode.Data;
using Scandit.DataCapture.Core.Capture;
using Scandit.DataCapture.Core.Source;
using Microsoft.Maui.Devices;

namespace ProfitOrder.Models
{
    public class DataCaptureManager
    {
        private static readonly Lazy<DataCaptureManager> instance =
            new Lazy<DataCaptureManager>(() => new DataCaptureManager(), LazyThreadSafetyMode.PublicationOnly);

        public static DataCaptureManager Instance => instance.Value;

        public bool IsLicenseValid { get; private set; }

        private DataCaptureManager()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.DataCaptureContext = DataCaptureContext.ForLicenseKey("Ac2iC6+BOxnvFl/LoQZVwds3C30cIfuDMkmMfuJSUUiAV9xrvkypF2NFYJiWUO+HmRer1RV0V81MXHQn12drITJLtw5FdqJVQipaoupT1yHIVuyE90nMf1okXD+/Ax+GFwbHadYkBZ0NHe3SVq3g1EZb75ALwov93C0YcvMCLx1oodJaSX+QaBubDGEuxzFMOuUESblQOP6uTTWw3AmyQ7Agk+0l/NLXTvR6T0m20BAmOxglbAih7O8Kf7M1DJXA8e3L7LcmvAAMl0JiSz4wa5SPuV1w7DyA+mVLKJ3sLi3++i9USNP836puSoM2lOQ9ZtziuxDJpXKMuSCEwIkvCZun7cTqu6BK9unuMkRL7BhWuathwv6gvC7/lkflyv6lIvTMO3xi9SsWKsdbLmmM2pu2e2mmjS6LWGCoVGb7DCgnre1kBMOENa1bjTbbHJUpFAA3Y1NIipJzVZz5uOxEY+N0D3to4BAeeim0wCH3vLSOX4Wvz5PqZ6/ajOMCpzycufzD+EYMkmlocg/BewM2+RJrAaKjclmekr85IltEj1BPhuXivDzN1PgpHeAvhQHz8beh6OHaSUOQ9e+jau5KyUBm6urAEN5oAmh0BdXxQhI6BmZKImNwA5nwb0K86c02uoS4HRwsEAGatwXc0rQepb1lc1WksILiO83z9QIAaOdkhP4vSVrlRKqyKg2yPsEkndIlG1iJwq+IqXXrIfs1JAUMrVSdfBsm6OzZVf0eXlYfc+jpclIKB1yJPfS+RVIBl8lpINnzSLXIuGmt8HcyxctMvc8Eoaw1afKrbCOMJIZg/gY6neko40neRhU=");
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                this.DataCaptureContext = DataCaptureContext.ForLicenseKey("Ae2iRdCBRDoaCFo1cg+WGKcO5+QJHk51tFW6Xc51nSnzT+8bZlahi3Rf92V9VdMlY2C4HE1X+uzeVkLJlE/SByFjL/7mfqF8iWtBWpdYWsPlZLG+PV4LCi1JMddXHtflEztD2CA8ECZmO2an3oeAXQvq32EvfWq5uMs6MbqWxU0TXF/TlxaLtfm/ZHWEAWhIhT/tkutbs+R8N4m2Sf1q+Au7iCgnasulox+L/UOIJSCKQsrEEhrUXorVe5jLIrpfkLCKallVIftEvJDhZyyInWlXbLgMDvzCPGZPqabH45f9ekBfLtL04a6zFSmYSQCNoNZy4wwO0t3pTOM8sHHre6bcPdx3bRXCDs+7YWUQiqtUITebr3+vi73hAisEYBUrf5aIWYdF61o6cyxIiwmFvZoOdX/bHxVUwl82TWGxVMwsPcEft9Uvhj5AcgpSWCCeNcPjJqL/aaG1d0W1PnacrKKM7NGZL76/uSwhuq7GL0w5ek6FVngyeTrXaSeIuFiMpGOQcPEWQD/O02p3YkVIKr3t/FAv7Mu8RtkrPjSTnhU6xU8J9qijzRdvp0hzzReqXEC38DPN08SO29cI+htciIkVaXYAriIGNXKipZMjxo7KzBvOHo1lOiNydgRfINs5w9VQe7yKvn4OkqIS22dIPHpK0wb6xc2vy6NbAjGD5RVpULe1duqVrleXtmCYa4IelArErkiQP+U9akB8Jy3qKeV9x8ouaMKK0skrcV5xoozQulHKqE/fCcQpCgHqbRzWYqPjm0hsVPG/mo0Ywou/oQBi70OLh3gyjU53QmqVoWwcuvHVOx1VXl0BdQ==");
            }

            DataCaptureContext.StatusChanged += OnStatusChanged;
        }

        #region Initialization

        public void InitializeCamera()
        {
            DataCaptureContext.SetFrameSourceAsync(CurrentCamera);
            CurrentCamera?.ApplySettingsAsync(CameraSettings);
        }

        public void InitializeBarcodeCapture()
        {
            DataCaptureContext.RemoveAllModes();

            BarcodeCaptureSettings = BarcodeCaptureSettings.Create();

            var symbologies = new HashSet<Symbology>
            {
                Symbology.Ean13Upca,
                Symbology.Ean8,
                Symbology.Upce,
                Symbology.Code39,
                Symbology.Code128,
                Symbology.InterleavedTwoOfFive
            };

            BarcodeCaptureSettings.EnableSymbologies(symbologies);
            BarcodeCapture = BarcodeCapture.Create(DataCaptureContext, BarcodeCaptureSettings);
        }

        #endregion

        #region DataCaptureContext

        public DataCaptureContext DataCaptureContext { get; }

        #endregion

        #region Camera

        public Camera CurrentCamera { get; } = Camera.GetDefaultCamera();
        public CameraSettings CameraSettings { get; } = BarcodeCapture.RecommendedCameraSettings;

        #endregion

        #region BarcodeCapture

        public BarcodeCapture BarcodeCapture { get; private set; }
        public BarcodeCaptureSettings BarcodeCaptureSettings { get; private set; }

        #endregion

        private void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            IsLicenseValid = e.Status.Valid;
        }
    }
}
