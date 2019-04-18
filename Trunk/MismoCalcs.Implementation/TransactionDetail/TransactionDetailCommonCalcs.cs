using MismoCalcs.Interface.TransactionDetail;
using System;
using System.Collections.Generic;
using System.Text;
using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;

namespace MismoCalcs.Implementation.TransactionDetail
{
    public class TransactionDetailCommonCalcs : ITransactionDetailCommonCalcs
    {
        public readonly CalcEngine.Models.Mismo.TransactionDetail _transactionDetail;

        public TransactionDetailCommonCalcs(CalcEngine.Models.Mismo.TransactionDetail trasactionDetail)
        {
            _transactionDetail = trasactionDetail;
        }

        public decimal PurchaseTotalCosts()
        {
            decimal total = 0m;

            total = _transactionDetail.PurchasePriceAmount
                + _transactionDetail.AlterationsImprovementsAndRepairsAmount
                + _transactionDetail.FNMCostOfLandAcquiredSeparatelyAmount
                + _transactionDetail.RefinanceIncludingDebtsToBePaidOffAmount
                + _transactionDetail.PrepaidItemsEstimatedAmount
                + _transactionDetail.EstimatedClosingCostsAmount
                + _transactionDetail.MIAndFundingFeeFinancedAmount
                + _transactionDetail.BorrowerPaidDiscountPointsTotalAmount;

            return total;
        }

        public decimal PurchaseTotalCredits()
        {
            decimal totalPurchaseCredit = 0M;

            totalPurchaseCredit = _transactionDetail.SubordinateLienAmount
                 + _transactionDetail.SellerPaidClosingCostsAmount
                 + _transactionDetail.MIAndFundingFeeFinancedAmount
                 + PurchaseAmountTotal(PurchaseCreditType.RelocationFunds)
                 + PurchaseAmountTotal(PurchaseCreditType.EmployerAssistedHousing)
                 + PurchaseAmountTotal(PurchaseCreditType.LeasePurchaseFund)
                 + PurchaseAmountTotal(PurchaseCreditType.EarnestMoney)
                 + PurchaseAmountTotal(PurchaseCreditType.SweatEquity)
                 + PurchaseAmountTotal(null, PurchaseCreditSourceType.Lender)
                 + PurchaseAmountTotal(PurchaseCreditType.Other);

            return totalPurchaseCredit;
        }

        private decimal PurchaseAmountTotal(PurchaseCreditType? credittype = null, 
            PurchaseCreditSourceType? purchaseCreditSourceType = null)
        {
            decimal total = 0.00m;

            foreach (PurchaseCredit purchaseCredit in _transactionDetail.PurchaseCredits)
            {
                if(credittype == null && purchaseCreditSourceType != null)
                {
                    if (purchaseCredit._SourceType == purchaseCreditSourceType)
                        total += purchaseCredit._Amount;
                }
                else if(purchaseCreditSourceType == null && credittype != null)
                {
                    if (purchaseCredit._Type == credittype)
                        total += purchaseCredit._Amount;
                }
                else
                {
                    if (purchaseCredit._Type == credittype && purchaseCredit._SourceType == purchaseCreditSourceType)
                        total += purchaseCredit._Amount;
                }
            }
            return total;
        }
    }
}
