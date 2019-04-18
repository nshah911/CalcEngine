using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class PurchaseCredit
    {
        public decimal _Amount;
        
        public PurchaseCreditSourceType _SourceType;

        public PurchaseCreditType _Type;
    }

    public enum PurchaseCreditSourceType
    {
        /// <summary>
        /// Default to unknown
        /// </summary>
        Unknown,

        /// <remarks/>
        BorrowerPaidOutsideClosing,

        /// <remarks/>
        PropertySeller,

        /// <remarks/>
        Lender,

        /// <remarks/>
        NonParentRelative,

        /// <remarks/>
        BuilderDeveloper,

        /// <remarks/>
        Employer,

        /// <remarks/>
        FederalAgency,

        /// <remarks/>
        LocalAgency,

        /// <remarks/>
        Other,

        /// <remarks/>
        Parent,

        /// <remarks/>
        RealEstateAgent,

        /// <remarks/>
        StateAgency,

        /// <remarks/>
        UnrelatedFriend,
    }

    public enum PurchaseCreditType
    {
        /// <summary>
        /// Default to unknown
        /// </summary>
        Unknown,

        /// <remarks/>
        EarnestMoney,

        /// <remarks/>
        RelocationFunds,

        /// <remarks/>
        EmployerAssistedHousing,

        /// <remarks/>
        LeasePurchaseFund,

        /// <remarks/>
        Other,

        /// <remarks/>
        BuydownFund,

        /// <remarks/>
        CommitmentOriginationFee,

        /// <remarks/>
        GiftOfEquity,

        /// <remarks/>
        PrivateMIMIPremiumVAFundingFeeRefund,

        /// <remarks/>
        SweatEquity,

        /// <remarks/>
        TradeEquity,
    }

}
