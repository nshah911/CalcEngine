using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.AdditionalReservesRequired
{
    public class AdditionalReservesRequiredAQCalcs : IAdditionalReservesRequired
    {
        private readonly ILiabilityCalcs _liabilityCalcs;
        private readonly IProposedHousingMortgRelatedCalcs _proposedHousingMortgRelatedCalcs;
        private readonly IReoCalcs _reoCalcs;
        private readonly ICashFromBorrower _cashFromBorrower;

        public AdditionalReservesRequiredAQCalcs(ILiabilityCalcs liabilityCalcs, IProposedHousingMortgRelatedCalcs proposedHousingMortgRelatedCalcs,
            IReoCalcs reoCalcs, ICashFromBorrower cashFromBorrower)
        {
            _liabilityCalcs = liabilityCalcs;
            _proposedHousingMortgRelatedCalcs = proposedHousingMortgRelatedCalcs;
            _reoCalcs = reoCalcs;
            _cashFromBorrower = cashFromBorrower;
        }

        public decimal AdditionalReservesRequired()
        {
            decimal result = 0;
            result = GetMonthlyLiabilities() + (_cashFromBorrower.CashFromBorrower() > 0 ? _cashFromBorrower.CashFromBorrower() : 0);
            return result;
            
        }

        private decimal GetMonthlyLiabilities()
        {
            decimal total = 0;
            total = _liabilityCalcs.TotalMonthlyLiabilites(10)
                + _proposedHousingMortgRelatedCalcs.MortgageRelatedExpenses()
                + _reoCalcs.PITIAforPrimaryResidence()
                + _reoCalcs.PITIAforSecondHome()
                 + _reoCalcs.TotalNetRental();
            return total;
        }
    }
}
