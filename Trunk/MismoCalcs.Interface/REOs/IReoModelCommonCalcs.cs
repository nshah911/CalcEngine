using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.REOs
{
    public interface IReoModelCommonCalcs
    {
        decimal Total(ReoPropertySubjectIndicator subjectIndicator);

        decimal Total(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator);

        decimal TotalMaintainanceExpAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator);

        decimal TotalMaintainanceExpAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, DispositionStatusType dispositionType);

        decimal TotalRentalIncomeGrossAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator);

        decimal TotalRentalIncomeGrossAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicato, DispositionStatusType dispositionType);

        decimal Total(ReoPropertySubjectIndicator subjectIndicator,
            ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, DispositionStatusType disposeType);

        decimal Total(ReoPropertySubjectIndicator subjectIndicator,
            ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType);

        decimal Total(ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType,
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator);

        decimal Total(ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType,
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator, DispositionStatusType disposeType);

        decimal TotalNetRental(ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType,
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator, DispositionStatusType disposeType);

        decimal TotalNetRental(ReoProperty ReoProperty, ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType,
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator, DispositionStatusType disposeType);

    }
}
