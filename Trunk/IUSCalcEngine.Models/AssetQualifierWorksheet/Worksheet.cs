using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.AssetQualifierWorksheet
{
    public class Worksheet
    {
        public Assets Assets { get; set; }

        public ResidualAssets ResidualAssets { get; set; }

        public MonthlyLiabilities MonthlyLiabilities { get; set; }

        public SixtyMonthsRequiredReserves SixtyMonthsRequiredReserves { get; set; }

        public string QualifyingPI { get; set; }

        public decimal BaseReserves { get; set; }

        public decimal ReservesForNonSubjRetainedProperties { get; set; }

        public decimal AdditionalReservesRequired { get; set; }

        public decimal UsableCashoutProceeds { get; set; }

        public decimal NetReservesNeededAfterCashOut { get; set; }

        public decimal TotalAdditionalReservesStillRequired { get; set; }

        public decimal TotalRequiredAssetsForTransaction { get; set; }

        public decimal TotalRequiredReserves { get; set; }

        public bool LoanQualify { get; set; }

        public decimal TotalMonthlyIncome { get; set; }

        public decimal TotalMonthlyResidualIncome { get; set; }

        public decimal FundRemain { get; set; }

        public string Date { get; set; }

        public string Message { get; set; }
    }
}
