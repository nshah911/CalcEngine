using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.NetReservesAfterCashout
{
    public class NetReserveAfterCashOutBKPR : INetReserveAfterCashOut
    {
        private readonly ILTVCalc _ltvCalc;
        private readonly IBaseReserveCalc _baseReserveCalc;
        private readonly IReoReserves _reoReserves;

        public NetReserveAfterCashOutBKPR(
            ILTVCalc ltvCalc,
            IReoReserves reoReserves,
            IBaseReserveCalc baseReserveCalc)
        {
            _baseReserveCalc = baseReserveCalc;
            _reoReserves = reoReserves;
            _ltvCalc = ltvCalc;
        }
        public decimal NetReserveNeededAfterCashOutApplied()
        {
            decimal result = 0;
            result = _baseReserveCalc.BaseReserve() + _reoReserves.AddReserveForOtherFinancedReo();
            return result;
        }
    }
}
