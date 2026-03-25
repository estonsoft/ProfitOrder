namespace ProfitOrder.Views
{
    public partial class PaymentMethodEditPage : ContentPage
    {
        public Boolean _IsCreditCardHighlighted;
        public Boolean _IsBankAccountHighlighted;
        private PaymentMethod? pm;

        public Boolean IsCreditCardHighlighted
        {
            get { return _IsCreditCardHighlighted; }
            set
            {
                _IsCreditCardHighlighted = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsBankAccountHighlighted
        {
            get { return _IsBankAccountHighlighted; }
            set
            {
                _IsBankAccountHighlighted = value;
                OnPropertyChanged();
            }
        }

        public PaymentMethodEditPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("InitializeComponent Error " + Environment.NewLine + ex.ToString() + Environment.NewLine + ex.StackTrace);
            }

            BindingContext = this;

            App.g_CurrentPage = "PaymentMethodEditPage";

            App.g_PaymentMethodEditPage = this;

            if (App.g_PaymentMethodEdit.PaymentMethodId > -1)
            {
                CreditCardSelect.IsVisible = false;
                BankAccountSelect.IsVisible = false;

                if (App.g_PaymentMethodEdit.Type == "C")
                {
                    CardholderName.Text = App.g_PaymentMethodEdit.AccountHolderName;
                    CardNumber.Text = App.g_PaymentMethodEdit.CreditCardNo;
                    ExpMonth.Text = App.g_PaymentMethodEdit.ExpMonth;
                    ExpYear.Text = App.g_PaymentMethodEdit.ExpYear;
                    CVV.Text = App.g_PaymentMethodEdit.CVV;
                    Zip.Text = App.g_PaymentMethodEdit.BillingZip;
                    if (App.g_PaymentMethodEdit.IsDefault == 1)
                    {
                        CreditCardIsDefault.IsChecked = true;
                    }
                    else
                    {
                        CreditCardIsDefault.IsChecked = false;
                    }

                    NewCreditCardLayout.IsVisible = true;
                    NewBankAccountLayout.IsVisible = false;
                }
                else if (App.g_PaymentMethodEdit.Type == "B")
                {
                    BankAccountName.Text = App.g_PaymentMethodEdit.AccountHolderName;
                    ABANo.Text = App.g_PaymentMethodEdit.CheckingABA;
                    AccountNo.Text = App.g_PaymentMethodEdit.CheckingAccountNo;
                    AccountNoVerify.Text = App.g_PaymentMethodEdit.CheckingAccountNo;
                    if (App.g_PaymentMethodEdit.IsDefault == 1)
                    {
                        BankAccountIsDefault.IsChecked = true;
                    }
                    else
                    {
                        BankAccountIsDefault.IsChecked = false;
                    }

                    NewCreditCardLayout.IsVisible = false;
                    NewBankAccountLayout.IsVisible = true;
                }
                else
                {
                    App.g_Shell.GoToPaymentMethod();
                }
            }
            else
            {
                IsCreditCardHighlighted = true;
                IsBankAccountHighlighted = false;
                NewCreditCardLayout.IsVisible = true;
                NewBankAccountLayout.IsVisible = false;
                DeleteCreditCardButton.IsVisible = false;
                DeleteBankAccountButton.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SaveCardButton.IsVisible = true;
            WaitImage.IsVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void OnNewCreditCard(object sender, EventArgs e)
        {
            IsBankAccountHighlighted = false;
            IsCreditCardHighlighted = true;
            NewCreditCardLayout.IsVisible = true;
            NewBankAccountLayout.IsVisible = false;
        }

        async void OnNewBankAccount(object sender, EventArgs e)
        {
            IsBankAccountHighlighted = true;
            IsCreditCardHighlighted = false;
            NewCreditCardLayout.IsVisible = false;
            NewBankAccountLayout.IsVisible = true;
        }

        async void OnSaveCreditCardClicked(object sender, EventArgs e)
        {
            if (! VerifyCreditCardInfo())
            {
                return;
            }

            pm = new PaymentMethod();
            pm.PaymentMethodId = App.g_PaymentMethodEdit.PaymentMethodId;
            pm.CustId = App.g_Customer.CustId;
            pm.AccountHolderName = CardholderName.Text.Trim();
            pm.Type = "C";
            pm.BillingZip = Zip.Text.Trim();
            pm.ExpMonth = ExpMonth.Text.Trim();
            pm.ExpYear = ExpYear.Text.Trim();
            pm.CreditCardNo = CardNumber.Text.Trim();
            pm.CVV = CVV.Text.Trim();
            pm.IsDefault = CreditCardIsDefault.IsChecked ? 1 : 0;
            pm.Last4 = pm.CreditCardNo.Substring(pm.CreditCardNo.Length - 4, 4);
            pm.DisplayText = "Card ending " + pm.Last4;

            string CCInfo = pm.BuildGetTokenInfo();

            App.g_PaymentMethodEdit = pm;

            await App.CommManager.ValidateToken(App.g_Customer.CustNo, CCInfo);

            SaveCardButton.IsVisible = false;
            WaitImage.IsVisible = true;
        }

        public async void OnCardVerifyResult(string sResult)
        {
            string[] aResult = sResult.Split("~");
            
            if (aResult[0] == "E")
            {
                await Shell.Current.DisplayAlertAsync("Profit Order", "Error verifying card information\n\n" + aResult[1], "Ok");
                SaveCardButton.IsVisible = true;
                WaitImage.IsVisible = false;
                return;
            }

            pm.CreditCardNo = "************" + pm.Last4;
            pm.Token = aResult[1];

            App.g_db.SavePaymentMethod(pm);

            await App.g_Shell.GoToPaymentMethod();
        }

        async void OnSaveBankAccountClicked(object sender, EventArgs e)
        {
            if (!VerifyBankAccountInfo())
            {
                return;
            }

            PaymentMethod pm = new PaymentMethod();
            pm.PaymentMethodId = App.g_PaymentMethodEdit.PaymentMethodId;
            pm.CustId = App.g_Customer.CustId;
            pm.AccountHolderName = BankAccountName.Text.Trim();
            pm.Type = "B";
            pm.CheckingABA = ABANo.Text.Trim();
            pm.CheckingAccountNo = AccountNo.Text.Trim();
            pm.IsDefault = BankAccountIsDefault.IsChecked ? 1 : 0;
            pm.Last4 = pm.CheckingAccountNo.Substring(pm.CheckingAccountNo.Length - 4, 4);
            pm.DisplayText = "Bank Account ending " + pm.Last4;

            App.g_db.SavePaymentMethod(pm);

            await App.g_Shell.GoToPaymentMethod();
        }

        private bool VerifyCreditCardInfo()
        {
            if ((CardholderName.Text.Trim() == "") || (CardNumber.Text.Trim() == "") || (ExpMonth.Text.Trim() == "") ||
                (ExpYear.Text.Trim() == "") || (CVV.Text.Trim() == "") || (Zip.Text.Trim() == ""))
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "All fields must be entered", "Ok");
                return false;
            }

            if (CardNumber.Text.Trim().Length < 4)
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Invalid Card #", "Ok");
                return false;
            }

            if ((ExpMonth.Text.Trim().Length != 2) || (ExpYear.Text.Trim().Length != 2))
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Invalid Exp Date", "Ok");
                return false;
            }

            if (! int.TryParse(CVV.Text, out _))
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Invalid CVV", "Ok");
                return false;
            }

            if (Zip.Text.Trim().Length != 5)
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Invalid Bill Zip Code", "Ok");
                return false;
            }

            return true;
        }

        private bool VerifyBankAccountInfo()
        {
            if ((BankAccountName.Text.Trim() == "") || (ABANo.Text.Trim() == "") || (AccountNo.Text.Trim() == "") || (AccountNoVerify.Text.Trim() == ""))
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "All fields must be entered", "Ok");
                return false;
            }

            if (AccountNo.Text.Trim().Length < 4)
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Invalid Bank Account #", "Ok");
                return false;
            }

            if (ABANo.Text.Trim().Length != 9)
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Invalid Bank ABA #", "Ok");
                return false;
            }

            if (AccountNo.Text.Trim() != AccountNoVerify.Text.Trim())
            {
                Shell.Current.DisplayAlertAsync("Profit Order", "Account # does not match Verify Account #", "Ok");
                return false;
            }

            return true;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool bDelete = await DisplayAlertAsync("Pitco Foods", "Are you sure you wish to delete this payment method?", "Yes", "No");

            if (bDelete)
            {
                App.g_db.DeletePaymentMethod(App.g_PaymentMethodEdit.PaymentMethodId);

                if (App.g_PaymentMethodEdit.PaymentMethodId == App.g_PaymentMethod.PaymentMethodId)
                {
                    App.g_App.GetDefaultPaymentMethod();
                }

                await App.g_Shell.GoToPaymentMethod();
            }
        }
    }
}