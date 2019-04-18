using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class CreditResponseGroup
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public DateTime ResponseDateTime { get; set; }

        public ResponseData ResponseData { get; set; }
    }

    public class ResponseData
    {
        public CreditResponse CreditResponse { get; set; }
    }

    public class CreditResponse
    {
        public DateTime CreditReportFirstIssuedDate { get; set; }

        public List<CreditLiability> CreditLiabilities { get; set; }

        public List<CreditPublicRecord> CreditPublicRecords { get; set; }

        public List<CreditScore> CreditScores { get; set; }

        public Borrower PrimaryBorrower { get; set; }

        public Borrower CoBorrower { get; set; }
    }
}
