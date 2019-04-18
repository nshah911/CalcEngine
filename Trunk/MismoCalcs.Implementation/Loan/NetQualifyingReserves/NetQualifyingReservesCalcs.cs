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
    public class NetQualifyingReservesCalcs : INetQualifyingReserves
    {
        private readonly IAssetCalcs _assetCalcs;
        private readonly MortgageTerms _mortgageTerms;
        private readonly decimal _subordinateLienAmount;
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly ILiabilityCalcs _liabilityCalcs;
        private readonly IProposedHousingMortgRelatedCalcs _proposedHousingMortgRelatedCalcs;
        private readonly IReoCalcs _reoCalcs;
        public NetQualifyingReservesCalcs(IAssetCalcs assetCalcs, MortgageTerms mortgageTerms, decimal subordinateLienAmount,
             ICashFromBorrower cashFromBorrower, ILiabilityCalcs liabilityCalcs, IReoCalcs reoCalcs,
             IProposedHousingMortgRelatedCalcs proposedHousingMortgRelatedCalcs = null)
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
            result = this.TotalUsableAssets() - _cashFromBorrower.CashFromBorrower();
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
    }
}
