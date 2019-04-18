using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Assets;
using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Loan.NetQualifyingReserves
{
    public class NetQualifyingReservesAQCalcs : INetQualifyingReserves
    {
        private readonly IAssetCalcs _assetCalcs;
        private readonly MortgageTerms _mortgageTerms;
        private readonly decimal _subordinateLienAmount;
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly ILiabilityCalcs _liabilityCalcs;
        private readonly IProposedHousingMortgRelatedCalcs _proposedHousingMortgRelatedCalcs;
        private readonly IReoCalcs _reoCalcs;
        public NetQualifyingReservesAQCalcs(IAssetCalcs assetCalcs, MortgageTerms mortgageTerms, decimal subordinateLienAmount,
             ICashFromBorrower cashFromBorrower, ILiabilityCalcs liabilityCalcs, IReoCalcs reoCalcs,
             IProposedHousingMortgRelatedCalcs proposedHousingMortgRelatedCalcs)
        {
            _assetCalcs = assetCalcs;
            _mortgageTerms = mortgageTerms;
            _subordinateLienAmount = subordinateLienAmount;
            _cashFromBorrower = cashFromBorrower;
            _liabilityCalcs = liabilityCalcs;
            _proposedHousingMortgRelatedCalcs = proposedHousingMortgRelatedCalcs;
            _reoCalcs = reoCalcs;
        }
        public decimal TotalNetQualifyingReservesAmount()
        {
            decimal result = 0;
            result = this.TotalUsableAssets() - _mortgageTerms.BaseLoanAmount - _subordinateLienAmount - _cashFromBorrower.CashFromBorrower() -
                (TotalNetRental() * 60);
            return result;
        }

        private decimal TotalUsableAssets()
        {
            decimal total = 0;
            total = _assetCalcs.LiquidAssetsUsable()
                + _assetCalcs.MutualFundsUsable()
                + _assetCalcs.RetirementAccountsUsable()
                + _assetCalcs.StocksUsable();
            return total;
        }

        private decimal TotalNetRental()
        {
            decimal totalNetRental = 0;

            totalNetRental = _liabilityCalcs.TotalMonthlyLiabilites(10) +
                 _proposedHousingMortgRelatedCalcs.MortgageRelatedExpenses() +
                 _reoCalcs.PITIAforPrimaryResidence() +
                 _reoCalcs.PITIAforSecondHome() +
                 _reoCalcs.TotalNetRental();
            return totalNetRental;
        }
    }
}
