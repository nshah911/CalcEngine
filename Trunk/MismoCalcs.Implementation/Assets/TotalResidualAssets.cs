using MismoCalcs.Interface.Assets;
using MismoCalcs.Interface.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Assets
{
    public class TotalResidualAssets : ITotalResidualAssets
    {
        private readonly IAssetCalcs _assetCalcs;
        private readonly ICashFromBorrower _cashFromBorrower;
        private readonly decimal _baseLoanAmount;
        private readonly decimal _subordinateLienAmount;

        public TotalResidualAssets(IAssetCalcs assetCalcs, ICashFromBorrower cashFromBorrower, decimal baseLoanAmount, decimal subordinateLienAmount)
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
            totalUsableAssets = totalUsableAssets + _baseLoanAmount + _subordinateLienAmount;
            result = totalUsableAssets - _cashFromBorrower.CashFromBorrower();
            return result;
        }
    }
}
