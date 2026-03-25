namespace ProfitOrder.Controls
{
    public class BuildToEntry : Entry
    {
        public static readonly BindableProperty ItemNoProperty = BindableProperty.Create("ItemNo", typeof(int), typeof(NumericEntryBehavior), 0);
        public static readonly BindableProperty BuildToProperty = BindableProperty.Create("BuildTo", typeof(int), typeof(int));

        public int ItemNo
        {
            get => (int)GetValue(ItemNoProperty);
            set => SetValue(ItemNoProperty, value);
        }

        public int BuildTo
        {
            get => (int)GetValue(BuildToProperty);
            set => SetValue(BuildToProperty, value);
        }

        public BuildToEntry()
        {
        }
    }
}
