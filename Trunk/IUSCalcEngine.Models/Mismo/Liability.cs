using CalcEngine.Models.Credit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class Liability
    {
        public string _ID { get; set; }

        public string _AccountIdentifier { get; set; }

        public LiabilityExclusionIndicator _ExclusionIndicator { get; set; }

        public LiabilityPayoffStatusIndicator _PayoffStatusIndicator { get; set; }

        public LiabilityType _Type { get; set; }

        public decimal _MonthlyPaymentAmount { get; set; }

        public int _RemainingTermMonths { get; set; }

        public CreditLiability CreditLiability { get; set; }
        
    }
    public enum LiabilityExclusionIndicator
    {

        /// Default indicate Unknown <remarks/>
        Unknown,

        N,

        /// <remarks/>
        Y,
    }

    public enum LiabilityPayoffStatusIndicator
    {

        /// Default indicate Unknown <remarks/>
        Unknown,

        N,

        /// <remarks/>
        Y,
    }

    public enum LiabilityType
    {

        /// <remarks/>
        Alimony,

        /// <remarks/>
        ChildCare,

        /// <remarks/>
        ChildSupport,

        /// <remarks/>
        CollectionsJudgementsAndLiens,

        /// <remarks/>
        HELOC,

        /// <remarks/>
        Installment,

        /// <remarks/>
        JobRelatedExpenses,

        /// <remarks/>
        LeasePayments,

        /// <remarks/>
        MortgageLoan,

        /// <remarks/>
        Open30DayChargeAccount,

        /// <remarks/>
        OtherLiability,

        /// <remarks/>
        Revolving,

        /// <remarks/>
        SeparateMaintenanceExpense,

        /// <remarks/>
        OtherExpense,

        /// <remarks/>
        Taxes,

        /// <remarks/>
        CollectionsJudgmentsAndLiens,

        /// <remarks/>
        DeferredStudentLoan,

        /// <remarks/>
        BorrowerEstimatedTotalMonthlyLiabilityPayment,

        /// <remarks/>
        DelinquentTaxes,

        /// <remarks/>
        FirstMortgageBeingFinanced,

        /// <remarks/>
        Garnishments,

        /// <remarks/>
        UnionDues,

        /// <remarks/>
        UnsecuredHomeImprovementLoanInstallment,

        /// <remarks/>
        UnsecuredHomeImprovementLoanRevolving,
    }

}
