namespace ProfitOrder.Views
{
    public partial class ReorderItemsPage : ContentPage
    {
        public ReorderItemsPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            App.g_CurrentPage = "ReorderItemsPage";

            RefreshList();
        }

        public async void RefreshList()
        {
            ReorderItemsList.ItemsSource = App.g_ReorderItemList;

            List<Item> lstItem = await App.g_db.GetItems();

            foreach (Item ri in (List<Item>)ReorderItemsList.ItemsSource)
            {
                ri.IsLoggedIn = App.g_IsLoggedIn;

                foreach (Item i in lstItem)
                {
                    if (ri.ItemNo == i.ItemNo)
                    {
                        ri.QtyOrder = i.QtyOrder;
                        ri.IsPriceVisible = i.IsPriceVisible;
                        break;
                    }
                }

                Item.SetListItem(ri, "O");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}

