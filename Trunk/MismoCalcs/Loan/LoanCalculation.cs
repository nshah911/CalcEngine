using CalcEngine.Models.Mismo;
using MismoCalcs.Implementation.Loan;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.Loan
{
    public class LoanCalculation
    {
        private readonly IPresentTotalHousingExpenseCalc _presentTotalHousingExpenseCalc;
        private readonly ILTVCalc _ltvCalc;
        private readonly IBaseReserveCalc _baseReserveCalc;
        private readonly IReservesForNonSubjRetainedPropertiesCalc _reservesForNonSubjRetainedPropertiesCalc;
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly INetReserveAfterCashOut _netReserveAfterCashOut;
        private readonly IAdditionalReservesRequired _additionalReservesRequired;
        private readonly IProgramReservesRequired _programReservesRequired;
        private readonly IQualifyingPIPayment _qualifyingPIPayment;
        private readonly ICashoutProceed _cashoutProceed;
        private readonly INetQualifyingReserves _netQualifyingReserves;

        public LoanCalculation(
            IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc,
            IBaseReserveCalc baseReserveCalc,
            ILTVCalc ltvCalc,
            ICashFromBorrower cashFromBorrower,
            IReservesForNonSubjRetainedPropertiesCalc reservesForNonSubjRetainedPropertiesCalc,
            IProgramReservesRequired programReservesRequired,
            IAdditionalReservesRequired additionalReservesRequired,
            INetReserveAfterCashOut netReserveAfterCashOut,
            IQualifyingPIPayment qualifyingPIPayment,
            ICashoutProceed cashoutProceed,
            INetQualifyingReserves netQualifyingReserves)
        {
            _presentTotalHousingExpenseCalc = presentTotalHousingExpenseCalc;
            _ltvCalc = ltvCalc;
            _baseReserveCalc = baseReserveCalc;
            _cashFromBorrower = cashFromBorrower;
            _additionalReservesRequired = additionalReservesRequired;
            _programReservesRequired = programReservesRequired;
            _reservesForNonSubjRetainedPropertiesCalc = reservesForNonSubjRetainedPropertiesCalc;
            _netReserveAfterCashOut = netReserveAfterCashOut;
            _qualifyingPIPayment = qualifyingPIPayment;
            _cashoutProceed = cashoutProceed;
            _netQualifyingReserves = netQualifyingReserves;
        }

        public decimal BaseReserve()
        {
            return _baseReserveCalc.BaseReserve();
        }

        public decimal LTV()
        {
            return _ltvCalc.LTV();
        }

        public decimal ReservesForNonSubjRetainedProperties()
        {
            return _reservesForNonSubjRetainedPropertiesCalc.ReservesForNonSubjRetainedProperties();
        }

        public decimal CashFromBorrower()
        {
            return _cashFromBorrower.CashFromBorrower();
        }

        public decimal TotalCredit()
        {
            return _cashFromBorrower.TotalCredit();
        }

        public decimal TotalCost()
        {
            return _cashFromBorrower.TotalCost();
        }

        public decimal NetReservesAfterCashOut()
        {
            return _netReserveAfterCashOut.NetReserveNeededAfterCashOutApplied();
        }

        public decimal CashoutProceed()
        {
            return _cashoutProceed.CashoutProceedAmount();
        }

        public decimal NetQualifyingReserves()
        {
            return _netQualifyingReserves.TotalNetQualifyingReservesAmount();
        }

        public decimal ProgramReservesRequired()
        {
            return _programReservesRequired.ProgramReservesRequired();
        }

        public decimal AdditionalReservesRequired()
        {
            return _additionalReservesRequired.AdditionalReservesRequired();
        }

        public decimal QualifyingPIAmount()
        {
            return _qualifyingPIPayment.QualifyingPIAmount();
        }
    }
}
