using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Loan
{
    public interface ICashFromBorrower
    {
        decimal CashFromBorrower();
        decimal TotalCredit();
        decimal TotalCost();
    }
}
