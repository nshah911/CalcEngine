using CalcEngine.Models;
using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.TransactionDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan
{
    public class CashFromBorrowerCalcs : ICashFromBorrower
    {
        private readonly ITransactionDetailCommonCalcs _transactionDetailCommonCalcs;
        private readonly ITransactionDetailRefinanceCalcs _transactionDetailRefinanceCalcs;
        private readonly LoanPurposeType _loanPurposeType;
        private readonly MismoLoan _mismoLoan;

        public CashFromBorrowerCalcs(ITransactionDetailCommonCalcs transactionDetailCommonCalcs, 
            ITransactionDetailRefinanceCalcs transactionDetailRefinanceCalcs, MismoLoan mismoLoan)
        {
            _transactionDetailCommonCalcs = transactionDetailCommonCalcs;
            _transactionDetailRefinanceCalcs = transactionDetailRefinanceCalcs;
            _loanPurposeType = mismoLoan.LoanPurpose._Type;
            _mismoLoan = mismoLoan;
        }
        public decimal TotalCost()
        {
            decimal totalCost = 0M;

            if (_loanPurposeType == LoanPurposeType.Purchase)
                totalCost = _transactionDetailCommonCalcs.PurchaseTotalCosts();

            if (_loanPurposeType == LoanPurposeType.Refinance)
                totalCost = _transactionDetailRefinanceCalcs.RefinanceTotalCosts();

            return totalCost;
        }

        public decimal TotalCredit()
        {
            decimal totalCredit = 0M;

            if (_loanPurposeType == LoanPurposeType.Purchase)
                totalCredit = _transactionDetailCommonCalcs.PurchaseTotalCredits() + _mismoLoan.MortgageTerms.BaseLoanAmount;

            return totalCredit;
        }

        public decimal CashFromBorrower()
        {
            decimal cashBack = 0;
            if (_loanPurposeType == LoanPurposeType.Purchase)
            {
                decimal difference = TotalCost() - TotalCredit();
                if (difference > 0)
                {
                    cashBack = difference;
                }
                else
                {
                    cashBack = 0;
                }
            }
            else if (_loanPurposeType == LoanPurposeType.Refinance)
            {
                cashBack = TotalCost();
            }
            else
            {
                cashBack = 0;
            }
            return cashBack;
        }
    }
}
