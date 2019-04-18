using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.TransactionDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.TransactionDetail
{
    public class TransactionDetailRefinanceCalcs : ITransactionDetailRefinanceCalcs
    {
        private readonly CalcEngine.Models.Mismo.TransactionDetail _transactionDetail;
        private readonly LoanPurposeType _loanPurposeType;

        public TransactionDetailRefinanceCalcs(CalcEngine.Models.Mismo.TransactionDetail transactionDetail,
            LoanPurposeType loanPurposeType)
        {
            _transactionDetail = transactionDetail;
            _loanPurposeType = loanPurposeType;
        }

        public decimal RefinanceTotalCosts()
        {
            decimal total = 0.00m;

            if (_loanPurposeType == LoanPurposeType.Refinance)
            {
                total = _transactionDetail.RefinanceIncludingDebtsToBePaidOffAmount
                + _transactionDetail.PrepaidItemsEstimatedAmount
                + _transactionDetail.EstimatedClosingCostsAmount
                + _transactionDetail.MIAndFundingFeeTotalAmount
                + _transactionDetail.BorrowerPaidDiscountPointsTotalAmount;
            }

            return total;
        }
    }
}
