using MismoCalcs.Interface.ProposedHousing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.ProposedHousing
{
    public class ProposedHousingExpenseCalculation
    {
        private readonly IProposedHousingCalcs _proposedHousingCalcs;
        private readonly IProposedHousingMortgRelatedCalcs _proposedHousingMortgRelatedCalcs;
        private readonly IProposedHousingQualifyingTIA _proposedHousingQualifyingTIA;

        public ProposedHousingExpenseCalculation(
            IProposedHousingCalcs proposedHousingCalcs,
            IProposedHousingQualifyingTIA proposedHousingQualifyingTIA,
            IProposedHousingMortgRelatedCalcs proposedHousingMortgRelatedCalcs = null)
        {
            _proposedHousingCalcs = proposedHousingCalcs;
            _proposedHousingMortgRelatedCalcs = proposedHousingMortgRelatedCalcs;
            _proposedHousingQualifyingTIA = proposedHousingQualifyingTIA;
        }

        public decimal AllTypesTotalProposedHousingExpense()
        {
            return _proposedHousingCalcs.AllTypesTotalProposedHousingExpense();
        }

        public decimal MortgageRelatedExpenses()
        {
            return _proposedHousingMortgRelatedCalcs.MortgageRelatedExpenses();
        }
        public decimal ProposedHousingQualifyingTIA()
        {
            return _proposedHousingQualifyingTIA.QualifyingTIA();
        }

        public decimal FirstMortgagePIAmount()
        {
            return _proposedHousingCalcs.FirstMortgagePIAmount();
        }

        public decimal ProposedTotalHousingPayment()
        {
            return _proposedHousingCalcs.ProposedTotalHousingPayment();
        }
    }
}
