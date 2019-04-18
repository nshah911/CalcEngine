using System;
using System.Collections.Generic;

namespace CalcEngine.Models.BankStatements
{
    public class BankStatementDetails
    {
        //public int BankStatementSequence { get; set; }
        public Nullable<int> StatementYear { get; set; }
        public Nullable<int> StatementMonth { get; set; }
        public Nullable<decimal> BeginningBalance { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public Nullable<decimal> EndingBalance { get; set; }
        public Nullable<YesNoType> NSFIndTypeId { get; set; }
        public Nullable<int> NSFCount { get; set; }
        //public Nullable<System.DateTime> CreateDt { get; set; }
        //public Nullable<System.DateTime> EditDt { get; set; }
        public string TransactionDetail { get; set; }
        //public Nullable<bool> IsReviewed { get; set; }

        public List<BankStatementDetailTransaction> BankStatementDetailTransactions { get; set; }
    }

    public enum YesNoType : int
    {
        No = 0,
        Yes = 1
    }
}