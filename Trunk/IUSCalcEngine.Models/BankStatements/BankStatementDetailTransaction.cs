using System;

namespace CalcEngine.Models.BankStatements
{
    public class BankStatementDetailTransaction
    {
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public Nullable<TransactionType> TransactionTypeId { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public Nullable<int> PageIndex { get; set; }
        public Nullable<int> RowIndex { get; set; }
        public Nullable<System.DateTime> CreateDt { get; set; }
        public Nullable<System.DateTime> EditDt { get; set; }
    }

    public enum TransactionType : int
    {
        Other = 0,
        DirectDeposit = 1
    }
}