namespace ProfitOrder.Controls
{
    public class QtyEntry : Entry
    {
        public static readonly BindableProperty ItemNoProperty = BindableProperty.Create("ItemNo", typeof(int), typeof(NumericEntryBehavior), 0);
        public static readonly BindableProperty AllocationQtyProperty = BindableProperty.Create("AllocationQty", typeof(int), typeof(int));

        public int ItemNo
        {
            get => (int)GetValue(ItemNoProperty);
            set => SetValue(ItemNoProperty, value);
        }

        public int AllocationQty
        {
            get => (int)GetValue(AllocationQtyProperty);
            set => SetValue(AllocationQtyProperty, value);
        }

        public QtyEntry()
        {
        }
    }
}
