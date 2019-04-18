using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.CashoutProceed
{
    public class CashoutProceedAQnBKPR : ICashoutProceed
    {
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly ILTVCalc _lTVCalc;
        public CashoutProceedAQnBKPR(ICashFromBorrower cashFromBorrower, ILTVCalc lTVCalc)
        {
            _cashFromBorrower = cashFromBorrower;
            _lTVCalc = lTVCalc;
        }
        public decimal CashoutProceedAmount()
        {
            return 0;
        }
    }
}
