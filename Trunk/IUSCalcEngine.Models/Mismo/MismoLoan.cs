using CalcEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class MismoLoan
    {
        public MismoLoan()
        { }

        public AdditionalCaseData AdditionalCaseData { get; set; }

        public List<Borrower> Borrowers { get; set; }

        public List<Asset> Assets { get; set; }

        public List<Liability> Liabilities { get; set; }

        public List<ReoProperty> ReoProperties { get; set; }

        public MortgageTerms MortgageTerms { get; set; }

        public List<ProposedHousingExpense> ProposedHousingExpenses { get; set; }

        public TransactionDetail TransactionDetail { get; set; }

        public LoanPurpose LoanPurpose { get; set; }

        public LoanProductData LoanProductData { get; set; }
    }
}
