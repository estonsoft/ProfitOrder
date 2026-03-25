namespace ProfitOrder.Controls
{
    public class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var current = args.NewTextValue;
            current = current.TrimStart('0');

            if (current.Length == 0)
            {
                current = "0";
            }

            if (string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                ((Entry)sender).Text = 0.ToString();
                return;
            }

            int iValue = 0;
            if (!int.TryParse(args.NewTextValue, out iValue))
            {
                ((Entry)sender).Text = args.OldTextValue;
            }
            else
            {
                ((Entry)sender).Text = iValue.ToString();
            }
        }
    }
}
