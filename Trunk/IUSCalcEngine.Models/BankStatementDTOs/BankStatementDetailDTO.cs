using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.BankStatementDTOs
{
    public class BankStatementDetailDTO
    {
        public Nullable<int> BankStatementSequence { get; set; }

        public Nullable<int> StatementYear { get; set; }

        public Nullable<int> StatementMonth { get; set; }

        public Nullable<decimal> BeginningBalance { get; set; }

        public Nullable<decimal> DepositAmount { get; set; }

        public Nullable<decimal> EndingBalance { get; set; }

        public Nullable<int> NSFCount { get; set; }

        public string TransactionDetail { get; set; }

        public string BatchID { get; set; }

        public List<BankStatementDetailTransactionDTO> BankTransactionDetailDTOs { get; set; }
    }
}
