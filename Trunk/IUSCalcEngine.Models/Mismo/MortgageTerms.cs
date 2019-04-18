using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class MortgageTerms
    {
        public decimal BaseLoanAmount { get; set; }

        public decimal QualifyingPIRate { get; set; }

        public decimal RequestedInterestRatePercent { get; set; }

        public LoanAmortixationType LoanAmortizationType { get; set; }

        public int LoanAmortizationTermMonths { get; set; }
    }
    public enum LoanAmortixationType
    {
        AdjustableRate,
        Fixed,
        GraduatedPaymentMortgage,
        OtherAmortizationType
    }
}
