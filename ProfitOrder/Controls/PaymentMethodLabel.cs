namespace ProfitOrder.Controls
{
    class PaymentMethodLabel : Label
    {
        public static readonly BindableProperty PaymentMethodIdProperty = BindableProperty.Create("PaymentMethodId", typeof(int), typeof(NumericEntryBehavior), 0);

        TapGestureRecognizer TapLabel;

        public int PaymentMethodId
        {
            get => (int)GetValue(PaymentMethodIdProperty);
            set => SetValue(PaymentMethodIdProperty, value);
        }

        public PaymentMethodLabel()
        {
            TapLabel = new TapGestureRecognizer();
            TapLabel.Tapped += (sender, e) =>
            {
                OnLabelTapped(sender, e);
            };

            GestureRecognizers.Add(TapLabel);
        }

        void OnLabelTapped(object sender, EventArgs e)
        {
            //Database db = new Database();
            
            if (Text == "Use this payment method")
            {
                App.g_PaymentMethod = App.g_db.FindPaymentMethod(PaymentMethodId);
                App.g_CheckoutPage.SetPaymentMethod();
                App.g_Shell.GoToCheckout();
            }
            else if (Text == "Edit")
            {
                App.g_PaymentMethodEdit = App.g_db.FindPaymentMethod(PaymentMethodId);
                App.g_Shell.GoToPaymentMethodEdit();
            }
        }
    }
}
