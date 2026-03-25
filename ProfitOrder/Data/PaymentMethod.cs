using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfitOrder
{
    public class PaymentMethod
    {
        [PrimaryKey]
        public int PaymentMethodId { get; set; }
        public int CustId { get; set; }
        public string Type { get; set; }  // C - Card, B - Bank Account
        public string BillingZip { get; set; }
        public string AccountHolderName { get; set; }
        public string Token { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CVV { get; set; }
        public string Last4 { get; set; }
        public string CreditCardNo { get; set; }
        public string CheckingABA { get; set; }
        public string CheckingAccountNo { get; set; }
        public int IsDefault { get; set; }
        public bool IsDefaultChecked { get; set; }
        public bool IsEditVisible { get; set; }
        public string DisplayText { get; set; }

        public string BuildCCInfo()
        {
            string sCCInfo = Type + Constants.FieldDelimiter;
            sCCInfo += App.g_Customer.CustNo + Constants.FieldDelimiter;
            if (Token == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += Token.ToString() + Constants.FieldDelimiter;
            }
            if (AccountHolderName == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += AccountHolderName.ToString() + Constants.FieldDelimiter;
            }
            if (BillingZip == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += BillingZip.ToString() + Constants.FieldDelimiter;
            }

            sCCInfo += Constants.FieldDelimiter; // Credit Card #
            sCCInfo += Constants.FieldDelimiter; // Last 4
            sCCInfo += Constants.FieldDelimiter; // Exp Month
            sCCInfo += Constants.FieldDelimiter; // Exp Year
            sCCInfo += Constants.FieldDelimiter; // CVV
            sCCInfo += Constants.FieldDelimiter; // Checking ABA
            sCCInfo += Constants.FieldDelimiter; // Checking Account #

            return sCCInfo;
        }

        public string BuildGetTokenInfo()
        {
            string sCCInfo = "";
            if (AccountHolderName == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += AccountHolderName.ToString() + Constants.FieldDelimiter;
            }
            if (BillingZip == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += BillingZip.ToString() + Constants.FieldDelimiter;
            }
            if (CreditCardNo == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += CreditCardNo.ToString() + Constants.FieldDelimiter;
            }
            if (Last4 == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += Last4.ToString() + Constants.FieldDelimiter;
            }
            if (ExpMonth == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += ExpMonth.ToString() + Constants.FieldDelimiter;
            }
            if (ExpYear == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += ExpYear.ToString() + Constants.FieldDelimiter;
            }
            if (CVV == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += CVV.ToString() + Constants.FieldDelimiter;
            }
            if (CheckingABA == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += CheckingABA.ToString() + Constants.FieldDelimiter;
            }
            if (CheckingAccountNo == null)
            {
                sCCInfo += Constants.FieldDelimiter;
            }
            else
            {
                sCCInfo += CheckingAccountNo.ToString() + Constants.FieldDelimiter;
            }

            return sCCInfo;
        }
    }
}

