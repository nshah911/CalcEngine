using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.ProgramReservesRequired
{
    public class ProgramReservesRequiredCalcs : IProgramReservesRequired
    {
        private readonly IReservesForNonSubjRetainedPropertiesCalc _reservesForNonSubjRetainedPropertiesCalc;
        private readonly IBaseReserveCalc _baseReserveCalc;        

        public ProgramReservesRequiredCalcs(IReservesForNonSubjRetainedPropertiesCalc reservesForNonSubjRetainedPropertiesCalc,
            IBaseReserveCalc baseReserveCalc)
        {
            _reservesForNonSubjRetainedPropertiesCalc = reservesForNonSubjRetainedPropertiesCalc;
            _baseReserveCalc = baseReserveCalc;
        }
        public decimal ProgramReservesRequired()
        {
            return (_baseReserveCalc.BaseReserve()) 
                + _reservesForNonSubjRetainedPropertiesCalc.ReservesForNonSubjRetainedProperties();

        }
    }
}
