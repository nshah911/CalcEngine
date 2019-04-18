using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Liabilities
{
    public interface ILiabilityModelCommonCalcs
    {
        decimal Total(LiabilityType type);

        decimal Total(LiabilityType type, 
            LiabilityPayoffStatusIndicator liabilityPayoffStatusIndicator, 
            LiabilityExclusionIndicator liabilityExclusionIndicator);

        decimal Total(LiabilityType type, 
            LiabilityPayoffStatusIndicator liabilityPayoffStatusIndicator,
            LiabilityExclusionIndicator liabilityExclusionIndicator,
            int minRemainingTermMonths);

        decimal Total(LiabilityType type,
            LiabilityPayoffStatusIndicator liabilityPayoffStatusIndicator,
            LiabilityExclusionIndicator liabilityExclusionIndicator,
            IEnumerable<Liability> liabilities);
    }
}
