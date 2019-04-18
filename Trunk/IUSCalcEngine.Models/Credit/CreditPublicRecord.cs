using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class CreditPublicRecord
    {
        public string CreditPublicRecordID { get; set; }

        public string BorrowerID { get; set; }

        public string CreditFileID { get; set; }

        public string CreditTradeReferenceID { get; set; }

        public CreditPublicAccountOwnershipType _AccountOwnershipType { get; set; }

        public CreditPublicRecordType _Type { get; set; }

        public DateTime _FiledDate { get; set; }

        public DateTime _DispositionDate { get; set; }
    }

    public enum CreditPublicAccountOwnershipType
    {
        /// <remarks/>
        Individual,

        /// <remarks/>
        Joint,

        /// <remarks/>
        Terminated,
    }

    public enum CreditPublicRecordType
    {

        /// <remarks/>
        Annulment,

        /// <remarks/>
        Attachment,

        /// <remarks/>
        BankruptcyChapter11,

        /// <remarks/>
        BankruptcyChapter12,

        /// <remarks/>
        BankruptcyChapter13,

        /// <remarks/>
        BankruptcyChapter7,

        /// <remarks/>
        BankruptcyChapter7Involuntary,

        /// <remarks/>
        BankruptcyChapter7Voluntary,

        /// <remarks/>
        BankruptcyTypeUnknown,

        /// <remarks/>
        Collection,

        /// <remarks/>
        CustodyAgreement,

        /// <remarks/>
        DivorceDecree,

        /// <remarks/>
        FicticiousName,

        /// <remarks/>
        FinancialCounseling,

        /// <remarks/>
        FinancingStatement,

        /// <remarks/>
        ForcibleDetainer,

        /// <remarks/>
        Foreclosure,

        /// <remarks/>
        Garnishment,

        /// <remarks/>
        Judgment,

        /// <remarks/>
        LawSuit,

        /// <remarks/>
        Lien,

        /// <remarks/>
        NonResponsibility,

        /// <remarks/>
        NoticeOfDefault,

        /// <remarks/>
        Other,

        /// <remarks/>
        PublicSale,

        /// <remarks/>
        RealEstateRecording,

        /// <remarks/>
        Repossession,

        /// <remarks/>
        SupportDebt,

        /// <remarks/>
        TaxLienCity,

        /// <remarks/>
        TaxLienCounty,

        /// <remarks/>
        TaxLienFederal,

        /// <remarks/>
        TaxLienOther,

        /// <remarks/>
        TaxLienState,

        /// <remarks/>
        Trusteeship,

        /// <remarks/>
        Unknown,

        /// <remarks/>
        UnlawfulDetainer,

        /// <remarks/>
        ChildSupport,

        /// <remarks/>
        FamilySupport,

        /// <remarks/>
        MechanicsLien,

        /// <remarks/>
        MedicalLien,

        /// <remarks/>
        SpouseSupport,
    }
}
