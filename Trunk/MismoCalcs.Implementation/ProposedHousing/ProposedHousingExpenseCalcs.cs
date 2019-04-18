using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.ProposedHousing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.ProposedHousing
{
    public class ProposedHousingExpenseCalcs : IProposedHousingCalcs
    {
        private readonly IProposedHousingModelCommonCalcs _commonCalcs;

        public ProposedHousingExpenseCalcs(IProposedHousingModelCommonCalcs commonCalcs)
        {
            _commonCalcs = commonCalcs;
        }

        public decimal ProposedTotalHousingPayment()
        {
            decimal total = _commonCalcs.Total(ProposedHousingExpenseType.FirstMortgagePrincipalAndInterest)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherMortgageLoanPrincipalAndInterest)
                + _commonCalcs.Total(ProposedHousingExpenseType.HazardInsurance)
                + _commonCalcs.Total(ProposedHousingExpenseType.RealEstateTax)
                + _commonCalcs.Total(ProposedHousingExpenseType.MI)
                + _commonCalcs.Total(ProposedHousingExpenseType.HomeownersAssociationDuesAndCondominiumFees)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherHousingExpense);

            return total;
        }

        public decimal AllTypesTotalProposedHousingExpense()
        {
            decimal total = _commonCalcs.Total(ProposedHousingExpenseType.FirstMortgagePITI)
                + _commonCalcs.Total(ProposedHousingExpenseType.FirstMortgagePrincipalAndInterest)
                + _commonCalcs.Total(ProposedHousingExpenseType.GroundRent)
                + _commonCalcs.Total(ProposedHousingExpenseType.HazardInsurance)
                + _commonCalcs.Total(ProposedHousingExpenseType.HomeownersAssociationDuesAndCondominiumFees)
                + _commonCalcs.Total(ProposedHousingExpenseType.LeaseholdPayments)
                + _commonCalcs.Total(ProposedHousingExpenseType.MaintenanceAndMiscellaneous)
                + _commonCalcs.Total(ProposedHousingExpenseType.MI)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherHousingExpense)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherMortgageLoanPrincipalAndInterest)
                + _commonCalcs.Total(ProposedHousingExpenseType.OtherMortgageLoanPrincipalInterestTaxesAndInsurance)
                + _commonCalcs.Total(ProposedHousingExpenseType.RealEstateTax)
                + _commonCalcs.Total(ProposedHousingExpenseType.Rent)
                + _commonCalcs.Total(ProposedHousingExpenseType.Unknown)
                + _commonCalcs.Total(ProposedHousingExpenseType.Utilities);

           return total;
        }

        public decimal FirstMortgagePIAmount()
        {
            decimal total = _commonCalcs.Total(ProposedHousingExpenseType.FirstMortgagePrincipalAndInterest);
            return total;
        }
    }
}
