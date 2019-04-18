using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Implementation.Liabilities;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan
{
    public class ReservesForNonSubjRetainedPropertiesCalc : IReservesForNonSubjRetainedPropertiesCalc
    {
        private readonly decimal _loanAmount;
        private readonly List<ReoProperty> _reoProperties;
        private readonly ILTVCalc _ltvCalc;
        private readonly ICreditLiabilityCalcs _creditLiabilityCalcs;
        private readonly ILiabilityCalcs _liabilityCalcs;        

        public ReservesForNonSubjRetainedPropertiesCalc(decimal loanAmount,
            List<ReoProperty> reoProperties,
            ILTVCalc ltvCalc,
            ICreditLiabilityCalcs creditLiabilityCalcs,
            ILiabilityCalcs liabilityCalcs)
        {
            _loanAmount = loanAmount;
            _reoProperties = reoProperties;
            _ltvCalc = ltvCalc;
            _creditLiabilityCalcs = creditLiabilityCalcs;
            _liabilityCalcs = liabilityCalcs;
        }

        public decimal ReservesForNonSubjRetainedProperties()
        {
            decimal result = 0;

            if(_loanAmount <= 679650 && _ltvCalc.LTV() <= 80 && LateCountOfLiabilities(12) == 0)
            {
                foreach(ReoProperty reo in _reoProperties)
                {
                    var Month = (DateTime.Today.Month - reo.AcquireDate.Month) + 12 * (DateTime.Today.Year - reo.AcquireDate.Year);
                    if(Month > 12)
                    {
                        result += 0;            
                    }
                    else
                    {
                        if(reo._SubjectIndicator == ReoPropertySubjectIndicator.N
                            && (reo._DispositionStatusType == DispositionStatusType.RetainForRental 
                                || reo._DispositionStatusType == DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                            && reo._LienUPBAmount > 0)
                        {
                            result += reo._MaintenanceExpenseAmount + GetTotalOfMortgageAmount(reo.LinkedLiabilities);
                        }
                    }
                }
            }
            else
            {
                foreach (ReoProperty reo in _reoProperties)
                {
                    if (reo._SubjectIndicator == ReoPropertySubjectIndicator.N
                        && (reo._DispositionStatusType == DispositionStatusType.RetainForRental 
                        || reo._DispositionStatusType == DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                        && reo._LienUPBAmount > 0)
                    {
                            result += reo._MaintenanceExpenseAmount + GetTotalOfMortgageAmount(reo.LinkedLiabilities);
                    }
                }
            }
            return result;
        }

        private decimal GetTotalOfMortgageAmount(List<Liability> linkedLiabilities)
        {
            return _liabilityCalcs.TotalMortgagePayment(linkedLiabilities);
        }

        private int LateCountOfLiabilities(int withInMonth)
        {
            return _creditLiabilityCalcs.LatePaymentCount(AccountType.CreditLine, PriorAdverseRatingType.Late30Days, withInMonth)
                    + _creditLiabilityCalcs.LatePaymentCount(AccountType.CreditLine, PriorAdverseRatingType.Late60Days, withInMonth)
                    + _creditLiabilityCalcs.LatePaymentCount(AccountType.CreditLine, PriorAdverseRatingType.Late90Days, withInMonth)
                    + _creditLiabilityCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late30Days, withInMonth)
                    + _creditLiabilityCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late60Days, withInMonth)
                    + _creditLiabilityCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late90Days, withInMonth);

        }
    }
}
