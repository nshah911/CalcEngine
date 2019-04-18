using System;
using System.Collections.Generic;
using System.Text;
using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.ProposedHousing;

namespace MismoCalcs.Implementation.ProposedHousing
{
    public class ProposedHousingCommonCalcs : IProposedHousingModelCommonCalcs
    {
        public readonly IEnumerable<CalcEngine.Models.Mismo.ProposedHousingExpense> _proposedHousingExpenses = null;

        public ProposedHousingCommonCalcs(
            IEnumerable<CalcEngine.Models.Mismo.ProposedHousingExpense> proposedHousingExpenses)
        {
            _proposedHousingExpenses = proposedHousingExpenses;
        }

        public decimal Total(ProposedHousingExpenseType type)
        {
            decimal total = 0.00m;

            foreach (CalcEngine.Models.Mismo.ProposedHousingExpense proposedHouseingExpense in _proposedHousingExpenses)
            {
                if (proposedHouseingExpense.HousingExpenseType == type)
                    total += proposedHouseingExpense._PaymentAmount;
            }

            return total;
        }
    }
}
