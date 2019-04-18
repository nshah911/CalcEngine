using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class ConstructionRefinanceData
    {
        public GSERefinancePurposeType GSERefinancePurposeType { get; set; }
    }

    public enum GSERefinancePurposeType
    {
        /// <remarks/>
        CashOutDebtConsolidation,

        /// <remarks/>
        CashOutHomeImprovement,

        /// <remarks/>
        CashOutLimited,

        /// <remarks/>
        CashOutOther,

        /// <remarks/>
        NoCashOutFHAStreamlinedRefinance,

        /// <remarks/>
        NoCashOutFREOwnedRefinance,

        /// <remarks/>
        NoCashOutOther,

        /// <remarks/>
        NoCashOutStreamlinedRefinance,

        /// <remarks/>
        ChangeInRateTerm,

        /// <remarks/>
        Other,
    }
}
