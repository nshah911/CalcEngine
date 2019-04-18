using CalcEngine.Logging;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.Liabilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.Liabilities
{
    public class LiabilitiesCalculation
    {
        private readonly ILiabilityModelCommonCalcs _liabilityModelCommonCalcs = null;
        private readonly ICreditLiabilityCalcs _creditLiabilityCalcs = null;
        private readonly ILiabilityCalcs _liabilityCalcs = null;

        public LiabilitiesCalculation(ILiabilityCalcs liabilityCalcs, ILiabilityModelCommonCalcs liabilityModelCommonCalcs,
            ICreditLiabilityCalcs creditLiabilityCalcs)
        {
            _liabilityCalcs = liabilityCalcs;
            _liabilityModelCommonCalcs = liabilityModelCommonCalcs;
            _creditLiabilityCalcs = creditLiabilityCalcs;
        }

        public decimal TotalMonthlyLiabilites(int minRemainingInstallmentMonths)
        {
            this.Info("Calculating Total Monthly Liabilities");
            decimal total = 0;
            total = _liabilityCalcs.TotalMonthlyLiabilites(minRemainingInstallmentMonths);
            return total;
        }
    }
}
