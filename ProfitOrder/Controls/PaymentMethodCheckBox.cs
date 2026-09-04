namespace ProfitOrder.Controls
{
    public class PaymentMethodCheckBox : CheckBox
    {
        bool IsUpdated = false;

        public static readonly BindableProperty PaymentMethodIdProperty = BindableProperty.Create("PaymentMethodId", typeof(int), typeof(NumericEntryBehavior), 0);
        public static readonly BindableProperty IsDefaultCheckedProperty = BindableProperty.Create(propertyName: "IsDefaultChecked", returnType: typeof(bool), declaringType: typeof(CheckBox), defaultValue: false, defaultBindingMode: BindingMode.TwoWay);

        public int PaymentMethodId
        {
            get => (int)GetValue(PaymentMethodIdProperty);
            set => SetValue(PaymentMethodIdProperty, value);
        }

        public bool IsDefaultChecked
        {
            get { return (bool)GetValue(IsDefaultCheckedProperty); }
            set { SetValue(IsDefaultCheckedProperty, value); }
        }

        public PaymentMethodCheckBox()
        {
            CheckedChanged += PaymentMethodCheckBox_CheckedChanged;
        }

        private async void PaymentMethodCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (IsUpdated)
            {
                IsUpdated = false;
                return;
            }

            IsUpdated = true;



            await App.g_db.ClearDefaultPaymentMethod();

            if (IsChecked)
            {
                await App.g_db.SetDefaultPaymentMethod(PaymentMethodId);
            }
        }
    }
}
