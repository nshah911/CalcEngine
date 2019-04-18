namespace CalcEngine.Models.Mismo
{
    public class Income
    {
        public IncomeType _IncomeType { get; set; }
        public decimal _MonthlyTotalAmount { get; set; }

    }
    public enum IncomeType : int
    {
        MilitaryBasePay = 1,
        MilitaryRationsAllowance = 2,
        MilitaryFlightPay = 3,
        MilitaryHazardPay = 4,
        MilitaryClothesAllowance = 5,
        MilitaryQuartersAllowance = 6,
        MilitaryPropPay = 7,
        MilitaryOverseasPay = 8,
        MilitaryCombatPay = 9,
        MilitaryVariableHousingAllowance = 10,
        AlimonyChildSupport = 11,
        NotesReceivableInstallment = 12,
        Pension = 13,
        SocialSecurity = 14,
        MortgageDifferential = 15,
        Trust = 16,
        Unemployment = 17,
        AutomobileExpenseAccount = 18,
        FosterCare = 19,
        VABenefitsNonEducational = 20,
        OtherIncome = 21,
        BasePay = 22,
        Overtime = 23,
        Bonuses = 24,
        Commissions = 25,
        DividendsInterest = 26,
        SubjectPropertyNetCashFlow = 27,
        NetRentalIncome = 28,
        FNMBoarderIncome = 29,
        FNMGovernmentMortgageCreditCertificate = 30,
        CapitalGains = 31,
        EmploymentRelatedAssets = 32,
        ForeignIncome = 33,
        RoyaltyPayment = 34,
        SeasonalIncome = 35,
        TemporaryLeave = 36,
        TipIncome = 37,
        AssetBasedIncome = 38,
        Base = 39,
        Bonus = 40,
        OtherTypesOfIncome = 41,
        FNMTrailingCoBorrower = 42,
        PublicAssistance = 43
    }
}