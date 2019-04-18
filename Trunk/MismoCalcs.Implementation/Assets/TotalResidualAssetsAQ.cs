using MismoCalcs.Interface.Assets;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Assets
{
    public class TotalResidualAssetsAQ : ITotalResidualAssets
    {
        private readonly IAssetCalcs _assetCalcs;
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly decimal _baseLoanAmount;
        private readonly decimal _subordinateLienAmount;

        public TotalResidualAssetsAQ(IAssetCalcs assetCalcs, ICashFromBorrower cashFromBorrower, decimal baseLoanAmount, decimal subordinateLienAmount)
        {
            _assetCalcs = assetCalcs;
            _cashFromBorrower = cashFromBorrower;
            _baseLoanAmount = baseLoanAmount;
            _subordinateLienAmount = subordinateLienAmount;
        }
        public decimal TotalResidualAssetsAmount()
        {
            decimal result = 0;
            decimal totalUsableAssets = _assetCalcs.LiquidAssetsUsable() + _assetCalcs.MutualFundsUsable() + _assetCalcs.RetirementAccountsUsable() + _assetCalcs.StocksUsable();
            result = totalUsableAssets - _baseLoanAmount - _subordinateLienAmount - _cashFromBorrower.CashFromBorrower();
            return result;
        }
    }
}
