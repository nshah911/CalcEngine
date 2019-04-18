using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.ProposedHousing;
using CalcEngine.Models.Credit;
using MismoCalcs.Interface.REOs;
using Microsoft.Extensions.Logging;

namespace MismoCalcs.Implementation.Loan.BaseReserve
{
    /// <summary>
    /// Base Reserve calculation specific to the Asset Qualifier product.
    /// </summary>
    public class BaseReverseCalcAssetQualifier : IBaseReserveCalc
    {
        private readonly MortgageTerms _mortgageTerms;
        private readonly LoanPurpose _loanPurpose;
        private readonly ILTVCalc _ltvCalc;
        private readonly ICreditCalcs _creditCalcs;
        private readonly ICreditLiabilityCalcs _creditLiabilityCalcs;
        private readonly IProposedHousingCalcs _proposedHousingExpenseCalcs;
        private readonly IPresentTotalHousingExpenseCalc _presentTotalHousingExpenseCalc;
        private readonly IReoCalcs _reoCalcs;
        private readonly string _programCode;
        private readonly IProposedHousingQualifyingTIA _proposedHousingQualifyingTIA;
        private readonly IQualifyingPIPayment _qualifyingPIPayment;


        public BaseReverseCalcAssetQualifier(
            MortgageTerms mortgageTerms,
            LoanPurpose loanPurpose, 
            ILTVCalc ltvCalc, 
            ICreditCalcs creditCalcs,
            ICreditLiabilityCalcs creditLiabilityCalcs,
            IProposedHousingCalcs proposedHousingExpenseCalcs,
            IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc,
            IReoCalcs reoCalcs,
            IProposedHousingQualifyingTIA proposedHousingQualifyingTIA,
            IQualifyingPIPayment qualifyingPIPayment,
            string programCode)
        {
            _mortgageTerms = mortgageTerms;
            _loanPurpose = loanPurpose;
            _ltvCalc = ltvCalc;
            _creditCalcs = creditCalcs;
            _creditLiabilityCalcs = creditLiabilityCalcs;
            _proposedHousingExpenseCalcs = proposedHousingExpenseCalcs;
            _presentTotalHousingExpenseCalc = presentTotalHousingExpenseCalc;
            _reoCalcs = reoCalcs;
            _proposedHousingQualifyingTIA = proposedHousingQualifyingTIA;
            _qualifyingPIPayment = qualifyingPIPayment;
            _programCode = programCode;
        }

        public decimal BaseReserve()
        {
            decimal result = 0;
            if(_ltvCalc.LTV() <= 80)
            {
                result = BaseReserveLTVLess80();  
            }
            else if(_ltvCalc.LTV() > 80 && _ltvCalc.LTV() <= 90)
            {
                result = BaseReserveLTVGt80Less90();
            }
            else
            {
                result = _programCode.Contains("IO") ? _qualifyingPIPayment.QualifyingPIAmount() * 12 
                    : _proposedHousingExpenseCalcs.FirstMortgagePIAmount();
            }
            return result;
        }

        private decimal BaseReserveLTVGt80Less90()
        {
            decimal result = 0;
            if (CreditScore() >= 680 && LateCountOfLiabilities(12) == 0 && BankruptCount(4) == 0)
            {
                if (_programCode.Contains("IO"))
                {
                    if (_mortgageTerms.BaseLoanAmount >= 2000000)
                    {
                        result = _qualifyingPIPayment.QualifyingPIAmount() * 12;
                    }
                    else
                    {
                        result = _qualifyingPIPayment.QualifyingPIAmount() * 6;
                    }
                }
                else
                {
                    if (_mortgageTerms.BaseLoanAmount >= 2000000)
                    {
                        result = _proposedHousingExpenseCalcs.FirstMortgagePIAmount() * 12;
                    }
                    else
                    {
                        result = _proposedHousingExpenseCalcs.FirstMortgagePIAmount() * 6;
                    }
                }
                
            }
            else
            {
                result = _qualifyingPIPayment.QualifyingPIAmount() * 12;
            }
            return result;
        }

        private decimal BaseReserveLTVLess80()
        {
            decimal result = 0;
            if (_loanPurpose._Type == LoanPurposeType.Refinance
                    //&& GetCountOfConstructionRefData() > 0
                    && _mortgageTerms.BaseLoanAmount <= 679650
                    && LateCountOfLiabilities(12) == 0
                    && ProposedIsLessThanPresentHousing() == true
                    && BankruptCount(120) == 0
                    && ForeclosureCount(120) == 0)
            {
                result = 0;
            }
            else
            {
                if (_programCode.Contains("IO"))
                {
                    if (_mortgageTerms.BaseLoanAmount < 1000000)
                    {
                        result = _qualifyingPIPayment.QualifyingPIAmount() * 3;
                    }
                    else if (_mortgageTerms.BaseLoanAmount >= 10000001 && _mortgageTerms.BaseLoanAmount < 2000000)
                    {
                        result = _qualifyingPIPayment.QualifyingPIAmount() * 6;
                    }
                    else
                    {
                        result = _qualifyingPIPayment.QualifyingPIAmount() * 12;
                    }
                }
                else
                {
                    if (_mortgageTerms.BaseLoanAmount < 1000000)
                    {
                        result = _proposedHousingExpenseCalcs.FirstMortgagePIAmount() * 3;
                    }
                    else if (_mortgageTerms.BaseLoanAmount >= 10000001 && _mortgageTerms.BaseLoanAmount < 2000000)
                    {
                        result = _proposedHousingExpenseCalcs.FirstMortgagePIAmount() * 6;
                    }
                    else
                    {
                        result = _proposedHousingExpenseCalcs.FirstMortgagePIAmount() * 12;
                    }
                }
                
            }
            return result;
        }

        private int CreditScore()
        {
            return _creditCalcs.DecisionCreditScore();
        }

        private int ForeclosureCount(int withInMonth)
        {
            return _creditCalcs.ForeclosureCount(withInMonth);
        }

        private int BankruptCount(int withInMonth)
        {
            return _creditCalcs.BankruptcyCount(withInMonth);
        }

        private int GetCountOfConstructionRefData()
        {
            if(_loanPurpose.ConstructionRefinanceData != null)
            {
                return _loanPurpose.ConstructionRefinanceData.Where(t => t.GSERefinancePurposeType == GSERefinancePurposeType.ChangeInRateTerm
                    || t.GSERefinancePurposeType == GSERefinancePurposeType.CashOutDebtConsolidation
                    || t.GSERefinancePurposeType == GSERefinancePurposeType.CashOutHomeImprovement
                    || t.GSERefinancePurposeType == GSERefinancePurposeType.CashOutLimited
                    || t.GSERefinancePurposeType == GSERefinancePurposeType.CashOutOther
                    || t.GSERefinancePurposeType == GSERefinancePurposeType.NoCashOutOther).Count();
            }
            else
            {
                return 0;
            }
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

        private bool ProposedIsLessThanPresentHousing()
        {
            bool result = false;
            if(_loanPurpose._PropertyUsageType == CalcEngine.Models.Mismo.PropertyUsageType.PrimaryResidence)
            {
                if(_proposedHousingExpenseCalcs.ProposedTotalHousingPayment() < _presentTotalHousingExpenseCalc.PresentTotalHousingExpense())
                {
                    result = true;
                }
            }
            if(_loanPurpose._PropertyUsageType == PropertyUsageType.Investor || _loanPurpose._PropertyUsageType == PropertyUsageType.SecondHome)
            {
                if(_proposedHousingExpenseCalcs.ProposedTotalHousingPayment() < _reoCalcs.SubTotalCurrentHousingPayment())
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
