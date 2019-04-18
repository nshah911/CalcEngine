using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan
{
    public class QualifyingPIPayment : IQualifyingPIPayment
    {
        private readonly MortgageTerms _mortgageTerms;
        private readonly string _programCode;
        private readonly RateAdjustment _rateAdjustment;
        public QualifyingPIPayment(MortgageTerms mortgageTerms, string programCode, RateAdjustment rateAdjustment)
        {
            _mortgageTerms = mortgageTerms;
            _programCode = programCode;
            _rateAdjustment = rateAdjustment;
        }

        public decimal QualifyingPIAmount()
        {
            decimal qualPayment = 0M;
            decimal rate = 0;
            decimal amortTerm = _mortgageTerms.LoanAmortizationTermMonths;
            if(_mortgageTerms.QualifyingPIRate != 0)
            {
                if (_mortgageTerms.LoanAmortizationType == LoanAmortixationType.AdjustableRate)
                {
                    rate = _mortgageTerms.QualifyingPIRate;
                    if (_programCode.Contains("IO"))
                    {
                        amortTerm -= _rateAdjustment.FirstRateAdjustmentMonths;
                    }
                }
                else
                {
                    rate = _mortgageTerms.RequestedInterestRatePercent;
                }

                // The following formula is used to calculate the fixed monthly payment (P) required to fully amortize a loan of L dollars over a term of n months at a monthly interest rate of c.
                // P = L[c(1 + c)n]/[(1 + c)n - 1]
                //
                // P = Monthly payment
                // L = Loan amount
                // c = Monthly interest (.06 / 12)
                // n = Amort term
                double L = Convert.ToDouble(_mortgageTerms.BaseLoanAmount);
                double c = Convert.ToDouble(rate / 100 / 12); // interest per month
                double n = Convert.ToDouble(amortTerm);
                double P = L * (c * (Math.Pow((1 + c), n) / (Math.Pow((1 + c), n) - 1)));
                qualPayment = Convert.ToDecimal(P);
                qualPayment = Math.Round(qualPayment, 2);
                return qualPayment;
            }
            else
            {
                return 0;
            }
        }
    }
}
