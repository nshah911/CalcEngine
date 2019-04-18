using CalcEngine.Models.BankStatements;
using MismoCalcs.Interface.BankStatements;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.BankStatements
{
    public class BankStatementCalcs : IBankStatementCalcs
    {
        private readonly List<BankStatement> _bankStatements;

        public decimal AvgDepositAmount(BankStatement bankStatement)
        {
            decimal result = 0;
            result = bankStatement._AvgDepositAmt;
            return result;
        }

        public bool IsDepositWithinLimit(decimal limit, BankStatement bankStatement)
        {
            bool result = true;
            decimal dblTotalDeposit = SumOfDepositAmount(bankStatement);
            if (dblTotalDeposit < (bankStatement._ProfitLossRevenue * limit))
            {
                result = false;
            }
            return result;
        }

        public decimal SumOfDepositAmount(BankStatement bankStatement)
        {
            decimal totalDeposits = 0;
            if (bankStatement.BankStatementDetails.Count > 0)
            {
                foreach (BankStatementDetails stmtDetail in bankStatement.BankStatementDetails)
                {
                    totalDeposits += stmtDetail.DepositAmount.HasValue ? stmtDetail.DepositAmount.Value : 0;
                }

                if (bankStatement._PercentOfOwnership > 0)
                {
                    totalDeposits = totalDeposits * bankStatement._PercentOfOwnership / 100;
                }
                else
                {
                    totalDeposits = 0;
                }
            }
            return totalDeposits;
        }
    }
}
