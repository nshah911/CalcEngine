using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class CurrentRating
    {
        public string _Code;

        public LiabilityCurrentRatingType _Type;
    }

    public enum LiabilityCurrentRatingType
    {

        /// <remarks/>
        AsAgreed,

        /// <remarks/>
        BankruptcyOrWageEarnerPlan,

        /// <remarks/>
        ChargeOff,

        /// <remarks/>
        Collection,

        /// <remarks/>
        CollectionOrChargeOff,

        /// <remarks/>
        Foreclosure,

        /// <remarks/>
        ForeclosureOrRepossession,

        /// <remarks/>
        Late30Days,

        /// <remarks/>
        Late60Days,

        /// <remarks/>
        Late90Days,

        /// <remarks/>
        LateOver120Days,

        /// <remarks/>
        NoDataAvailable,

        /// <remarks/>
        Repossession,

        /// <remarks/>
        TooNew,

        /// <remarks/>
        WageEarnerPlan,
    }
}
