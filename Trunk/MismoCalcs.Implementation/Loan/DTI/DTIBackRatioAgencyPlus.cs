using CalcEngine.Models.BankStatements;
using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.BankStatements;
using MismoCalcs.Interface.Incomes;
using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.DTI
{
    public class DTIBackRatioAgencyPlus : IDTICalc
    {
        private readonly IReoCalcs _reoCalcs;
        private readonly IIncomeCalcs _incomeCalcs;
        private readonly IBankStatementCalcs _bankStatementCalcs;
        private readonly IProposedHousingCalcs _proposedHousingCalcs;
        private readonly IPresentTotalHousingExpenseCalc _presentTotalHousingExpenseCalc;
        private readonly ILiabilityCalcs _liabilityCalcs;
        public readonly LoanPurpose _loanPurpose;

        public DTIBackRatioAgencyPlus(
            IReoCalcs reoCalcs,
            IIncomeCalcs incomeCalcs,
            IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc,
            ILiabilityCalcs liabilityCalcs, 
            LoanPurpose loanPurpose,
            IProposedHousingCalcs proposedHousingExpenseCalcs)
        {
            _reoCalcs = reoCalcs;
            _incomeCalcs = incomeCalcs;
            _liabilityCalcs = liabilityCalcs;
            _presentTotalHousingExpenseCalc = presentTotalHousingExpenseCalc;
            _loanPurpose = loanPurpose;
            _proposedHousingCalcs = proposedHousingExpenseCalcs;
        }
        public decimal BackRatio()
        {
            decimal result = 0;

            if (TotalIncome() > 0)
            {
                result = TotalExpence() / TotalIncome();
            }
            return result;
        }

        public decimal TotalExpence()
        {
            decimal totalExpense = +_reoCalcs.RealEstateRental() < 0 ? _reoCalcs.RealEstateRental() : 0
                + _reoCalcs.SubTotalCurrentHousingPayment() < 0 ? _reoCalcs.SubTotalCurrentHousingPayment() : 0
                + TotalLiabilities()
                + HousingExpenseCalc();
            return totalExpense;
        }

        public decimal TotalIncome()
        {
            decimal totalIncome = _incomeCalcs.TotalBorrowerIncome()
                 + (_reoCalcs.RealEstateRental() > 0 ? _reoCalcs.RealEstateRental() : 0)
                 + (_reoCalcs.SubTotalCurrentHousingPayment() > 0 ? _reoCalcs.SubTotalCurrentHousingPayment() : 0);

            return totalIncome;
        }
        private decimal HousingExpenseCalc()
        {
            decimal total = 0;
            if (_loanPurpose._PropertyUsageType == PropertyUsageType.Investment)
            {
                total = _proposedHousingCalcs.AllTypesTotalProposedHousingExpense()
                    + _presentTotalHousingExpenseCalc.NonOccupantCurrentHousingExpense();
            }
            if (_loanPurpose._PropertyUsageType == PropertyUsageType.Investor)
            {
                total = _presentTotalHousingExpenseCalc.CurrentTotalHousingExpense();
            }
            if (_loanPurpose._PropertyUsageType == PropertyUsageType.SecondHome)
            {
                total = _proposedHousingCalcs.AllTypesTotalProposedHousingExpense()
                    + _presentTotalHousingExpenseCalc.CurrentTotalHousingExpense();
            }
            return total;
        }

        private decimal TotalLiabilities()
        {
            decimal total = _liabilityCalcs.TotalLiabilitiesNotMortgageHeloc();
            return total;
        }
    }
}
