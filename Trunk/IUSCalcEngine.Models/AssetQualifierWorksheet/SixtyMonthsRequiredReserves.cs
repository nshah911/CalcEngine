using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.AssetQualifierWorksheet
{
    public class SixtyMonthsRequiredReserves
    {

        public decimal Total { get; set; }
    }

    public class MonthlyLiabilities
    {
        public decimal TotalMonthlyLiabilities { get; set; }
        public decimal SubjectPropertyTaxInsAssc { get; set; }
        public decimal PITIAforPrimaryResidence { get; set; }
        public decimal PITIAforSecondHome { get; set; }
        public decimal TotalNetRental { get; set; }
        public decimal Total { get; set; }
    }
}
