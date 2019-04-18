using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Liabilities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MismoCalcs.Implementation.Liabilities
{
    public class LiabilityModelCommonCalcs : ILiabilityModelCommonCalcs
    {
        private readonly IEnumerable<Liability> _liabilities;

        public LiabilityModelCommonCalcs(IEnumerable<Liability> liabilities)
        {
            _liabilities = liabilities;
        }

        public decimal Total(LiabilityType type)
        {
            decimal total = 0;
            foreach (Liability Liab in _liabilities)
            {
                if (Liab._Type == type)
                    total += Liab._MonthlyPaymentAmount;
            }
            return total;
        }

        public decimal Total(LiabilityType type, 
            LiabilityPayoffStatusIndicator liabilityPayoffStatusIndicator, 
            LiabilityExclusionIndicator liabilityExclusionIndicator)
        {
            decimal total = 0;
            foreach(Liability liab in _liabilities)
            {
                if(liab._Type == type 
                    && liab._ExclusionIndicator == liabilityExclusionIndicator 
                    && liab._PayoffStatusIndicator == liabilityPayoffStatusIndicator)
                {
                    total += liab._MonthlyPaymentAmount;
                }
            }
            return total;
        }

        public decimal Total(LiabilityType type, 
            LiabilityPayoffStatusIndicator liabilityPayoffStatusIndicator, 
            LiabilityExclusionIndicator liabilityExclusionIndicator, 
            int minRemainingTermMonths)
        {
            decimal total = 0;
            foreach (Liability liab in _liabilities)
            {
                if (liab._Type == type
                    && liab._ExclusionIndicator == liabilityExclusionIndicator
                    && liab._PayoffStatusIndicator == liabilityPayoffStatusIndicator
                    && liab._RemainingTermMonths > minRemainingTermMonths)
                {
                    total += liab._MonthlyPaymentAmount;
                }
            }
            return total;
        }

        public decimal Total(LiabilityType type, 
            LiabilityPayoffStatusIndicator liabilityPayoffStatusIndicator, 
            LiabilityExclusionIndicator liabilityExclusionIndicator, 
            IEnumerable<Liability> liabilities)
        {
            decimal total = 0;
            if(liabilities != null)
            {
                foreach (Liability liab in liabilities)
                {
                    if(liab._Type == type
                        && liab._ExclusionIndicator == liabilityExclusionIndicator
                        && liab._PayoffStatusIndicator == liabilityPayoffStatusIndicator)
                    {
                        total += liab._MonthlyPaymentAmount;
                    }
                }
            }
            return total;
        }
    }
}
