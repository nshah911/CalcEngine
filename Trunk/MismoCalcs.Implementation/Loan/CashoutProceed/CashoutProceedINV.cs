using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.CashoutProceed
{
    public class CashoutProceedINV : ICashoutProceed
    {
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly ILTVCalc _lTVCalc;
        public CashoutProceedINV(ICashFromBorrower cashFromBorrower, ILTVCalc lTVCalc)
        {
            _cashFromBorrower = cashFromBorrower;
            _lTVCalc = lTVCalc;
        }
        public decimal CashoutProceedAmount()
        {
            decimal result = 0;
            if(_cashFromBorrower.CashFromBorrower() < 0)
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
