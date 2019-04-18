using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class PriorAdverseRating
    {
        public string _Code { get; set; }
        public string _DateRaw { get; set; }
        public DateTime _Date
        {
            get
            {
                if (DateTime.TryParse(_DateRaw, out DateTime d))
                    return d;
                else
                    return DateTime.MinValue;
            }
        }
        public PriorAdverseRatingType _Type { get; set; }
    }

    public enum PriorAdverseRatingType
    {
        Unknown,

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
