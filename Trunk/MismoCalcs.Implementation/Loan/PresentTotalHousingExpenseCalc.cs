using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MismoCalcs.Implementation.Loan
{
    public class PresentTotalHousingExpenseCalc : IPresentTotalHousingExpenseCalc
    {
        public readonly IEnumerable<ReoProperty> _reoProperties;
        public readonly IEnumerable<Borrower> _borrowers;
        public readonly LoanPurpose _loanPurpose;

        public PresentTotalHousingExpenseCalc(
            IEnumerable<ReoProperty> reoProperties,
            LoanPurpose loanPurpose,
            IEnumerable<Borrower> borrowers)
        {
            _reoProperties = reoProperties;
            _loanPurpose = loanPurpose;
            _borrowers = borrowers;
        }

        public decimal PresentTotalHousingExpense()
        {
            if (_loanPurpose._PropertyUsageType == PropertyUsageType.PrimaryResidence)
                return CurrentTotalHousingExpense();
            else
                return InvestorSecondHomePresentTotalHousingExpense();
        }

        public decimal CurrentTotalHousingExpense()
        {
            decimal total = 0.00m;
            foreach(Borrower borrower in _borrowers)
            {
                foreach(PresentHousingExpense expense in borrower.PresentHousingExpenses)
                {
                    total += expense._PaymentAmount;
                }
            }
            return total;
        }

        private decimal InvestorSecondHomePresentTotalHousingExpense()
        {
            decimal total = 0.00m;

            foreach (ReoProperty reo in _reoProperties)
            {
                if (reo._SubjectIndicator == ReoPropertySubjectIndicator.Y)
                {
                    total += reo._LienInstallmentAmount
                        + reo._MaintenanceExpenseAmount;
                }
            }

            return total;
        }

        public decimal NonOccupantCurrentHousingExpense()
        {
            decimal total = 0.00m;
            foreach (Borrower borrower in _borrowers)
            {
                Declaration declaration = borrower._Declaration.FirstOrDefault();
                if(declaration != null && declaration.IntentToOccupyType == YesOrNo.No)
                {
                    foreach (PresentHousingExpense expense in borrower.PresentHousingExpenses)
                    {
                        total += expense._PaymentAmount;
                    }
                }
                
            }
            return total;
        }
    }
}
