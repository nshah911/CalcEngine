using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Incomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.Incomes
{
    public class IncomeCalculation
    {
        private readonly IIncomeModelCommonCalcs _incomeModelCommonCalcs = null;
        private readonly IIncomeCalcs _incomeCalcs = null;
        public IncomeCalculation(IIncomeCalcs incomeCalcs, IIncomeModelCommonCalcs incomeModelCommonCalcs)
        {
            _incomeModelCommonCalcs = incomeModelCommonCalcs;
            _incomeCalcs = incomeCalcs;
        }
    }
}
