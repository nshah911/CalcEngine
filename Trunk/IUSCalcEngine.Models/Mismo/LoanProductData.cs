using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class LoanProductData
    {
        public LoanFeatures LoanFeatures{ get; set; }
        public List<RateAdjustment> RateAdjustment { get; set; }
    }

    public class RateAdjustment
    {
        public decimal FirstRateAdjustmentMonths { get; set; }
        public decimal DurationMonths { get; set; }
    }

    public class LoanFeatures
    {
        public GSEPropertyType GSEPropertyType { get; set; }
    }
    public enum GSEPropertyType
    {
        /// <remarks/>
        Attached,

        /// <remarks/>
        Condominium,

        /// <remarks/>
        Cooperative,

        /// <remarks/>
        Detached,

        /// <remarks/>
        HighRiseCondominium,

        /// <remarks/>
        ManufacturedHousing,

        /// <remarks/>
        Modular,

        /// <remarks/>
        PUD,

        /// <remarks/>
        ManufacturedHousingSingleWide,

        /// <remarks/>
        ManufacturedHousingDoubleWide,

        /// <remarks/>
        DetachedCondominium,

        /// <remarks/>
        ManufacturedHomeCondominium,

        /// <remarks/>
        ManufacturedHousingMultiWide,

        /// <remarks/>
        ManufacturedHomeCondominiumOrPUDOrCooperative,
    }
}
