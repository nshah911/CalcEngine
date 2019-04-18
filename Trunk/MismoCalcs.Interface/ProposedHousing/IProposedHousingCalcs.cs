using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.ProposedHousing
{
    public interface IProposedHousingCalcs
    {
        decimal ProposedTotalHousingPayment();
        decimal AllTypesTotalProposedHousingExpense();
        decimal FirstMortgagePIAmount();
    }
}
