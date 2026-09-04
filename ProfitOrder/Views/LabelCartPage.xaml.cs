namespace ProfitOrder.Views
{
    public partial class LabelCartPage : ContentPage
    {
        public LabelCartPage()
        {
            InitializeComponent();

            //BindingContext = _viewModel = new ShoppingCartViewModel();
            BindingContext = this;

            App.g_LabelCartPage = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();


            List<Item> items = await App.g_db.GetReturnCartItems();

            if (items.Count > 0)
            {
                App.g_CurrentPage = "LabelCartPage";

                RefreshList();
            }
        }

        public async void RefreshList()
        {
            ItemsListCart.ItemsSource = null;

            ItemsListCart.ItemsSource = await App.g_db.GetLabelCartItems();

            foreach (Item i in (List<Item>)ItemsListCart.ItemsSource)
            {
                Item.SetListItem(i, "L");
            }
        }

        private async void btnCheckout_Clicked(object sender, EventArgs e)
        {
            await App.g_Shell.GoToCheckout();
        }

        private async void btnClearCart_Clicked(object sender, EventArgs e)
        {
            bool bClear = await DisplayAlertAsync("Profit Order", "Are you sure you wish to remove all the items from your label print cart?", "Yes", "No");

            if (bClear)
            {
                await App.g_db.ClearLabelCartItems();
                await App.g_Shell.GoToHome();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
