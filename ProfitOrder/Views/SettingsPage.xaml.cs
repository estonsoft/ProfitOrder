namespace ProfitOrder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;

            App.g_CurrentPage = "Settings";
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
            }
            catch { }

            if ((App.g_ServerURL == "") || (App.g_ServerURL == "https://www.turningpointsystems.com"))
            {
                ServerURLCurrent.Text = "Missing Server URL";
            }
            else
            {
                ServerURLCurrent.Text = App.g_ServerURL;
            }

            try
            {
                if (App.g_IsScannerDisabled == "1")
                {
                    DisableScanner.IsChecked = true;
                }
                else
                {
                    DisableScanner.IsChecked = false;
                }
            }
            catch
            {
                DisableScanner.IsChecked = false;
            }

            ServerURLList.ItemsSource = App.g_db.GetServers();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            if (DisableScanner.IsChecked)
            {
                App.g_IsScannerDisabled = "1";
                App.g_db.SaveSetting("ScannerDisabled", "1");
            }
            else
            {
                App.g_IsScannerDisabled = "0";
                App.g_db.SaveSetting("ScannerDisabled", "0");
            }

            //Navigation.PopModalAsync();
            App.g_Shell.GoToLogin();
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            string sURL = ServerURL.Text.Trim().ToLower();

            if ((sURL == "http://muswicksales.ddns.net:8040") && (App.g_SettingsUser.ToUpper() != "MANDANI"))
            {
                App.Current.MainPage.DisplayAlert("Profit Order", "Muswick Wholesale Grocers customers must download and use the Muswick app", "Ok");
                return;
            }

            if (sURL != "")
            {
                if (!Uri.IsWellFormedUriString(sURL, UriKind.Absolute))
                {
                    App.Current.MainPage.DisplayAlert("Profit Order", "Invalid Server URL", "Ok");
                    return;
                }

                Server server = new Server();
                server.ServerURL = sURL;
                App.g_db.SaveServer(server);

                ServerURLList.ItemsSource = null;
                ServerURLList.ItemsSource = App.g_db.GetServers();

                ServerURL.Text = "";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            App.g_Shell.GoToLogin();
            return true;
        }

        private void ServerURL_Clicked(object sender, EventArgs e)
        {
            SetServerURL.IsVisible = false;
            ServerURL.IsVisible = true;
            AddStack.IsVisible = true;
            BoxTop.IsVisible = true;
            BoxBottom.IsVisible = true;
            ServerURLList.IsVisible = true;
        }

        private async void ServerURLList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.g_ServerURL != "")
            {
                bool bResult = await Shell.Current.DisplayAlertAsync("Profit Order", "Are you sure you wish to change the Server URL?  All data will be deleted and refreshed.", "Yes", "No");

                if (!bResult)
                {
                    return;
                }
            }
            var SetURL = e.CurrentSelection?.FirstOrDefault() as Server;
            if (SetURL == null)
                return;

            ServerURLCurrent.Text = SetURL.ServerURL;
            SetServerURL.IsVisible = true;
            ServerURL.IsVisible = false;
            AddStack.IsVisible = false;
            BoxTop.IsVisible = false;
            BoxBottom.IsVisible = false;
            ServerURLList.IsVisible = false;

            if (App.g_ServerURL != SetURL.ServerURL)
            {
                try
                {
                    App.g_db.SuspendCartItems(App.g_Customer.CustNo);
                }
                catch { }

                App.g_db.DeleteAll();

                App.g_ServerURL = SetURL.ServerURL;
                App.UpdateServerLinks();
                App.g_db.SaveSetting("ServerURL", App.g_ServerURL);

                await App.RefreshAll();
            }

            List<Setting> lSettings = App.g_db.GetSettings();
        }
    }
}