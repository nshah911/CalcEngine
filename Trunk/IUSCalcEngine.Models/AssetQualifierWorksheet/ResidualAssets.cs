namespace CalcEngine.Models.AssetQualifierWorksheet
{
    public class ResidualAssets
    {
        public decimal LoanAmount { get; set; }

        public decimal SubordinateLienAmount { get; set; }

        public CashFromBorrower CashFromBorrower { get ; set;}

        public decimal TotalResidualAsset { get; set; }
    }

    public class CashFromBorrower
    {
        public decimal TotalCosts { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal CashFromBorrowerTotal { get; set; }
    }
}