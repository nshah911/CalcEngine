using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CalcEngine.Models.Mismo_v231
{
    [DataContract, XmlRootAttribute(ElementName = "LOAN_APPLICATION")]
    public class LoanApplicationDTO
    {
        public LoanApplicationDTO()
        {
            ADDITIONAL_CASE_DATA = new AdditionalCaseDataDTO();
            ASSET = new List<AssetDTO>();
            BORROWER = new List<BorrowerDTO>();
            DOWN_PAYMENT = new List<DownPaymentDTO>();
            LIABILITY = new List<LiabilityDTO>();
            LOAN_DETAIL = new LoanDetailDTO();
            LOAN_PRODUCT_DATA = new LoanProductDataDTO();
            LOAN_PURPOSE = new LoanPurposeDTO();
            MORTGAGE_TERMS = new MortgageTermsDTO();
            PROPERTY = new PropertyDTO();
            PROPOSED_HOUSING_EXPENSE = new List<HousingExpenseDTO>();
            REO_PROPERTY = new List<ReoPropertyDTO>();
            TRANSACTION_DETAIL = new TransactionDetailDTO();
            INTERVIEWER_INFORMATION = new InterviewerInfoDTO();
        }

        [DataMember]
        public AdditionalCaseDataDTO ADDITIONAL_CASE_DATA { get; set; }
        [DataMember, XmlElement]
        public List<AssetDTO> ASSET { get; set; }
        [DataMember, XmlElement]
        public List<DownPaymentDTO> DOWN_PAYMENT { get; set; }
        [DataMember, XmlElement]
        public List<LiabilityDTO> LIABILITY { get; set; }
        [DataMember]
        public LoanDetailDTO LOAN_DETAIL { get; set; }
        [DataMember]
        public LoanProductDataDTO LOAN_PRODUCT_DATA { get; set; }
        [DataMember]
        public LoanPurposeDTO LOAN_PURPOSE { get; set; }
        [DataMember]
        public MortgageTermsDTO MORTGAGE_TERMS { get; set; }
        [DataMember]
        public InterviewerInfoDTO INTERVIEWER_INFORMATION { get; set; }
        [DataMember]
        public PropertyDTO PROPERTY { get; set; }
        [DataMember, XmlElement("PROPOSED_HOUSING_EXPENSE")]
        public List<HousingExpenseDTO> PROPOSED_HOUSING_EXPENSE { get; set; }
        [DataMember, XmlElement]
        public List<ReoPropertyDTO> REO_PROPERTY { get; set; }
        [DataMember]
        public TransactionDetailDTO TRANSACTION_DETAIL { get; set; }
        [DataMember, XmlElement]
        public List<BorrowerDTO> BORROWER { get; set; }
    }

    [DataContract]
    public class AdditionalCaseDataDTO
    {
        public AdditionalCaseDataDTO()
        {
            TRANSMITTAL_DATA = new TransmittalDataDTO();
        }

        [DataMember]
        public TransmittalDataDTO TRANSMITTAL_DATA { get; set; }
        [DataMember, XmlAttribute]
        public string FNMCommunitySecondsIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string FNMCommunitySecondsRepaymentStructure { get; set; }
    }

    [DataContract]
    public class ARMDTO
    {
        [DataMember, XmlAttribute]
        public decimal _IndexCurrentValuePercent { get; set; }
        [DataMember, XmlAttribute]
        public decimal _IndexMarginPercent { get; set; }
        [DataMember, XmlAttribute]
        public decimal _QualifyingRatePercent { get; set; }
    }

    [DataContract]
    public class AssetDTO
    {
        [DataMember, XmlAttribute]
        public string BorrowerID { get; set; }
        [DataMember, XmlAttribute]
        public decimal _CashOrMarketValueAmount { get; set; }
        [DataMember, XmlAttribute]
        public string _Type { get; set; }
        [DataMember, XmlAttribute]
        public string OtherAssetTypeDescription { get; set; }
        [DataMember, XmlAttribute]
        public string _HolderName { get; set; }
        [DataMember, XmlAttribute]
        public string _AccountIdentifier { get; set; }
    }

    [DataContract]
    public class BorrowerDTO
    {
        public BorrowerDTO()
        {
            _RESIDENCE = new List<ResidenceDTO>();
            CURRENT_INCOME = new List<CurrentIncomeDTO>();
            DECLARATION = new List<DeclarationDTO>();
            EMPLOYER = new List<EmployerDTO>();
            GOVERNMENT_MONITORING = new GovernmentMonitoringDTO();
            PRESENT_HOUSING_EXPENSE = new List<HousingExpenseDTO>();
            SUMMARY = new List<BorrowerSummaryDTO>();
        }

        [DataMember, XmlAttribute]
        public string BorrowerID { get; set; }
        [DataMember, XmlAttribute]
        public string JointAssetBorrowerID { get; set; }
        [DataMember, XmlAttribute]
        public string _FirstName { get; set; }
        [DataMember, XmlAttribute]
        public string _LastName { get; set; }
        [DataMember, XmlAttribute]
        public string _NameSuffix { get; set; }
        [DataMember, XmlAttribute]
        public string _BirthDate { get; set; }
        [DataMember, XmlAttribute]
        public string _PrintPositionType { get; set; }
        [DataMember, XmlAttribute]
        public string _SSN { get; set; }
        [DataMember, XmlElement]
        public List<ResidenceDTO> _RESIDENCE { get; set; }
        [DataMember, XmlElement]
        public List<CurrentIncomeDTO> CURRENT_INCOME { get; set; }
        [DataMember, XmlElement]
        public List<DeclarationDTO> DECLARATION { get; set; }
        [DataMember, XmlElement]
        public List<EmployerDTO> EMPLOYER { get; set; }
        [DataMember]
        public GovernmentMonitoringDTO GOVERNMENT_MONITORING { get; set; }
        [DataMember, XmlElement]
        public List<HousingExpenseDTO> PRESENT_HOUSING_EXPENSE { get; set; }
        [DataMember, XmlElement]
        public List<BorrowerSummaryDTO> SUMMARY { get; set; }
    }

    [DataContract]
    public class BorrowerSummaryDTO
    {
        [DataMember, XmlAttribute]
        public string _Amount { get; set; }
        [DataMember, XmlAttribute]
        public string _AmountType { get; set; }
    }

    [DataContract]
    public class ConstructionRefinanceDataDTO
    {
        [DataMember, XmlAttribute]
        public string GSERefinancePurposeType { get; set; }
    }

    [DataContract]
    public class CurrentIncomeDTO
    {
        [DataMember, XmlAttribute]
        public string IncomeType { get; set; }
        [DataMember, XmlAttribute]
        public decimal _MonthlyTotalAmount { get; set; }
    }

    [DataContract]
    public class DeclarationDTO
    {
        public DeclarationDTO()
        {
            _EXPLANATION = new List<ExplanationDTO>();
        }

        [DataMember, XmlAttribute]
        public string BankruptcyIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string CitizenshipResidencyType { get; set; }
        [DataMember, XmlAttribute]
        public string HomeownerPastThreeYearsType { get; set; }
        [DataMember, XmlAttribute]
        public string IntentToOccupyType { get; set; }
        [DataMember, XmlAttribute]
        public string PriorPropertyTitleType { get; set; }
        [DataMember, XmlAttribute]
        public string PriorPropertyUsageType { get; set; }
        [DataMember, XmlAttribute]
        public string PropertyForeclosedPastSevenYearsIndicator { get; set; }
        [DataMember, XmlElement]
        public List<ExplanationDTO> _EXPLANATION { get; set; }
    }

    [DataContract]
    public class DownPaymentDTO
    {
        [DataMember, XmlAttribute]
        public string _Type { get; set; }
    }

    [DataContract]
    public class EmployerDTO
    {
        [DataMember, XmlAttribute]
        public int CurrentEmploymentMonthsOnJob { get; set; }
        [DataMember, XmlAttribute]
        public int CurrentEmploymentYearsOnJob { get; set; }
        [DataMember, XmlAttribute]
        public string EmploymentBorrowerSelfEmployedIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string EmploymentCurrentIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string EmploymentPrimaryIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string PreviousEmploymentStartDate { get; set; }
    }

    [DataContract]
    public class ExplanationDTO
    {
        [DataMember, XmlAttribute]
        public string _Description { get; set; }
        [DataMember, XmlAttribute]
        public string _Type { get; set; }
    }

    [DataContract]
    public class GovernmentMonitoringDTO
    {
    }

    [DataContract]
    public class GovernmentReportingDTO
    {
    }

    [DataContract]
    public class HousingExpenseDTO
    {
        [DataMember, XmlAttribute]
        public string HousingExpenseType { get; set; }
        [DataMember, XmlAttribute]
        public decimal _PaymentAmount { get; set; }
    }

    [DataContract]
    public class LiabilityDTO
    {
        [DataMember, XmlAttribute]
        public string _ID { get; set; }
        [DataMember, XmlAttribute]
        public string BorrowerID { get; set; }
        [DataMember, XmlAttribute]
        public string _AccountIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string REO_ID { get; set; }
        [DataMember, XmlAttribute]
        public string _ExclusionIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string _HolderName { get; set; }
        [DataMember, XmlAttribute]
        public string _PayoffStatusIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string _RemainingTermMonths { get; set; }
        [DataMember, XmlAttribute]
        public decimal _MonthlyPaymentAmount { get; set; }
        [DataMember, XmlAttribute]
        public string _Type { get; set; }
        [DataMember, XmlAttribute]
        public decimal _UnpaidBalanceAmount { get; set; }
        [DataMember, XmlAttribute]
        public string SubjectLoanResubordinationIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string FNMSubjectPropertyIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string FNMRentalPropertyIndicator { get; set; }
    }

    [DataContract]
    public class LoanDetailDTO
    {
        [DataMember, XmlAttribute]
        public int TotalMortgagedPropertiesCount { get; set; }
    }

    [DataContract]
    public class LoanFeaturesDTO
    {
        [DataMember, XmlAttribute]
        public string BalloonIndicator { get; set; }
        [DataMember, XmlAttribute]
        public int BalloonLoanMaturityTermMonths { get; set; }
        [DataMember, XmlAttribute]
        public string FNMProductPlanIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string GSEPropertyType { get; set; }
        [DataMember, XmlAttribute]
        public string LienPriorityType { get; set; }
        [DataMember, XmlAttribute]
        public decimal NegativeAmortizationLimitPercent { get; set; }
        [DataMember, XmlAttribute]
        public string PaymentFrequencyType { get; set; }
        [DataMember, XmlAttribute]
        public string ProductDescription { get; set; }
    }

    [DataContract]
    public class LoanProductDataDTO
    {
        public LoanProductDataDTO()
        {
            ARM = new List<ARMDTO>();
            LOAN_FEATURES = new LoanFeaturesDTO();
            RATE_ADJUSTMENT = new List<RateAdjustmentDTO>();
        }

        [DataMember, XmlElement]
        public List<ARMDTO> ARM { get; set; }
        [DataMember]
        public LoanFeaturesDTO LOAN_FEATURES { get; set; }
        [DataMember, XmlElement]
        public List<RateAdjustmentDTO> RATE_ADJUSTMENT { get; set; }
    }

    [DataContract]
    public class LoanPurposeDTO
    {
        public LoanPurposeDTO()
        {
            CONSTRUCTION_REFINANCE_DATA = new List<ConstructionRefinanceDataDTO>();
        }

        [DataMember, XmlAttribute]
        public string _Type { get; set; }
        [DataMember, XmlAttribute]
        public string PropertyRightsType { get; set; }
        [DataMember, XmlAttribute]
        public string PropertyUsageType { get; set; }
        [DataMember, XmlElement]
        public List<ConstructionRefinanceDataDTO> CONSTRUCTION_REFINANCE_DATA { get; set; }
    }

    [DataContract]
    public class MortgageTermsDTO
    {
        [DataMember, XmlAttribute]
        public decimal BaseLoanAmount { get; set; }
        [DataMember, XmlAttribute]
        public int LoanAmortizationTermMonths { get; set; }
        [DataMember, XmlAttribute]
        public string LoanAmortizationType { get; set; }
        [DataMember, XmlAttribute]
        public string MortgageType { get; set; }
        [DataMember, XmlAttribute]
        public decimal RequestedInterestRatePercent { get; set; }
    }

    [DataContract]
    public class ParsedStreetAddressDTO
    {
        [DataMember, XmlAttribute]
        public string _ApartmentOrUnit { get; set; }
        [DataMember, XmlAttribute]
        public string _HouseNumber { get; set; }
        [DataMember, XmlAttribute]
        public string _StreetName { get; set; }
        [DataMember, XmlAttribute]
        public string AppraisalIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string _Name { get; set; }
        [DataMember, XmlAttribute]
        public string _LicenseIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string _LicenseState { get; set; }
        [DataMember, XmlAttribute]
        public string _SupervisoryAppraiserLicenseNumber { get; set; }
    }

    [DataContract]
    public class PropertyDTO
    {
        public PropertyDTO()
        {
            PARSEDSTREETADDRESS = new List<ParsedStreetAddressDTO>();
        }

        [DataMember, XmlAttribute]
        public string _StreetAddress { get; set; }
        [DataMember, XmlAttribute]
        public string _City { get; set; }
        [DataMember, XmlAttribute]
        public string _State { get; set; }
        [DataMember, XmlAttribute]
        public string _County { get; set; }
        [DataMember, XmlAttribute]
        public string _PostalCode { get; set; }
        [DataMember, XmlAttribute]
        public int _FinancedNumberOfUnits { get; set; }
        [DataMember, XmlAttribute]
        public string _AcquiredDate { get; set; }
        [DataMember, XmlElement]
        public List<ParsedStreetAddressDTO> PARSEDSTREETADDRESS { get; set; }
    }

    [DataContract]
    public class InterviewerInfoDTO
    {
        [DataMember, XmlAttribute]
        public string InterviewerApplicationSignedDate { get; set; }

    }

    [DataContract]
    public class PurchaseCreditDTO
    {
        [DataMember, XmlAttribute]
        public decimal _Amount { get; set; }
        [DataMember, XmlAttribute]
        public string _SourceType { get; set; }
        [DataMember, XmlAttribute]
        public string _Type { get; set; }
    }

    [DataContract]
    public class RateAdjustmentDTO
    {
        [DataMember, XmlAttribute]
        public decimal FirstRateAdjustmentMonths { get; set; }
        [DataMember, XmlAttribute]
        public decimal _DurationMonths { get; set; }
        [DataMember, XmlAttribute]
        public decimal _InitialCapPercent { get; set; }
        [DataMember, XmlAttribute]
        public decimal _SubsequentCapPercent { get; set; }
        [DataMember, XmlAttribute]
        public decimal SubsequentRateAdjustmentMonths { get; set; }
    }

    [DataContract]
    public class ReoPropertyDTO
    {
        [DataMember, XmlAttribute]
        public string REO_ID { get; set; }
        [DataMember, XmlAttribute]
        public string BorrowerID { get; set; }
        [DataMember, XmlAttribute]
        public string _CurrentResidenceIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string _DispositionStatusType { get; set; }
        [DataMember, XmlAttribute]
        public decimal _LienInstallmentAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal _LienUPBAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal _MaintenanceExpenseAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal _MarketValueAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal _RentalIncomeNetAmount { get; set; }
        [DataMember, XmlAttribute]
        public string _SubjectIndicator { get; set; }
        [DataMember, XmlAttribute]
        public string LiabilityID { get; set; }
        [DataMember, XmlAttribute]
        public string _StreetAddress { get; set; }
        [DataMember, XmlAttribute]
        public string _City { get; set; }
        [DataMember, XmlAttribute]
        public string _State { get; set; }
        [DataMember, XmlAttribute]
        public string _PostalCode { get; set; }
    }

    [DataContract]
    public class ResidenceDTO
    {
        [DataMember, XmlAttribute]
        public string _StreetAddress { get; set; }
        [DataMember, XmlAttribute]
        public string _City { get; set; }
        [DataMember, XmlAttribute]
        public string _State { get; set; }
        [DataMember, XmlAttribute]
        public string _PostalCode { get; set; }
        [DataMember, XmlAttribute]
        public string BorrowerResidencyType { get; set; }
    }

    [DataContract]
    public class TransactionDetailDTO
    {
        public TransactionDetailDTO()
        {
            PURCHASE_CREDIT = new List<PurchaseCreditDTO>();
        }

        [DataMember, XmlAttribute]
        public decimal AlterationsImprovementsAndRepairsAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal BorrowerPaidDiscountPointsTotalAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal EstimatedClosingCostsAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal MIAndFundingFeeFinancedAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal MIAndFundingFeeTotalAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal PrepaidItemsEstimatedAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal PurchasePriceAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal RefinanceIncludingDebtsToBePaidOffAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal SellerPaidClosingCostsAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal SubordinateLienAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal FNMCostOfLandAcquiredSeparatelyAmount { get; set; }
        [DataMember, XmlElement]
        public List<PurchaseCreditDTO> PURCHASE_CREDIT { get; set; }
    }

    [DataContract]
    public class TransmittalDataDTO
    {
        [DataMember, XmlAttribute]
        public decimal BuydownRatePercent { get; set; }
        [DataMember, XmlAttribute]
        public string CurrentFirstMortgageHolderType { get; set; }
        [DataMember, XmlAttribute]
        public decimal PropertyAppraisedValueAmount { get; set; }
        [DataMember, XmlAttribute]
        public decimal PropertyEstimatedValueAmount { get; set; }
        [DataMember, XmlAttribute, StringLength(30)]
        public string InvestorLoanIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string InvestorInstitutionIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string FIPSCodeIdentifier { get; set; }
        [DataMember, XmlAttribute]
        public string LoanOriginatorID { get; set; }
        [DataMember, XmlAttribute]
        public string LoanOriginationCompanyID { get; set; }
    }
}
