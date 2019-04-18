using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Credit
{
    public interface  ICreditCalcs
    {
        int DecisionCreditScore();

        int BankruptcyCount(int withinMonths);

        int ForeclosureCount(int withinMonths);
    }
}
