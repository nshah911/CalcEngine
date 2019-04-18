using CalcEngine.Models.BankStatements;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.BankStatements;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.BankStatements
{
    public class BankStatementCommonCalcs : IBankStatementCommonCalcs
    {
        private readonly IEnumerable<BankStatement> _responseGroups;
        public BankStatementCommonCalcs(IEnumerable<BankStatement> responseGroups)
        {
            _responseGroups = responseGroups;
        }

        public decimal TotalDeposit()
        {
            throw new NotImplementedException();
        }
    }
}
