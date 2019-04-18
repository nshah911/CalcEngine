using CalcEngine.Models.BankStatements;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Requests
{
    public class LoanCalcRequestModel
    {
        public string fileId { get; set; }

        public string userId { get; set; }

        public IUSLoanInfo IUSLoanInfo { get; set; }

        public MismoContentModel MismoContent { get; set; }

        public List<CreditContentModel> CreditContents { get; set; }

        public List<BankStatementContentModel> BankStatementContents { get; set; }
    }

    public class IUSLoanInfo
    {
        public MortgageTermsInfo MortgageTermsInfo { get; set; }
        public List<ReoPropertyInfo> ReoPropertiesInfo { get; set; }
    }

    public class MortgageTermsInfo
    {
        public string QualifyingPI { get; set; }
    }

    public class ReoPropertyInfo
    {
        public string ReoId { get; set; }
        public string AcquireDate { get; set; }
        public string PercentOfParticipation { get; set; }
        public string PercentOfRental { get; set; }
    }
}
