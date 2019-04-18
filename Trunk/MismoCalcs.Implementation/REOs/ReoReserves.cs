using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.REOs
{
    public class ReoReserves : IReoReserves
    {
        private readonly MortgageTerms _mortgageTerms;
        private readonly ICreditLiabilityCalcs _creditLiabilityCalcs;
        private readonly ILTVCalc _ltvCalc;
        private readonly IEnumerable<ReoProperty> _reoProperties;
        private readonly IReoModelCommonCalcs _reoModelCommonCalcs;

        public ReoReserves(MortgageTerms mortgageTerms, ILTVCalc ltvCalc, IReoModelCommonCalcs reoModelCommonCalcs, ICreditLiabilityCalcs creditLiabilityCalcs, IEnumerable<ReoProperty> reoProperties)
        {
            _mortgageTerms = mortgageTerms;
            _ltvCalc = ltvCalc;
            _creditLiabilityCalcs = creditLiabilityCalcs;
            _reoModelCommonCalcs = reoModelCommonCalcs;
            _reoProperties = reoProperties;
        }
        
        public decimal AddReserveForOtherFinancedReo()
        {
            decimal result = 0;
            foreach( ReoProperty reoProperty in _reoProperties)
            {
                var Month = (DateTime.Today.Month - reoProperty.AcquireDate.Month) + 12 * (DateTime.Today.Year - reoProperty.AcquireDate.Year);
                if (_mortgageTerms.BaseLoanAmount <= 679650 && _ltvCalc.LTV() <= 80
                && LateCountOfLiabilities(12) == 0 && Month > 12)
                {
                    result += 0;
                }
                else
                {
                    result += Total1MonthReserve(reoProperty);
                }
            }

            return result;
        }


        private decimal Total1MonthReserve(ReoProperty reoProperty)
        {
            return _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)


                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.TotalNetRental(reoProperty, CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.TotalMaintainanceExpAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y);
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
