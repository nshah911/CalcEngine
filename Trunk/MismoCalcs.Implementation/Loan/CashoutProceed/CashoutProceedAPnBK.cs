using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.CashoutProceed
{
    public class CashoutProceedAPnBK : ICashoutProceed
    {
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly ILTVCalc _lTVCalc;

        public CashoutProceedAPnBK(ICashFromBorrower cashFromBorrower, ILTVCalc lTVCalc)
        {
            _cashFromBorrower = cashFromBorrower;
            _lTVCalc = lTVCalc;
        }

        public decimal CashoutProceedAmount()
        {
            decimal result = 0;
            if (_cashFromBorrower.CashFromBorrower() < 0 && _lTVCalc.LTV() <= 80)
            {
                result = Math.Abs(_cashFromBorrower.CashFromBorrower());
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}
