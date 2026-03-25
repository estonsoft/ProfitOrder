namespace ProfitOrder.Data
{
	public class CommManager
	{
		ISoapService soapService;

        public CommManager (ISoapService service)
		{
			soapService = service;
		}

		public Task<string> GetBanners()
		{
			return soapService.GetBannersAsync();
		}

		public Task<string> GetCategoriesAndSubcategories()
		{
			return soapService.GetCategoriesAndSubcategoriesAsync();
		}

		public Task<string> GetCategoriesAndSubcategoriesCust(string sCust)
		{
			return soapService.GetCategoriesAndSubcategoriesCustAsync(sCust);
		}

		public Task<string> GetItems(String sCustomer, String sDate)
		{
			return soapService.GetItemsAsync(sCustomer, sDate);
		}

		public Task<string> GetItemQOH(String sCustomer)
		{
			return soapService.GetItemQOHAsync(sCustomer);
		}

		public Task<string> GetItemQOH2(String sUser, String sCustomer)
		{
			return soapService.GetItemQOH2Async(sUser, sCustomer);
		}

        public Task<string> ValidateLogin(String sUser, String sPassword, String sDeviceId)
        {
            return soapService.ValidateLoginAsync(sUser, sPassword, sDeviceId);
        }

        public Task<string> ValidateToken(String sCustNo, String sCCInfo)
        {
            return soapService.ValidateTokenAsync(sCustNo, sCCInfo);
        }

        public Task<string> ValidateUserActive(String sUser)
        {
            return soapService.ValidateUserActiveAsync(sUser);
        }

        public Task<string> GetSettings()
        {
            return soapService.GetSettingsAsync();
        }

        public Task<string> SubmitOrder(string sCustNo, string sPO, string sPaymentMethod, string sCCInfo, string sOrderInfo, string sDeliveryPickup, string sUser, string sNotes, int iHoldForReview, string sOrderType)
        {
            return soapService.SubmitOrderAsync(sCustNo, sPO, sPaymentMethod, sCCInfo, sOrderInfo, sDeliveryPickup, sUser, sNotes, iHoldForReview, sOrderType);
        }

        public Task<string> SaveBuildTo(string sCustNo, string sItemNo, string sBuildTo)
        {
            return soapService.SaveBuildToAsync(sCustNo, sItemNo, sBuildTo);
        }

        public Task<string> SubmitReturn(string sCustNo, string sOrderInfo, string sUser, string sNotes)
        {
            return soapService.SubmitReturnAsync(sCustNo, sOrderInfo, sUser, sNotes);
        }

        public Task<string> GetOrderHistory(string sCustNo)
        {
            return soapService.GetOrderHistoryAsync(sCustNo);
        }

        public Task<string> GetSalespersonCustomers(string sUser)
        {
            return soapService.GetSalespersonCustomersAsync(sUser);
        }

        public Task<string> GetFlyerItemsPDF()
        {
            return soapService.GetFlyerItemsPDFAsync();
        }
    }
}
