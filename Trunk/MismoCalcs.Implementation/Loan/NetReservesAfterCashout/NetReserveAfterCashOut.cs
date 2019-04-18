using CalcEngine.Models;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.NetReservesAfterCashout
{
    public class NetReserveAfterCashOut : INetReserveAfterCashOut
    {
        private readonly IUSLoan _IUSLoan;
        private readonly ILTVCalc _ltvCalc;
        private readonly IBaseReserveCalc _baseReserveCalc;
        private readonly IReoReserves _reoReserves;
        private readonly ICashFromBorrower _cashFromBorrower;

        public NetReserveAfterCashOut(IUSLoan iUSLoan, IReoReserves reoReserves, ICashFromBorrower cashFromBorrowerCalcs, IBaseReserveCalc baseReserveCalc)
        {
            _IUSLoan = iUSLoan;
            _reoReserves = reoReserves;
            _cashFromBorrower = cashFromBorrowerCalcs;
            _baseReserveCalc = baseReserveCalc;
        }

        public decimal NetReserveNeededAfterCashOutApplied()
        {
            decimal result = 0;
            if (_IUSLoan.AllowReserve)
            {
                if(_cashFromBorrower.CashFromBorrower() <= 0 && _ltvCalc.LTV() <= 80)
                {
                    result = _baseReserveCalc.BaseReserve() + _reoReserves.AddReserveForOtherFinancedReo() - Math.Abs(_cashFromBorrower.CashFromBorrower());
                }
                if(_cashFromBorrower.CashFromBorrower() <= 0 && _ltvCalc.LTV() > 80)
                {
                    result = _baseReserveCalc.BaseReserve() + _reoReserves.AddReserveForOtherFinancedReo();
                }
            }
            return result;
        }
    }
}
