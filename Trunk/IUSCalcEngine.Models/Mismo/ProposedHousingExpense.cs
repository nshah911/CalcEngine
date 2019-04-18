using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class ProposedHousingExpense
    {
        public ProposedHousingExpenseType HousingExpenseType { get; set; }

        public decimal _PaymentAmount { get; set; }
    }

    public enum ProposedHousingExpenseType
    {
        /// Defaults unknown
        Unknown,

        /// <remarks/>
        FirstMortgagePrincipalAndInterest,

        /// <remarks/>
        GroundRent,

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
        FirstMortgagePITI,

        /// <remarks/>
        LeaseholdPayments,

        /// <remarks/>
        MaintenanceAndMiscellaneous,

        /// <remarks/>
        OtherMortgageLoanPrincipalInterestTaxesAndInsurance,

        /// <remarks/>
        Rent,

        /// <remarks/>
        Utilities,
    }
}
