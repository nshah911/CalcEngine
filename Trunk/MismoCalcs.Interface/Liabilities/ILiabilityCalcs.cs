using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Liabilities
{
    public interface ILiabilityCalcs
    {
        decimal TotalMonthlyLiabilites(int minRemainingInstallmentMonths);

        decimal TotalMortgagePayment(IEnumerable<Liability> liabilities);

        decimal TotalLiabilitiesNotMortgageHeloc();
    }
}
