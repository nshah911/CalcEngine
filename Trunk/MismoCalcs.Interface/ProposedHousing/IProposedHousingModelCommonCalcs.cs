using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.ProposedHousing
{
    public interface IProposedHousingModelCommonCalcs
    {
        decimal Total(ProposedHousingExpenseType type);

    }
}
