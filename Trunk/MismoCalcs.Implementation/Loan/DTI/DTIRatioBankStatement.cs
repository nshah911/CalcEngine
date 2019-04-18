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
    public class DTIRatioBankStatement : IDTICalc
    {
        private readonly IReoCalcs _reoCalcs;
        private readonly IIncomeCalcs _incomeCalcs;
        private readonly IBankStatementCalcs _bankStatementCalcs;
        private readonly IProposedHousingCalcs _proposedHousingCalcs;
        private readonly ILiabilityCalcs _liabilityCalcs;
        private readonly IPresentTotalHousingExpenseCalc _presentTotalHousingExpenseCalc;
        public readonly LoanPurpose _loanPurpose;
        public readonly IEnumerable<BankStatement> _bankStatements;

        public DTIRatioBankStatement(
            IReoCalcs reoCalcs,
            IIncomeCalcs incomeCalcs,
            IBankStatementCalcs bankStatementCalcs,
            ILiabilityCalcs liabilityCalcs,
            IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc,
            LoanPurpose loanPurpose,
            IEnumerable<BankStatement> bankStatemet,
            IProposedHousingCalcs proposedHousingExpenseCalcs)
        {
            _reoCalcs = reoCalcs;
            _incomeCalcs = incomeCalcs;
            _bankStatements = bankStatemet;
            _presentTotalHousingExpenseCalc = presentTotalHousingExpenseCalc;
            _loanPurpose = loanPurpose;
            _liabilityCalcs = liabilityCalcs;
            _proposedHousingCalcs = proposedHousingExpenseCalcs;
            _bankStatementCalcs = bankStatementCalcs;
        }
        public decimal BackRatio()
        {
            decimal result = 0;
                 
            if(TotalIncome() > 0)
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
                + (_reoCalcs.SubTotalCurrentHousingPayment() > 0 ? _reoCalcs.SubTotalCurrentHousingPayment() : 0)
                + TotalAvgDepositAmt();

            return totalIncome;
        }

        private decimal HousingExpenseCalc()
        {
            decimal total = 0;
            if(_loanPurpose._PropertyUsageType == PropertyUsageType.Investment)
            {
                total = _proposedHousingCalcs.AllTypesTotalProposedHousingExpense()
                    + _presentTotalHousingExpenseCalc.NonOccupantCurrentHousingExpense();
            }
            if(_loanPurpose._PropertyUsageType == PropertyUsageType.Investor)
            {
                total = _presentTotalHousingExpenseCalc.CurrentTotalHousingExpense();
            }
            if(_loanPurpose._PropertyUsageType == PropertyUsageType.SecondHome)
            {
                total = _proposedHousingCalcs.AllTypesTotalProposedHousingExpense()
                    + _presentTotalHousingExpenseCalc.CurrentTotalHousingExpense();
            }
            return total;
        }


        private decimal TotalAvgDepositAmt()
        {
            decimal totalAvgDeposit = 0;
            foreach(BankStatement bk in _bankStatements)
            {
                if (_bankStatementCalcs.IsDepositWithinLimit(0.75M, bk))
                {
                    totalAvgDeposit += _bankStatementCalcs.AvgDepositAmount(bk);
                }
            }
            return totalAvgDeposit;
        }

        private decimal TotalLiabilities()
        {
            decimal total = _liabilityCalcs.TotalLiabilitiesNotMortgageHeloc();
            return total;
        }
    }
}
