namespace ProfitOrder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickEntryPageNoCamera : ContentPage
    {
        List<Item> lstItems;

        public QuickEntryPageNoCamera()
        {
            this.InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            App.g_CurrentPage = "QuickEntryPage";

            lstItems = new List<Item>();
            ClearItemInfo();

            await Task.Delay(100);
            EntryFocus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void EntryFocus()
        {
            ScanItem.Unfocus();
            ScanItem.Text = "";
            await Task.Delay(500);
            ScanItem.Focus();
        }

        private void ShowItemInfo(Item item)
        {
            Item.SetListItem(item, "O");
            item.IsBoxViewVisible = false;

            lstItems.Clear();
            lstItems.Add(item);
            ItemsListSearch.ItemsSource = null;
            ItemsListSearch.ItemsSource = lstItems;

            ScanItem.Text = "";

            Message.IsVisible = false;
        }

        private void ClearItemInfo()
        {
            ScanItem.Text = "";

            Message.IsVisible = false;

            lstItems.Clear();
            ItemsListSearch.ItemsSource = null;
            ItemsListSearch.ItemsSource = lstItems;

            EntryFocus();
        }

        private void SetMessage(string sMessage)
        {
            ClearItemInfo();
            Message.Text = sMessage;
            Message.IsVisible = true;
        }

        public async Task ScanComplete()
        {
            OnScannerDisable();

            Item item = await FindItem();

            if (item == null)
            {
                ClearItemInfo();
                SetMessage("Item Not Found " + ScanItem.Text);
                ScanItem.Text = "";
                EntryFocus();
                return;
            }

            if (await App.g_db.GetItemQty(item.ItemNo) > 0)
            {
                SetMessage("Item Already In Shopping Cart");
            }

            ShowItemInfo(item);
        }

        async void OnScannerEnable(object sender, EventArgs e)
        {
            ClearItemInfo();
            TapToScan.IsVisible = false;
            ScanRow.IsVisible = true;
            ScanItem.Text = "";
            Message.Text = "";
            EntryFocus();
        }

        async void OnScannerDisable()
        {
            TapToScan.IsVisible = true;
            ScanRow.IsVisible = false;
        }

        private async void ScanItem_Completed(object sender, EventArgs e)
        {
            await ScanComplete();
        }

        public void SetScanItem(string barcode)
        {
            ScanItem.Text = barcode;
        }

        private async void EnterButton_Clicked(object sender, EventArgs e)
        {
            Message.Text = "";
            await ScanComplete();
        }

        private async Task<Item> FindItem()
        {
            Item item = null;
            List<Item> items = new List<Item>();
            int ItemNo = 0;

            string ScanText = ScanItem.Text.Trim();
            int.TryParse(ScanItem.Text, out ItemNo);

            if (ItemNo > 0)
            {
                item = await App.g_db.FindItem(ItemNo, ItemNo.ToString());
            }

            if (item == null)
            {
                items = await App.g_db.SearchItemsQuickEntry(ScanText);

                if (items.Count >= 1)
                {
                    item = items[0];
                }
            }

            return item;
        }
    }
}
