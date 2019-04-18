using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.Incomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Incomes
{
    public class IncomeModelCommonCalcs : IIncomeModelCommonCalcs
    {
        //private readonly IEnumerable<Income> _incomes;
        private readonly IEnumerable<Borrower> _borrowers;

        public IncomeModelCommonCalcs(IEnumerable<Borrower> borrowers)
        {
            _borrowers = borrowers;            
        }

        public decimal Total(IncomeType incomeType)
        {
            decimal total = 0;
            foreach (Borrower bor in _borrowers)
            {
                foreach (Income inc in bor._Incomes)
                {
                    if (inc._IncomeType == incomeType)
                        total += inc._MonthlyTotalAmount;
                }
            }
            return total;
        }
    }
}
