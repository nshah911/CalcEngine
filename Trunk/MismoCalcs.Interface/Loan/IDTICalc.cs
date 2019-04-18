using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Loan
{
    public interface IDTICalc
    {
        decimal BackRatio();
        decimal TotalIncome();
        decimal TotalExpence();
    }
}
