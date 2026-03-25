namespace ProfitOrder.Controls
{
    public class MySearchHandler : SearchHandler
    {
        public MySearchHandler()
        {
            this.FontSize = 12;
            this.ItemsSource = null;
            this.ShowsResults = false;
        }

        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            ShowsResults = false;

            base.OnQueryChanged(oldValue, newValue);
            if (newValue != "")
            {
                App.g_SearchText = newValue;
            }
            //await QueryItems(oldValue, newValue);
        }

        protected override void OnQueryConfirmed()
        {
            //base.OnQueryConfirmed();
            GoToSearchPage();
        }


        protected async void GoToSearchPage()
        {
            App.g_ScanBarcode = "";
            App.g_SearchFromPage = App.g_CurrentPage;
            App.g_Shell.GoToItemSearch();
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            await System.Threading.Tasks.Task.Delay(1000);

            App.g_Shell.ShowNavBar();

            //var id = ((TodoItem)item).ID;
            var id = 0;

            // Note: strings will be URL encoded for navigation
            // RG await Shell.Current.GoToAsync($"//todo/todoItem?itemid={id}");
        }

        private async Task QueryItems(string oldValue, string newValue)
        {
            var shell = Application.Current.MainPage as Shell;
            await shell.GoToAsync($"app:///HomePage");
        }
    }
}
