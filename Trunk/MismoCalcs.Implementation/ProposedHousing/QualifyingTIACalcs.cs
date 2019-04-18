using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.ProposedHousing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.ProposedHousing
{
    public class QualifyingTIACalcs : IProposedHousingQualifyingTIA
    {
        private readonly IProposedHousingModelCommonCalcs _commonCalcs;

        public QualifyingTIACalcs(IProposedHousingModelCommonCalcs commonCalcs)
        {
            _commonCalcs = commonCalcs;
        }
        public decimal QualifyingTIA()
        {
            decimal total = _commonCalcs.Total(ProposedHousingExpenseType.OtherMortgageLoanPrincipalAndInterest)
                + _commonCalcs.Total(ProposedHousingExpenseType.HazardInsurance)
                + _commonCalcs.Total(ProposedHousingExpenseType.RealEstateTax)
                + _commonCalcs.Total(ProposedHousingExpenseType.HomeownersAssociationDuesAndCondominiumFees)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherHousingExpense);
            return total;
        }
    }
}
