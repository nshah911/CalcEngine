using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{

    public class CreditLiability
    {
        public string _CreditLiabilityID { get; set; }

        public string _BorrowerID { get; set; }

        public string _CreditFileID { get; set; }

        public string _CreditTradeReferenceID { get; set; }

        public string _AccountClosedDate { get; set; }

        public string _AccountIdentifier { get; set; }

        public string _AODRaw { get; set; }

        public DateTime _AccountOpenedDate {
            get
            {
                if (DateTime.TryParse(_AODRaw, out DateTime d))
                    return d;
                else
                    return DateTime.MinValue;
            }
        }

        public CreditLiabilityAccountOwnershipType _AccountOwnershipType { get; set; }

        public string _AccountPaidDate { get; set; }

        public string _AccountReportedDate { get; set; }

        public string _AccountBalanceDate { get; set; }

        public DateTime _AccountStatusDate { get; set; }

        public AccountStatusType _AccountStatusType { get; set; }

        public AccountType _AccountType { get; set; }

        public decimal _BalloonPaymentAmount { get; set; }

        public string _BalloonPaymentDueDate { get; set; }

        public decimal _ChargeOffAmount { get; set; }

        public string _ChargeOffDate { get; set; }

        public string _CollateralDescription { get; set; }

        public string _ConsumerDisputeIndicator { get; set; }

        public decimal ? _CreditLimitAmount { get; set; }

        public string _DerogatoryDataIndicator { get; set; }

        public decimal _HighBalanceAmount { get; set; }

        public decimal _HighCreditAmount { get; set; }

        public string _LADRaw { get; set; }

        public DateTime _LastActivityDate
        {
            get
            {
                if (DateTime.TryParse(_LADRaw, out DateTime d))
                    return d;
                else
                    return DateTime.MinValue;
            }
        }

        public string _ManualUpdateIndicator { get; set; }

        public decimal _MonthlyPaymentAmount { get; set; }

        public string _MonthsRemainingCount { get; set; }

        public string _MonthsReviewedCount { get; set; }

        public string _OriginalCreditorName { get; set; }

        public decimal _PastDueAmount { get; set; }

        public string _TermsDescription { get; set; }

        public string _TermsMonthsCount { get; set; }

        public TermsSourceType _TermsSourceType { get; set; }

        public decimal _UnpaidBalanceAmount { get; set; }

        public CreditBusinessType _CreditBusinessType { get; set; }

        public string _CreditCounselingIndicator { get; set; }

        public CreditLoanType _CreditLoanType { get; set; }

        public string _CreditLoanTypeOtherDescription { get; set; }

        public LateCount _LateCount { get; set; }

        public PaymentPattern _PaymentPattern { get; set; }

        public List<PriorAdverseRating> _PriorAdverseRatings { get; set; }

        public HighestAdverseRating _HighestAdverseRating { get; set; }

        public CurrentRating _CurrentRating { get; set; }

        public MostRecentAdverseRating _MostRecentAdverseRating { get; set; }

        public CalcEngine.Models.Mismo.Liability Liability { get; set; }
    }

    public enum CreditLiabilityAccountOwnershipType
    {

        /// <remarks/>
        AuthorizedUser,

        /// <remarks/>
        Comaker,

        /// <remarks/>
        Individual,

        /// <remarks/>
        JointContractualLiability,

        /// <remarks/>
        JointParticipating,

        /// <remarks/>
        Maker,

        /// <remarks/>
        OnBehalfOf,

        /// <remarks/>
        Terminated,

        /// <remarks/>
        Undesignated,

        /// <remarks/>
        Deceased,
    }
    public enum AccountStatusType
    {

        /// <remarks/>
        Closed,

        /// <remarks/>
        Frozen,

        /// <remarks/>
        Open,

        /// <remarks/>
        Paid,

        /// <remarks/>
        Refinanced,

        /// <remarks/>
        Transferred,
    }
    public enum AccountType
    {
        /// <remarks/>
        Unknown,

        /// <remarks/>
        CreditLine,

        /// <remarks/>
        Installment,

        /// <remarks/>
        Mortgage,

        /// <remarks/>
        Open,

        /// <remarks/>
        Revolving,

    }
    public enum TermsSourceType
    {

        /// <remarks/>
        Calculated,

        /// <remarks/>
        Provided,
    }
    public enum CreditBusinessType
    {

        /// <remarks/>
        Advertising,

        /// <remarks/>
        Automotive,

        /// <remarks/>
        Banking,

        /// <remarks/>
        Clothing,

        /// <remarks/>
        CollectionServices,

        /// <remarks/>
        Contractors,

        /// <remarks/>
        DepartmentAndMailOrder,

        /// <remarks/>
        Employment,

        /// <remarks/>
        FarmAndGardenSupplies,

        /// <remarks/>
        Finance,

        /// <remarks/>
        Government,

        /// <remarks/>
        Grocery,

        /// <remarks/>
        HomeFurnishing,

        /// <remarks/>
        Insurance,

        /// <remarks/>
        JewelryAndCamera,

        /// <remarks/>
        LumberAndHardware,

        /// <remarks/>
        MedicalAndHealth,

        /// <remarks/>
        MiscellaneousAndPublicRecord,

        /// <remarks/>
        OilAndNationalCreditCards,

        /// <remarks/>
        PersonalServicesNotMedical,

        /// <remarks/>
        RealEstateAndPublicAccommodation,

        /// <remarks/>
        SportingGoods,

        /// <remarks/>
        UtilitiesAndFuel,

        /// <remarks/>
        Wholesale,
    }
    public enum CreditLoanType
    {

        /// <remarks/>
        Agriculture,

        /// <remarks/>
        Airplane,

        /// <remarks/>
        ApplianceOrFurniture,

        /// <remarks/>
        AttorneyFees,

        /// <remarks/>
        AutoLease,

        /// <remarks/>
        AutoLoanEquityTransfer,

        /// <remarks/>
        Automobile,

        /// <remarks/>
        AutoRefinance,

        /// <remarks/>
        Boat,

        /// <remarks/>
        Business,

        /// <remarks/>
        BusinessCreditCard,

        /// <remarks/>
        ChargeAccount,

        /// <remarks/>
        CheckCreditOrLineOfCredit,

        /// <remarks/>
        ChildSupport,

        /// <remarks/>
        Collection,

        /// <remarks/>
        CollectionAttorney,

        /// <remarks/>
        Comaker,

        /// <remarks/>
        CombinedCreditPlan,

        /// <remarks/>
        CommercialCreditObligation,

        /// <remarks/>
        CommercialLineOfCredit,

        /// <remarks/>
        CommercialMortgage,

        /// <remarks/>
        ConditionalSalesContract,

        /// <remarks/>
        Consolidation,

        /// <remarks/>
        ConventionalRealEstateMortgage,

        /// <remarks/>
        CreditCard,

        /// <remarks/>
        CreditLineSecured,

        /// <remarks/>
        DebitCard,

        /// <remarks/>
        DebtCounselingService,

        /// <remarks/>
        DepositRelated,

        /// <remarks/>
        Educational,

        /// <remarks/>
        Employment,

        /// <remarks/>
        FactoringCompanyAccount,

        /// <remarks/>
        FamilySupport,

        /// <remarks/>
        FarmersHomeAdministrationFHMA,

        /// <remarks/>
        FederalConsolidatedLoan,

        /// <remarks/>
        FHAComakerNotBorrower,

        /// <remarks/>
        FHAHomeImprovement,

        /// <remarks/>
        FHARealEstateMortgage,

        /// <remarks/>
        FinanceStatement,

        /// <remarks/>
        GovernmentBenefit,

        /// <remarks/>
        GovernmentEmployeeAdvance,

        /// <remarks/>
        GovernmentFeeForService,

        /// <remarks/>
        GovernmentFine,

        /// <remarks/>
        GovernmentGrant,

        /// <remarks/>
        GovernmentMiscellaneousDebt,

        /// <remarks/>
        GovernmentOverpayment,

        /// <remarks/>
        GovernmentSecuredDirectLoan,

        /// <remarks/>
        GovernmentSecuredGuaranteeLoan,

        /// <remarks/>
        GovernmentUnsecuredDirectLoan,

        /// <remarks/>
        GovernmentUnsecuredGuaranteeLoan,

        /// <remarks/>
        HomeEquityLineOfCredit,
        HomeEquityLineofCredit,

        /// <remarks/>
        HomeImprovement,

        /// <remarks/>
        HouseholdGoods,

        /// <remarks/>
        HouseholdGoodsAndOtherCollateralAuto,

        /// <remarks/>
        HouseholdGoodsSecured,

        /// <remarks/>
        InstallmentLoan,

        /// <remarks/>
        InstallmentSalesContract,

        /// <remarks/>
        InsuranceClaims,

        /// <remarks/>
        Lease,

        /// <remarks/>
        LenderPlacedInsurance,

        /// <remarks/>
        ManualMortgage,

        /// <remarks/>
        MedicalDebt,

        /// <remarks/>
        MobileHome,

        /// <remarks/>
        Mortgage,

        /// <remarks/>
        NoteLoan,

        /// <remarks/>
        NoteLoanWithComaker,

        /// <remarks/>
        Other,

        /// <remarks/>
        PartiallySecured,

        /// <remarks/>
        PersonalLoan,

        /// <remarks/>
        RealEstateJuniorLiens,

        /// <remarks/>
        RealEstateLoanEquityTransfer,

        /// <remarks/>
        RealEstateMortgageWithoutOtherCollateral,

        /// <remarks/>
        RealEstateSpecificTypeUnknown,

        /// <remarks/>
        Recreational,

        /// <remarks/>
        RecreationalVehicle,

        /// <remarks/>
        Refinance,

        /// <remarks/>
        RefundAnticipationLoan,

        /// <remarks/>
        RentalAgreement,

        /// <remarks/>
        ResidentialLoan,

        /// <remarks/>
        ReturnedCheck,

        /// <remarks/>
        RevolvingBusinessLines,

        /// <remarks/>
        Secured,

        /// <remarks/>
        SecuredByCosigner,

        /// <remarks/>
        SecuredCreditCard,

        /// <remarks/>
        SecuredHomeImprovement,

        /// <remarks/>
        SinglePaymentLoan,

        /// <remarks/>
        SpouseSupport,

        /// <remarks/>
        SummaryOfAccountsWithSameStatus,

        /// <remarks/>
        TimeSharedLoan,

        /// <remarks/>
        Title1Loan,

        /// <remarks/>
        UnknownLoanType,

        /// <remarks/>
        Unsecured,

        /// <remarks/>
        VeteransAdministrationLoan,

        /// <remarks/>
        VeteransAdministrationRealEstateMortgage,

        /// <remarks/>
        BiMonthlyMortgageTermInYears,

        /// <remarks/>
        ConstructionLoan,

        /// <remarks/>
        DeferredStudentLoan,

        /// <remarks/>
        SecondMortgage,

        /// <remarks/>
        SemiMonthlyMortgagePayment,

        /// <remarks/>
        Camper,

        /// <remarks/>
        ConditionalSalesContractRefinance,

        /// <remarks/>
        HomeEquity,

        /// <remarks/>
        ManufacturedHome,

        /// <remarks/>
        MobilePhone,

        /// <remarks/>
        UtilityCompany,
    }
}
