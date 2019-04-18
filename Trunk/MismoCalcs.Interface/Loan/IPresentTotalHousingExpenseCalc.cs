using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Loan
{
    public interface IPresentTotalHousingExpenseCalc
    {
        decimal PresentTotalHousingExpense();

        decimal CurrentTotalHousingExpense();

        decimal NonOccupantCurrentHousingExpense();
    }
}
