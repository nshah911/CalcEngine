using MismoCalcs.Interface.Credit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Credit
{
    public class CreditCalcs : ICreditCalcs
    {
        private readonly ICreditCalcs _creditCalcs;

        public CreditCalcs(ICreditCalcs creditCalcs)
        {
            _creditCalcs = creditCalcs;
        }

        public int BankruptcyCount(int withinMonths)
        {
            return _creditCalcs.BankruptcyCount(withinMonths);
        }

        public int DecisionCreditScore()
        {
            return _creditCalcs.DecisionCreditScore();
        }

        public int ForeclosureCount(int withinMonths)
        {
            return _creditCalcs.ForeclosureCount(withinMonths);
        }
    }
}
