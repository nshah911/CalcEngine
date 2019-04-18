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
    public class BaseReserveCalcBKPrem : IBaseReserveCalc
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
        private readonly IProposedHousingQualifyingTIA _proposedHousingQualifyingTIA;
        private readonly IQualifyingPIPayment _qualifyingPIPayment;
        private readonly LoanProductData _loanProductData;
        private readonly BaseReverseCalc _regularBankStatement;

        public BaseReserveCalcBKPrem(
            LoanProductData loanProductData,
            MortgageTerms mortgageTerms,
            LoanPurpose loanPurpose,
            ILTVCalc ltvCalc,
            ICreditCalcs creditCalcs,
            ICreditLiabilityCalcs creditLiabilityCalcs,
            IProposedHousingCalcs proposedHousingExpenseCalcs,
            IDTICalc dTICalc,
            IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc,
            IReoCalcs reoCalcs,
            IProposedHousingQualifyingTIA proposedHousingQualifyingTIA,
            IQualifyingPIPayment qualifyingPIPayment,
            string programCode)
        {
            _mortgageTerms = mortgageTerms;
            _loanPurpose = loanPurpose;
            _ltvCalc = ltvCalc;
            _dTICalc = dTICalc;
            _creditCalcs = creditCalcs;
            _loanProductData = loanProductData;
            _creditLiabilityCalcs = creditLiabilityCalcs;
            _proposedHousingExpenseCalcs = proposedHousingExpenseCalcs;
            _presentTotalHousingExpenseCalc = presentTotalHousingExpenseCalc;
            _reoCalcs = reoCalcs;
            _programCode = programCode;
            _proposedHousingQualifyingTIA = proposedHousingQualifyingTIA;
            _qualifyingPIPayment = qualifyingPIPayment;

            _regularBankStatement = new BaseReverseCalc(
                   _loanProductData, _mortgageTerms,
                   _loanPurpose, _ltvCalc,
                   _creditCalcs, _creditLiabilityCalcs,
                   _proposedHousingExpenseCalcs, _presentTotalHousingExpenseCalc,
                   _reoCalcs, _dTICalc, _proposedHousingQualifyingTIA, _qualifyingPIPayment,  _programCode);
        }


        public decimal BaseReserve()
        {
            decimal result = 0;
            if(_dTICalc.BackRatio() <= 43)
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
                    result = BaseReserveGT90();
                }
            }
            else
            {
                _regularBankStatement.BaseReserve();
            }
           
            return result;
        }


        private decimal BaseReserveLTVGt80Less90()
        {
            decimal result = 0;
            if (CreditScore() >= 680
                && LateCountOfLiabilities(24) == 0
                && BankruptCount(48) == 0
                && ForeclosureCount(48) == 0
                && (_loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.Attached
                || _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.Condominium
                || _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.Detached
                || _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.HighRiseCondominium
                || _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.PUD
                || _loanProductData.LoanFeatures.GSEPropertyType == GSEPropertyType.DetachedCondominium
                ))
            {
                if (_mortgageTerms.BaseLoanAmount < 2000000)
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
                result = _regularBankStatement.BaseReserve();
            }
            return result;
        }

        private decimal BaseReserveLTVLess80()
        {
            decimal result = 0;

            if (CreditScore() >= 680
                && LateCountOfLiabilities(24) == 0
                && BankruptCount(48) == 0
                && ForeclosureCount(48) == 0)
            {
                if (_programCode.Contains("IO"))
                {
                    if(_mortgageTerms.BaseLoanAmount < 2000000)
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
                    if (_mortgageTerms.BaseLoanAmount < 2000000)
                    {
                        result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 6;
                    }
                    else
                    {
                        result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 12;
                    }
                }
            }
            else
            {
                result = _regularBankStatement.BaseReserve();
            }
            return result;
        }
        
        private decimal BaseReserveGT90()
        {
            decimal result = 0;
            result = _proposedHousingExpenseCalcs.ProposedTotalHousingPayment() * 12;
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

        private decimal GetQualifyingPITIA()
        {
            decimal result = 0;
            result = _qualifyingPIPayment.QualifyingPIAmount() + _proposedHousingQualifyingTIA.QualifyingTIA();
            return result;

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
