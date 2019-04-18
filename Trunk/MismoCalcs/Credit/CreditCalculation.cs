using CalcEngine.Models.Credit;
using MismoCalcs.Interface.Credit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.Credit
{
    public class CreditCalculation
    {
        private readonly ICreditCalcs _creditCalcs;

        public CreditCalculation(ICreditCalcs creditCalcs)
        {
            _creditCalcs = creditCalcs;
        }
    }
}
