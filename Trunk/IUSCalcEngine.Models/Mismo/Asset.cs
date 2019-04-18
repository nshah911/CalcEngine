using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class Asset
    {
        public AssetType _Type { get; set; }
        public decimal _CashOrMarketValueAmount { get; set; }
        public string _AccountIdentifier { get; set; }
        public string _HolderName { get; set; }
    }

    public enum AssetType
    {
        ///Default to Unknown
        Unknown,

        /// <remarks/>
        Automobile,

        /// <remarks/>
        Bond,

        /// <remarks/>
        BridgeLoanNotDeposited,

        /// <remarks/>
        CashOnHand,

        /// <remarks/>
        CertificateOfDepositTimeDeposit,

        /// <remarks/>
        CheckingAccount,

        /// <remarks/>
        EarnestMoneyCashDepositTowardPurchase,

        /// <remarks/>
        GiftsTotal,

        /// <remarks/>
        GiftsNotDeposited,

        /// <remarks/>
        LifeInsurance,

        /// <remarks/>
        MoneyMarketFund,

        /// <remarks/>
        MutualFund,

        /// <remarks/>
        NetWorthOfBusinessOwned,

        /// <remarks/>
        OtherLiquidAssets,

        /// <remarks/>
        OtherNonLiquidAssets,

        /// <remarks/>
        PendingNetSaleProceedsFromRealEstateAssets,

        /// <remarks/>
        RelocationMoney,

        /// <remarks/>
        RetirementFund,

        /// <remarks/>
        SaleOtherAssets,

        /// <remarks/>
        SavingsAccount,

        /// <remarks/>
        SecuredBorrowedFundsNotDeposited,

        /// <remarks/>
        Stock,

        /// <remarks/>
        TrustAccount,

        /// <remarks/>
        BorrowerEstimatedTotalAssets,

        /// <remarks/>
        GrantsNotDeposited,

        /// <remarks/>
        RealEstateOwned,
    }
}
