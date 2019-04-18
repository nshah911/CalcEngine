using CalcEngine.Models.BankStatements;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Enums;
using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models
{
    public class IUSLoan
    {
        public DocTypeEnum DocType { get; set; }

        public SourceType Source { get; set; }

        public DateTime GuideLineDt { get; set; }

        public string ImpacProgramCode { get; set; }

        public MismoLoan MismoLoan { get; set; }

        public List<CreditResponseGroup> CreditReports { get; set; }

        public List<BankStatement> BankStatements { get; set; }        

        public bool AllowReserve { get; set; }

    }
}
