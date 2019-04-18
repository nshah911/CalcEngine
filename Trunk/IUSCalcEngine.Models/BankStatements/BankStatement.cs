using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.BankStatements
{
    public class BankStatement
    {
        public BankStatementType _BankStatementType { get; set; }
        public string _BankName { get; set; }
        public string _AccountNumber { get; set; }
        public string _NonBorrowingEntity { get; set; }
        public decimal _CurrentBalance { get; set; }
        public decimal _AvgDepositAmt { get; set; }
        public decimal _ProfitLossRevenue { get; set; }
        public decimal _ProfitLossNetIncome { get; set; }
        public ExpenseFactorType _ExpenseFactorType { get; set; }
        public decimal _CPAorTaxPreparerAmt { get; set; }
        public decimal _PercentOfOwnership { get; set; }
        public string _BusinessName { get; set; }
        public List<BankStatementDetails> BankStatementDetails { get; set; }
    }
    public enum BankStatementType : int
    {
        PersonalAndBusiness = 1,
        Personal = 2,
        Business = 3
    }
    public enum ExpenseFactorType : int
    {
        HighExpense = 1,
        CPAorTaxPreparer = 2,
        OptionalPAndL = 3
    }
}
