using CalcEngine.Models.BankStatements;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.BankStatementDTOs
{
    public class BankStatementDetailTransactionDTO
    {
        public System.Guid BankTransactionDetailId { get; set; }

        public System.Guid BankStatementDetailId { get; set; }

        public string TransactionDate { get; set; }

        public string Description { get; set; }

        public decimal? DepositAmount { get; set; }

        public string ActualDepositAmount { get; set; }

        public Nullable<TransactionType> TransactionTypeId { get; set; }

        public bool IsSelected { get; set; }

        public int PageIndex { get; set; }

        public int RowIndex { get; set; }

        public BankStatementCoordinate DatePosition { get; set; }
        public BankStatementCoordinate DescriptionPosition { get; set; }
        public BankStatementCoordinate AmountPosition { get; set; }
    }

    public class BankStatementCoordinate
    {
        public BankStatementCoordinate()
        {
            Bottom = 0; Top = 0; Left = 0; Right = 0; TabIndex = 0;
        }
        public int Bottom { get; set; }

        public int Top { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public int TabIndex { get; set; }
    }
}
