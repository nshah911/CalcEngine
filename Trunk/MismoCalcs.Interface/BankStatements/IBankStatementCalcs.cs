using CalcEngine.Models.BankStatements;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.BankStatements
{
    public interface IBankStatementCalcs
    {
        bool IsDepositWithinLimit(decimal limit, BankStatement bankStatements);
        decimal AvgDepositAmount(BankStatement bankStatements);
        decimal SumOfDepositAmount(BankStatement bankStatements);
    }
}
