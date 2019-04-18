using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.AdditionalReservesRequired
{
    public class AdditionalReservesRequiredCalcs : IAdditionalReservesRequired
    {
        private readonly IReservesForNonSubjRetainedPropertiesCalc _reservesForNonSubjRetainedPropertiesCalc;
        private readonly IBaseReserveCalc _baseReserveCalc;

        public AdditionalReservesRequiredCalcs(IReservesForNonSubjRetainedPropertiesCalc reservesForNonSubjRetainedPropertiesCalc,
           IBaseReserveCalc baseReserveCalc)
        {
            _reservesForNonSubjRetainedPropertiesCalc = reservesForNonSubjRetainedPropertiesCalc;
            _baseReserveCalc = baseReserveCalc;
        }

        public decimal AdditionalReservesRequired()
        {
            return 0;
        }
    }
}
