using CalcEngine.Models.Credit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Credit
{
    public interface ICreditLiabilityCalcs
    {
        int LatePaymentCount();

        int LatePaymentCount(AccountType accountType);

        int LatePaymentCount(AccountType accountType, PriorAdverseRatingType priorAdverseRatingType);

        int LatePaymentCount(AccountType accountType, PriorAdverseRatingType priorAdverseRatingType, int withinMonths);
    }
}
