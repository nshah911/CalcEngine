using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.Reo
{
    public class ReoCalculation
    {
        private readonly IReoModelCommonCalcs _reoModelCommonCalcs = null;
        private readonly IReoCalcs _reoCalcs= null;
        private readonly IReoReserves _reoReserves = null;

        public ReoCalculation(IReoCalcs  reoCalcs,IReoModelCommonCalcs reoModelCommonCalcs, IReoReserves reoReserves)
        {
            _reoModelCommonCalcs = reoModelCommonCalcs;
            _reoCalcs = reoCalcs;
            _reoReserves = reoReserves;
        }

        public decimal PITIAforPrimaryResidence()
        {
            return _reoCalcs.PITIAforPrimaryResidence();
        }

        public decimal PITIAforSecondHome()
        {
            return _reoCalcs.PITIAforSecondHome();
        }

        public decimal TotalNetRental()
        {
            return _reoCalcs.TotalNetRental();
        }

        public decimal AddReserveForOtherFinancedReo()
        {
            return _reoReserves.AddReserveForOtherFinancedReo();
        }
    }

}
