using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.ProposedHousing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.ProposedHousing
{
    public class AssetQualifierProposedHousingCalcs : IProposedHousingMortgRelatedCalcs
    {
        private readonly IProposedHousingModelCommonCalcs _commonCalcs;

        public AssetQualifierProposedHousingCalcs(IProposedHousingModelCommonCalcs commonCalcs)
        {
            _commonCalcs = commonCalcs;
        }

        public decimal MortgageRelatedExpenses()
        {
            decimal total = _commonCalcs.Total(ProposedHousingExpenseType.OtherMortgageLoanPrincipalAndInterest)
                + _commonCalcs.Total(ProposedHousingExpenseType.HazardInsurance)
                + _commonCalcs.Total(ProposedHousingExpenseType.RealEstateTax)
                + _commonCalcs.Total(ProposedHousingExpenseType.MI)
                + _commonCalcs.Total(ProposedHousingExpenseType.HomeownersAssociationDuesAndCondominiumFees)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherHousingExpense);

            return total;
        }
    }
}
