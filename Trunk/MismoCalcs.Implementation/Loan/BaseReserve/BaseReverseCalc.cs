using CalcEngine.Models;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MismoCalcs.Implementation.Loan.BaseReserve
{
    /// <summary>
    /// Base Reserve calculation common to more than 1 product.
    /// </summary>
    public class BaseReverseCalc : IBaseReserveCalc
    {
        private readonly MortgageTerms _mortgageTerms;
        private readonly LoanPurpose _loanPurpose;
        private readonly ILTVCalc _ltvCalc;
        private readonly ICreditCalcs _creditCalcs;
        private readonly ICreditLiabilityCalcs _creditLiabilityCalcs;
        private readonly IProposedHousingCalcs _proposedHousingExpenseCalcs;
        private readonly IPresentTotalHousingExpenseCalc _presentTotalHousingExpenseCalc;
        private readonly IReoCalcs _reoCalcs;
        private readonly IDTICalc _dTICalc;
        private readonly string _programCode;
        private readonly LoanProductData _loanProductData;
        private readonly IProposedHousingQualifyingTIA _proposedHousingQualifyingTIA;
        private readonly IQualifyingPIPayment _qualifyingPIPayment;

        public BaseReverseCalc(
            LoanProductData loanProductData,
            MortgageTerms mortgageTerms,
            LoanPurpose loanPurpose,
            ILTVCalc ltvCalc,
            ICreditCalcs creditCalcs,
            ICreditLiabilityCalcs creditLiabilityCalcs,
            IProposedHousingCalcs proposedHousingExpenseCalcs,
            IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc,
            IReoCalcs reoCalcs,
            IDTICalc dTICalc,
            IProposedHousingQualifyingTIA proposedHousingQualifyingTIA,
            IQualifyingPIPayment qualifyingPIPayment,
            string programCode)
        {
            _mortgageTerms = mortgageTerms;
            _loanPurpose = loanPurpose;
            _ltvCalc = ltvCalc;
            _creditCalcs = creditCalcs;
            _dTICalc = dTICalc;
            _programCode = programCode;
            _creditLiabilityCalcs = creditLiabilityCalcs;
            _proposedHousingExpenseCalcs = proposedHousingExpenseCalcs;
            _presentTotalHousingExpenseCalc = presentTotalHousingExpenseCalc;
            _reoCalcs = reoCalcs;
            _proposedHousingQualifyingTIA = proposedHousingQualifyingTIA;
            _qualifyingPIPayment = qualifyingPIPayment;
            _loanProductData = loanProductData;
        }
        public decimal BaseReserve()
        {
            decimal result = 0;
            if(_dTICalc.BackRatio() <= 50)
            {
                if (_ltvCalc.LTV() <= 80)
                {
                    result = BaseReserveLTVLess80();
                }
                else if (_ltvCalc.LTV() > 80 && _ltvCalc.LTV() <= 90)
                {
                    result = BaseReserveLTVGt80Less90();
                }
                else
                {
                    result = TwelveMonthProposedHousing();
                }
            }
            else
            {
                result = TwelveMonthProposedHousing();
            }
            return result;
        }

        private decimal TwelveMonthProposedHousing()
        {
            decimal result = 0;
            result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 12;
            return result;
        }

        private decimal BaseReserveLTVGt80Less90()
        {
            decimal result = 0;
            if(CreditScore() >= 680
                && LateCountOfLiabilities(12) == 0
                && BankruptCount(48) == 0
                && ForeclosureCount(48) == 0
                //&& (_loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.Attached
                //|| _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.Condominium
                //|| _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.Detached
                //|| _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.HighRiseCondominium
                //|| _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.PUD
                //|| _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.DetachedCondominium
                //)
                )
            {
                if (_mortgageTerms.BaseLoanAmount <= 1000000)
                {
                    result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 3;
                }
                else if (_mortgageTerms.BaseLoanAmount > 10000000 && _mortgageTerms.BaseLoanAmount < 2000000)
                {
                    result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 6;
                }
                else
                {
                    result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 12;
                }
            }
            else
            {
                result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 12;
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
                    if (_mortgageTerms.BaseLoanAmount <= 1000000)
                    {
                        result = GetQualifyingPITIA() * 3;
                    }
                    else if (_mortgageTerms.BaseLoanAmount > 10000000 && _mortgageTerms.BaseLoanAmount < 2000000)
                    {
                        result = GetQualifyingPITIA() * 6;
                    }
                    else
                    {
                        result = GetQualifyingPITIA() * 12;
                    }
                }
                else
                {
                    if (_mortgageTerms.BaseLoanAmount <= 1000000)
                    {
                        result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 3;
                    }
                    else if (_mortgageTerms.BaseLoanAmount > 10000000 && _mortgageTerms.BaseLoanAmount < 2000000)
                    {
                        result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 6;
                    }
                    else
                    {
                        result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 12;
                    }
                }
            }
            return result;
        }

        private decimal GetQualifyingPITIA()
        {
            decimal result = 0;
            result = _qualifyingPIPayment.QualifyingPIAmount() + _proposedHousingQualifyingTIA.QualifyingTIA();
            return result;

        }


        private int GetCountOfConstructionRefData()
        {
            if (_loanPurpose.ConstructionRefinanceData != null)
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
            if (_loanPurpose._PropertyUsageType == CalcEngine.Models.Mismo.PropertyUsageType.PrimaryResidence)
            {
                if (_proposedHousingExpenseCalcs.ProposedTotalHousingPayment() < _presentTotalHousingExpenseCalc.PresentTotalHousingExpense())
                {
                    result = true;
                }
            }
            if (_loanPurpose._PropertyUsageType == PropertyUsageType.Investor || _loanPurpose._PropertyUsageType == PropertyUsageType.SecondHome)
            {
                if (_proposedHousingExpenseCalcs.ProposedTotalHousingPayment() < _reoCalcs.SubTotalCurrentHousingPayment())
                {
                    result = true;
                }
            }
            return result;
        }

        private int CreditScore()
        {
            return _creditCalcs.DecisionCreditScore();
        }

        private int BankruptCount(int withInMonth)
        {
            return _creditCalcs.BankruptcyCount(withInMonth);
        }

        private int ForeclosureCount(int withInMonth)
        {
            return _creditCalcs.ForeclosureCount(withInMonth);
        }
    }
}
