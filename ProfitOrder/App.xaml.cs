using ProfitOrder.Data;
using ProfitOrder.ViewModels;
using ProfitOrder.Views;

namespace ProfitOrder
{
    public partial class App : Application
    {
        public static App g_App;
        public static AppShell g_Shell;

        public static Database g_db;

        public static ItemSearchPage g_SearchPage;
        public static HomePage g_HomePage;
        public static LoginPage g_LoginPage;
        public static ShoppingCartPage g_ShoppingCartPage;
        public static ReturnCartPage g_ReturnCartPage;
        public static LabelCartPage g_LabelCartPage;
        public static CheckoutPage g_CheckoutPage;
        public static PaymentMethodPage g_PaymentMethodPage;
        public static PaymentMethodEditPage g_PaymentMethodEditPage;
        public static Customer g_Customer;
        public static Category g_Category;
        public static Subcategory g_Subcategory;
        public static Subsubcategory g_Subsubcategory;
        public static PaymentMethod g_PaymentMethod;
        public static PaymentMethod g_PaymentMethodEdit;

        //public static List<Category> g_CategoryList;
        public static List<Category> g_HomePageCategoryList;
        public static List<Item> g_ItemList;
        public static List<Item> g_ReorderItemList;

        public static CommManager CommManager { get; set; }
        public static String g_SearchText { get; set; }
        public static String g_SectionName { get; set; }
        public static String g_ScanBarcode { get; set; }
        public static String g_UserName { get; set; }
        public static String g_ServerURL { get; set; }
        public static String g_Company { get; set; }
        public static String g_CurrentPage { get; set; }
        public static String g_SearchFromPage { get; set; }
        public static Boolean g_IsLoggedIn { get; set; }
        public static Boolean g_IsTopSellers { get; set; }
        public static Boolean g_InStockOnly { get; set; }
        public static Boolean g_IsCredits { get; set; }
        public static Boolean g_HoldForReview { get; set; }
        public static Boolean g_ForceSubmit { get; set; }
        public static Boolean g_BlockItemsNoQOH { get; set; }
        public static Boolean g_IsScandit { get; set; }
        public static Boolean g_IsSalesUser { get; set; }
        public static Boolean g_IsChainManager { get; set; }
        public static Boolean g_IsAutoAdd1 { get; set; }
        public static String g_QOHDisplay { get; set; }
        public static String g_PaymentProvider { get; set; }
        public static String g_OrderNo { get; set; }
        public static String g_HeaderTitle { get; set; }
        public static int g_ShoppingCartItems { get; set; }
        public static String g_SettingsUser { get; set; }
        public static bool g_IsScannerInit { get; set; }
        public static ScanditViewModelBase g_ScanditViewModel { get; set; }
        public static String g_IsScannerDisabled { get; set; }
        public static String g_FlyerFilename { get; set; }
        public static Boolean g_IsMonthlyAdPDFClick { get; set; }
        public static int g_MonthlyAdPage { get; set; }
        public static int g_MonthlyAdX { get; set; }
        public static int g_MonthlyAdY { get; set; }
        public static Boolean g_IsMonthlyFlyer { get; set; }
        public static Boolean g_IsRefNoLookup { get; set; }
        public static int g_FlyerStartDate { get; set; }
        public static int g_FlyerEndDate { get; set; }
        public static string g_Notes { get; set; }
        public static string g_ShoppingCartSort { get; set; }
        public static Boolean g_IsShowSubcategories { get; set; }
        public static Boolean g_IsBuildToEnabled { get; set; }
        public static Boolean g_IsBuildToViewOnly { get; set; }

        public class MessageKeys
        {
            public const string OnStart = nameof(OnStart);
            public const string OnSleep = nameof(OnSleep);
            public const string OnResume = nameof(OnResume);
        }

        public App(CommManager _commManager)
        {
            InitializeComponent();
            CommManager = _commManager;

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ0OTcyOEAzMTM5MmUzNDJlMzBoTVFSazNhbDdpOTVGMVE3VXExSzNPZENwUFJ5WmhnT2ZxaDQrK2dBQ0hJPQ==");

            try
            {
                if (g_db == null)
                {
                    g_db = new Database();
                }
            }
            catch
            {
                g_db = new Database();
            }

            g_App = this;
            Task.Run(async () =>
            {
                LoadSettings();
            });
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        private async void LoadSettings()
        {
            g_FlyerFilename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "MonthlyFlyer.pdf");

            try
            {
                if (!g_IsLoggedIn)
                {
                    g_IsLoggedIn = false;
                }
            }
            catch
            {
                g_IsLoggedIn = false;
            }

            if (!g_IsLoggedIn)
            {
                //Database db = new Database();

                g_UserName = "";

                if (g_db.GetSetting("LoggedIn") == "1")
                {
                    g_IsLoggedIn = true;
                    g_UserName = g_db.GetSetting("UserName");
                }

                g_ServerURL = g_db.GetSetting("ServerURL");
                if (g_ServerURL != "")
                {
                    Server server = new Server();
                    server.ServerURL = g_ServerURL;
                    g_db.SaveServer(server);
                }
                else
                {
                    g_ServerURL = "https://www.turningpointsystems.com";
                }
                UpdateServerLinks();

                if (g_db.GetSetting("Credits") == "1")
                {
                    g_IsCredits = true;
                }
                else
                {
                    g_IsCredits = false;
                }
                g_QOHDisplay = g_db.GetSetting("QOHDisplay");

                g_IsScannerDisabled = g_db.GetSetting("ScannerDisabled");

                g_PaymentProvider = g_db.GetSetting("PaymentProvider");

                if (g_db.GetSetting("MonthlyFlyer") == "1")
                {
                    g_IsMonthlyFlyer = true;
                }
                else
                {
                    g_IsMonthlyFlyer = false;
                }
                string sFlyerStartDate = g_db.GetSetting("FlyerStartDate");
                if (sFlyerStartDate == "")
                {
                    g_FlyerStartDate = 0;
                }
                else
                {
                    int FlyerStartDate = 0;
                    int.TryParse(sFlyerStartDate, out FlyerStartDate);
                    g_FlyerStartDate = FlyerStartDate;
                }
                string sFlyerEndDate = g_db.GetSetting("FlyerEndDate");
                if (sFlyerEndDate == "")
                {
                    g_FlyerEndDate = 0;
                }
                else
                {
                    int FlyerEndDate = 0;
                    int.TryParse(sFlyerEndDate, out FlyerEndDate);
                    g_FlyerEndDate = FlyerEndDate;
                }
                if (g_db.GetSetting("AutoAdd1") == "1")
                {
                    g_IsAutoAdd1 = true;
                }
                else
                {
                    g_IsAutoAdd1 = false;
                }
                if (g_db.GetSetting("RefNoLookup") == "1")
                {
                    g_IsRefNoLookup = true;
                }
                else
                {
                    g_IsRefNoLookup = false;
                }
                if (g_db.GetSetting("ShowSubcateogries") == "0")
                {
                    g_IsShowSubcategories = false;
                }
                else
                {
                    g_IsShowSubcategories = true;
                }

                g_Company = "";
                g_SearchText = "";
                g_SearchFromPage = "";
                g_ScanBarcode = "";
                g_SectionName = "";
                g_CurrentPage = "";
                g_IsTopSellers = false;
                g_OrderNo = "";
                g_HeaderTitle = "";

                if (g_db.GetSetting("IsSalesUser") == "1")
                {
                    g_IsSalesUser = true;
                }
                else
                {
                    g_IsSalesUser = false;
                }
                if (g_db.GetSetting("IsChainManager") == "1")
                {
                    g_IsChainManager = true;
                }
                else
                {
                    g_IsChainManager = false;
                }
                if (g_db.GetSetting("HoldForReview") == "1")
                {
                    g_HoldForReview = true;
                }
                else
                {
                    g_HoldForReview = false;
                }
                if (g_db.GetSetting("BlockItemsNoQOH") == "1")
                {
                    g_BlockItemsNoQOH = true;
                }
                else
                {
                    g_BlockItemsNoQOH = false;
                }
                g_IsScandit = true;
                g_ShoppingCartSort = g_db.GetSetting("ShoppingCartSort");
                if (g_db.GetSetting("IsBuildToEnabled") == "1")
                {
                    g_IsBuildToEnabled = true;
                }
                else
                {
                    g_IsBuildToEnabled = false;
                }
                if (g_db.GetSetting("IsBuildToViewOnly") == "1")
                {
                    g_IsBuildToViewOnly = true;
                }
                else
                {
                    g_IsBuildToViewOnly = false;
                }

                g_IsScannerInit = false;
                g_ScanditViewModel = null;

                g_Category = new Category();
                g_Category.Code = "";
                g_Category.Description = "ALL CATEGORIES";

                g_Subcategory = new Subcategory();
                g_Subcategory.Code = "";
                g_Subcategory.Description = "ALL SUBCATEGORIES";

                g_Subsubcategory = new Subsubcategory();
                g_Subsubcategory.Code = "";
                g_Subsubcategory.Description = "ALL SUB-SUBCATEGORIES";

                Constants.Load();

                Location location = new Location();
                location.Refresh();

                g_Customer = new Customer();
                g_ShoppingCartItems = App.g_db.GetCartPieces();

                
                    try
                    {
                        g_Customer = new Customer();
                        g_Customer = App.g_db.GetCustomer();
                        if (g_Customer == null)
                        {
                            g_Customer = new Customer();
                        }
                    }
                    catch
                    {
                        g_Customer = new Customer();
                    }
                

                //g_CategoryList = App.g_db.GetCategories();
                g_HomePageCategoryList = App.g_db.GetHomePageCategories();
                g_ItemList = App.g_db.GetItems();
                g_ReorderItemList = App.g_db.GetReorderItems();

                try
                {
                    await App.CommManager.GetSettings();
                }
                catch { }

                InsertOnAccountPaymentMethod();
                GetDefaultPaymentMethod();

                await RefreshAll();
                InitializeAllTimer();

                await RefreshOrderHistory();
                InitializeOrderHistoryTimer();

                if (g_IsSalesUser || g_IsChainManager)
                {
                    await App.CommManager.GetSalespersonCustomers(g_UserName);
                }

                await RefreshQOH();
                InitializeQOHTimer();
            }
        }
        public static void UpdateServerLinks()
        {
            Constants.BaseURL = App.g_ServerURL;
            Constants.SoapUrl = App.g_ServerURL + "/RemotePhoneApp.asmx";
            Constants.LogoUrl = App.g_ServerURL + "/images/logo/logo.jpg";
            Constants.BannerUrl = App.g_ServerURL + "/images/banner phone/";
            Constants.CategoryImageUrl = App.g_ServerURL + "/images/category/";
            Constants.ItemImageUrl = App.g_ServerURL + "/images/items/";
        }

        private void InitializeAllTimer()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(Constants.TimerHour * 24), () =>
            {
                Task.Run(async () =>
                {
                    _ = await RefreshAll();
                    return true;
                });

                return true;
            });
        }

        public static async Task<String> RefreshAll()
        {
            if (App.g_ServerURL != "")
            {
                // start with banners  services will call next when one is done
                await App.CommManager.GetBanners();
            }

            return "";
        }

        private void InitializeQOHTimer()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(Constants.TimerHour), () =>
            {
                Task.Run(async () =>
                {
                    _ = await RefreshQOH();
                    return true;
                });

                return true;
            });
        }

        public static async Task<String> RefreshQOH()
        {
            try
            {
                if ((App.g_Customer.CustNo != null) && (App.g_Customer.CustNo != "") && (App.g_Customer.CustNo != "0"))
                {
                    await App.CommManager.GetItemQOH2(App.g_UserName, App.g_Customer.CustNo);
                }
            }
            catch { }

            return "";
        }

        private void InitializeBannerTimer()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(Constants.TimerHour * 24), () =>
            {
                Task.Run(async () =>
                {
                    _ = await RefreshBanners();
                    return true;
                });

                return true;
            });
        }

        private async Task<String> RefreshBanners()
        {
            return "";
        }

        private void InitializeItemTimer()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(Constants.TimerHour), () =>
            {
                Task.Run(async () =>
                {
                    _ = await RefreshItems();
                    return true;
                });

                return true;
            });
        }

        private async Task<String> RefreshItems()
        {
            //Database db = new Database();

            //g_Customer = await db.GetCustomerAsync();

            return "";
        }

        private void InitializeOrderHistoryTimer()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(Constants.TimerHour), () =>
            {
                Task.Run(async () =>
                {
                    _ = await RefreshOrderHistory();
                    return true;
                });

                return true;
            });
        }

        public static async Task<String> RefreshOrderHistory()
        {
            try
            {
                if ((App.g_Customer.CustNo != null) && (App.g_Customer.CustNo != "") && (App.g_Customer.CustNo != "0"))
                {
                    await App.CommManager.GetOrderHistory(App.g_Customer.CustNo);
                }
            }
            catch { }

            return "";
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            int i = 9;
        }

        protected override void OnResume()
        {
            if (App.g_IsLoggedIn)
            {
                ValidateUserActive();
            }

            try
            {
                App.CommManager.GetSettings();
            }
            catch { }
        }

        public static async Task<String> ValidateUserActive()
        {
            try
            {
                await App.CommManager.ValidateUserActive(App.g_UserName);
            }
            catch { }

            return "";
        }

        public void InsertOnAccountPaymentMethod()
        {
            //Database db = new Database();

            PaymentMethod pm = App.g_db.FindPaymentMethod(1);

            if (pm is null)
            {
                g_PaymentMethod = new PaymentMethod();
                g_PaymentMethod.DisplayText = "On Account";
                g_PaymentMethod.PaymentMethodId = 1;
                g_PaymentMethod.IsDefault = 0;
                g_PaymentMethod.IsDefaultChecked = false;
                g_PaymentMethod.Type = "A";

                App.g_db.SavePaymentMethod(g_PaymentMethod);
            }
        }

        public void GetDefaultPaymentMethod()
        {
            //Database db = new Database();

            List<PaymentMethod> paymentMethods = App.g_db.GetDefaultPaymentMethod();
            if (paymentMethods.Count > 0)
            {
                g_PaymentMethod = paymentMethods[0];
            }
            else
            {
                // set to On Account
                g_PaymentMethod = App.g_db.FindPaymentMethod(1);
            }
        }
    }
}
