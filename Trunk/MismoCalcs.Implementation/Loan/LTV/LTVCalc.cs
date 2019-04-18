using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.LTV
{
    public class LTVCalc : ILTVCalc
    {
        private decimal _loanAmount;
        private decimal _purchasePrice;
        private decimal _appraisedValue;

        public LTVCalc(decimal loanAmount, decimal purchasePrice, decimal appraisedValue)
        {
            _loanAmount = loanAmount;
            _purchasePrice = purchasePrice;
            _appraisedValue = appraisedValue;
        }
        public decimal LTV()
        {
            try
            {
                decimal denominator = 0.0M;
                decimal ltv = 0.0M;

                denominator = (_purchasePrice < _appraisedValue && _purchasePrice > 0) ? _purchasePrice : _appraisedValue;

                if (denominator > 0)
                {
                    decimal value = 0.0M;
                    value = ((_loanAmount / denominator)) * 100M;
                    ltv = Math.Round(value, 3, MidpointRounding.AwayFromZero);
                }

                return ltv;
            }
            catch
            {
                throw;
            }
        }
    }
}
