namespace ProfitOrder.Controls
{
    public class OrderLabel : Label
    {
        public static readonly BindableProperty OrderNoProperty = BindableProperty.Create("OrderNo", typeof(string), typeof(string));

        public string OrderNo
        {
            get => (string)GetValue(OrderNoProperty);
            set => SetValue(OrderNoProperty, value);
        }

        public OrderLabel()
        {
        }
    }
}
