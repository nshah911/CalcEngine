using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.AssetQualifierWorksheet
{
    public class Assets
    {
        public LiquidAssets LiquidAssets { get; set; }
        public Stocks Stocks { get; set; }
        public MutualFunds MutualFunds { get; set; }
        public Retirement Retirement { get; set; }
        public decimal TotalUsable { get; set; }
        public decimal TotalVerifiedAmount { get; set; }
    }

    public class LiquidAssets
    {
        public decimal  OriginalAmt { get; set; }
        public decimal UsableAmt { get; set; }
    }

    public class Stocks
    {
        public decimal OriginalAmt { get; set; }
        public decimal UsableAmt { get; set; }
    }

    public class MutualFunds
    {
        public decimal OriginalAmt { get; set; }
        public decimal UsableAmt { get; set; }
    }

    public class Retirement
    {
        public decimal OriginalAmt { get; set; }
        public decimal UsableAmt { get; set; }
    }
}
