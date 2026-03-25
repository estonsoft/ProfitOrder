using ProfitOrder.ViewModels;

namespace ProfitOrder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentMethodPage : ContentPage
    {
        List<PaymentMethod> lstItems = new List<PaymentMethod>();

        public PaymentMethodPage()
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

            App.g_CurrentPage = "PaymentMethodPage";
            App.g_PaymentMethodPage = this;

            RefreshList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void OnAddNewPaymentMethodClicked(object sender, EventArgs e)
        {
            App.g_PaymentMethodEdit = new PaymentMethod();
            App.g_PaymentMethodEdit.PaymentMethodId = -1;
            App.g_Shell.GoToPaymentMethodEdit();
        }

        public void RefreshList()
        {
            PaymentMethodList.ItemsSource = null;

            lstItems = App.g_db.GetPaymentMethods();

            foreach (PaymentMethod pm in lstItems)
            {
                if (pm.IsDefault == 1)
                {
                    pm.DisplayText = pm.DisplayText + " (default)";
                }

                if (pm.PaymentMethodId == 1)
                {
                    pm.IsEditVisible = false;
                }
                else
                {
                    pm.IsEditVisible = true;
                }
            }

            PaymentMethodList.ItemsSource = lstItems;
        }
    }
}