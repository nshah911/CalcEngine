using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.REOs;
using CalcEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.REOs
{
    public class ReoCalcs : IReoCalcs
    {
        private readonly IReoModelCommonCalcs _reoModelCommonCalcs = null;

        public ReoCalcs(IReoModelCommonCalcs reoModelCommonCalcs)
        {
            _reoModelCommonCalcs = reoModelCommonCalcs;
        }

        public decimal SubTotalCurrentHousingPayment()
        {
            decimal total = _reoModelCommonCalcs.Total(ReoPropertySubjectIndicator.Y);
            return total;
        }

        public decimal PITIAforPrimaryResidence()
        {
            decimal total = _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                 +_reoModelCommonCalcs.TotalMaintainanceExpAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y);

            return total;
        }

        public decimal PITIAforSecondHome()
        {
            decimal total = _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 +_reoModelCommonCalcs.TotalMaintainanceExpAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence);
            return total;
        }

        public decimal TotalNetRental()
        {
            decimal total = 0;
            decimal totalNetRental = _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental);

            total = _reoModelCommonCalcs.TotalRentalIncomeGrossAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, DispositionStatusType.RetainForRental)
                - (totalNetRental + _reoModelCommonCalcs.TotalMaintainanceExpAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N,DispositionStatusType.RetainForRental));
                
            if(total < 0)
            {
                total = total * -1;
            }
            else if(total > 0)
            {
                total = 0;
            }
            else
            { total = 0;}
            return total;
        }

        /// <summary>
        /// Real Estate Rental income
        /// where subject property is no and current resicence is y or no 
        /// and disposition type is rental or second home
        /// </summary>
        /// <returns></returns>

        public decimal RealEstateRental()
        {
            decimal total = 0;
            decimal totalNetRental = _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)

                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)
                 + _reoModelCommonCalcs.TotalNetRental(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForRental)


                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.HELOC, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)

                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence)
                 + _reoModelCommonCalcs.Total(ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.N, LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, DispositionStatusType.RetainForPrimaryOrSecondaryResidence);

            total = (_reoModelCommonCalcs.TotalRentalIncomeGrossAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, DispositionStatusType.RetainForRental)
                + _reoModelCommonCalcs.TotalRentalIncomeGrossAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, DispositionStatusType.RetainForPrimaryOrSecondaryResidence))
                - (totalNetRental + _reoModelCommonCalcs.TotalMaintainanceExpAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, DispositionStatusType.RetainForRental)
                    + _reoModelCommonCalcs.TotalMaintainanceExpAmount(CalcEngine.Models.Mismo.ReoPropertySubjectIndicator.N, CalcEngine.Models.Mismo.ReoPropertyCurrentResidenceIndicator.Y, DispositionStatusType.RetainForPrimaryOrSecondaryResidence));

            return total;
        }
    }
}
