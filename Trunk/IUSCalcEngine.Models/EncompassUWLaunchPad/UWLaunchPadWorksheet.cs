using CalcEngine.Models.AssetQualifierWorksheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.EncompassUWLaunchPad
{
    public class UWLaunchPadWorksheet
    {
        public decimal ProgramReservesRequired { get; set; }
        public string Source { get; set; }
        public decimal AdditionalReservesRequired { get; set; }
        public decimal TotalReservesRequired { get; set; }
        public Assets Assets { get; set; }
        public decimal FundsAvailableForReserves { get; set; }
        public decimal UsableCashOutProceed { get; set; }
        public decimal RequiredFundsToClose { get; set; }
        public decimal TotalReservesAfterClose { get; set; }
        public decimal TotalReservesWithCashoutProceed { get; set; }
        public decimal TotalNetQualifyingReserves { get; set; }
        public decimal TotalNetCashReserveAmount { get; set; }
        public decimal TotalReservesRequiredMonth { get; set; }
        public decimal VerifiedAssetsMonth { get; set; }
        public decimal UsableCashOutProceedMonth { get; set; }
        public decimal TotalReservesAfterCloseMonth { get; set; }
        public decimal TotalReservesWithCashoutProceedMonth { get; set; }
        public string Message { get; set; }
    }
}
