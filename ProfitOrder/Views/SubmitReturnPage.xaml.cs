namespace ProfitOrder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubmitReturnPage : ContentPage
    {
        public SubmitReturnPage()
        {
            InitializeComponent();
            BindingContext = this;

            App.g_CurrentPage = "SubmitReturnPage";
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            List<Item> lstCartItems = await App.g_db.GetReturnCartItems();
            String sReturnInfo = "";

            foreach (Item item in lstCartItems)
            {
                try
                {
                    sReturnInfo += item.ItemNo.ToString() + "|";
                    sReturnInfo += item.QtyCredit.ToString() + "|";
                    sReturnInfo += "0" + "~";
                }
                catch { }
            }

            App.CommManager.SubmitReturn(App.g_Customer.CustNo, sReturnInfo, App.g_UserName, "");
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}