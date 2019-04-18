using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class PresentHousingExpense
    {
        public PresentHousingExpenseType HousingExpenseType { get; set; }

        public decimal _PaymentAmount { get; set; }
    }

    public enum PresentHousingExpenseType
    {
        /// Defaults unknown
        Unknown,

        /// <remarks/>
        FirstMortgagePrincipalAndInterest,

        /// <remarks/>
        HazardInsurance,

        /// <remarks/>
        HomeownersAssociationDuesAndCondominiumFees,

        /// <remarks/>
        MI,

        /// <remarks/>
        OtherHousingExpense,

        /// <remarks/>
        OtherMortgageLoanPrincipalAndInterest,

        /// <remarks/>
        RealEstateTax,

        /// <remarks/>
        Rent,

        /// <remarks/>
        GroundRent,

        /// <remarks/>
        FirstMortgagePITI,

        /// <remarks/>
        LeaseholdPayments,

        /// <remarks/>
        MaintenanceAndMiscellaneous,

        /// <remarks/>
        OtherMortgageLoanPrincipalInterestTaxesAndInsurance,

        /// <remarks/>
        Utilities,
    }
}
