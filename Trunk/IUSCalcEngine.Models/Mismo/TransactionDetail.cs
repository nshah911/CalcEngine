using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class TransactionDetail
    {
        public decimal SubordinateLienAmount { get; set; }

        public decimal SellerPaidClosingCostsAmount { get; set; }

        public decimal PurchasePriceAmount { get; set; }

        public decimal AlterationsImprovementsAndRepairsAmount { get; set; }

        public decimal FNMCostOfLandAcquiredSeparatelyAmount { get; set; }

        public decimal RefinanceIncludingDebtsToBePaidOffAmount { get; set; }

        public decimal PrepaidItemsEstimatedAmount { get; set; }

        public decimal EstimatedClosingCostsAmount { get; set; }

        public decimal MIAndFundingFeeTotalAmount { get; set; }

        public decimal BorrowerPaidDiscountPointsTotalAmount { get; set; }

        public decimal FREReservesAmount { get; set; }

        public decimal MIAndFundingFeeFinancedAmount { get; set; }

        public PurchaseCredit[] PurchaseCredits { get; set; }

    }
}
