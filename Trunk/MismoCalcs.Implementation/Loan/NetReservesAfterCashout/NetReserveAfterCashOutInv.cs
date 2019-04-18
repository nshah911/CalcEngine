using CalcEngine.Models;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.NetReservesAfterCashout
{
    public class NetReserveAfterCashOutInv : INetReserveAfterCashOut
    {
        private readonly IUSLoan _IUSLoan;
        private readonly ILTVCalc _ltvCalc;
        private readonly IBaseReserveCalc _baseReserveCalc;
        private readonly IReoReserves _reoReserves;
        private readonly ICashFromBorrower _cashFromBorrower;

        public NetReserveAfterCashOutInv(IUSLoan iUSLoan,
            ILTVCalc ltvCalc,
            IReoReserves reoReserves,
            ICashFromBorrower cashFromBorrower,
            IBaseReserveCalc baseReserveCalc)
        {
            _IUSLoan = iUSLoan;
            _baseReserveCalc = baseReserveCalc;
            _reoReserves = reoReserves;
            _cashFromBorrower = cashFromBorrower;
            _ltvCalc = ltvCalc;
        }

        public decimal NetReserveNeededAfterCashOutApplied()
        {
            decimal result = 0;
            result = _baseReserveCalc.BaseReserve() + _reoReserves.AddReserveForOtherFinancedReo();
            if (_cashFromBorrower.CashFromBorrower() <= 0)
            {
                result -= Math.Abs(_cashFromBorrower.CashFromBorrower());
            }
            return result;
        }
    }
}
