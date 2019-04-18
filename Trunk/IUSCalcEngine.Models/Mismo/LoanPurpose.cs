using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class LoanPurpose
    {
        public LoanPurposeType _Type { get; set; }

        public PropertyUsageType _PropertyUsageType { get; set; }

        public List<ConstructionRefinanceData> ConstructionRefinanceData { get; set; }
    }

    public enum LoanPurposeType
    {
        Unknown,
        ConstructionOnly,
        ConstructionToPermanent,
        Other,
        Purchase,
        Refinance
    }

    public enum PropertyUsageType
    {
        Unknown,

        /// <remarks/>
        Investment,

        /// <remarks/>
        Investor,

        /// <remarks/>
        PrimaryResidence,

        /// <remarks/>
        SecondHome,

        /// <remarks/>
        Other,
    }
}
