namespace ProfitOrder.Controls
{
    public class QtyBreakLabel : Label
    {
        public static readonly BindableProperty ItemNoProperty = BindableProperty.Create("ItemNo", typeof(int), typeof(NumericEntryBehavior), 0);
        public static readonly BindableProperty QtyBreakProperty = BindableProperty.Create("QtyBreak", typeof(int), typeof(int));

        public int ItemNo
        {
            get => (int)GetValue(ItemNoProperty);
            set => SetValue(ItemNoProperty, value);
        }

        public int QtyBreak
        {
            get => (int)GetValue(QtyBreakProperty);
            set => SetValue(QtyBreakProperty, value);
        }

        public QtyBreakLabel()
        {
        }
    }
}
