namespace ProfitOrder.Controls
{
    public class CustomerStackLayout : StackLayout
    {
        public static readonly BindableProperty CustNoProperty = BindableProperty.Create("CustNo", typeof(string), typeof(string));

        public string CustNo
        {
            get => (string)GetValue(CustNoProperty);
            set => SetValue(CustNoProperty, value);
        }

        public CustomerStackLayout()
        {
        }
    }
}
