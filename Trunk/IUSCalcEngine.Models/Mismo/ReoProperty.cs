using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class ReoProperty
    {
        public string Reo_Id { get; set; }

        public ReoPropertySubjectIndicator _SubjectIndicator { get; set; }

        public ReoPropertyCurrentResidenceIndicator _CurrentResidenceIndicator { get; set; }

        public DispositionStatusType _DispositionStatusType { get; set; }

        public string LiabilityID { get; set; }

        public decimal _LienInstallmentAmount { get; set; }

        public decimal _RentalIncomeGrossAmount { get; set; }

        public decimal _LienUPBAmount { get; set; }

        public decimal _MaintenanceExpenseAmount { get; set; }

        public List<Liability> LinkedLiabilities { get; set; }

        public DateTime AcquireDate { get; set; }

        public decimal PercentOfParticipation { get; set; }

        public decimal PercentOfRental { get; set; }
    }
    public enum ReoPropertySubjectIndicator
    {
        /// <summary>
        /// Default to unknown
        /// </summary>
        Unknown,
        
        N,

        /// <remarks/>
        Y,
    }
    public enum ReoPropertyCurrentResidenceIndicator
    {
        /// <summary>
        /// Default to unknown
        /// </summary>
        Unknown,

        N,

        /// <remarks/>
        Y,
    }
    public enum DispositionStatusType
    {

        /// <remarks/>
        PendingSale,

        /// <remarks/>
        RetainForRental,

        /// <remarks/>
        RetainForPrimaryOrSecondaryResidence,

        /// <remarks/>
        Sold,
    }
}
