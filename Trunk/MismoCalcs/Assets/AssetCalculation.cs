using CalcEngine.Logging;
using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.Assets
{
    public class AssetCalculation 
    {
        private readonly IAssetModelCommonCalcs _AssetModelCommonCals = null;
        private readonly IAssetCalcs _AssetCalcs= null;
        private readonly ITotalResidualAssets _totalResidualAssets = null;

        public AssetCalculation(IAssetCalcs assetCalcs, IAssetModelCommonCalcs assetModelCommonCalcs, ITotalResidualAssets totalResidualAssets)
        {
            _AssetModelCommonCals = assetModelCommonCalcs;
            _AssetCalcs = assetCalcs;
            _totalResidualAssets = totalResidualAssets;
        }

        /// <summary>
        /// Gets the total of the following asset types:<para></para>
        /// SavingsAccount<para></para>
        /// PendingNetSaleProceedsFromRealEstateAssets<para></para>
        /// CertificateOfDepositTimeDeposit<para></para>
        /// MoneyMarketFund<para></para>
        /// GiftsTotal<para></para>
        /// GiftsNotDeposited<para></para>
        /// BridgeLoanNotDeposited<para></para>
        /// SecuredBorrowedFundsNotDeposited<para></para>
        /// LifeInsurance<para></para>
        /// </summary>
        public decimal LiquidAssetsTotal()
        {
            this.Info("Calculating LiquidAssetsTotal");
            decimal total = _AssetCalcs.LiquidAssetsTotal();
            return total;
        }

        public decimal TotalResidualAssets()
        {
            this.Info("Calculating TotalResidualAssets");
            decimal total = _totalResidualAssets.TotalResidualAssetsAmount();
            return total;
        }

        public decimal MutualFundsTotal()
        {
            this.Info("Calculating MutualFundsTotal");
            decimal total = _AssetModelCommonCals.Total(AssetType.MutualFund);

            return total;
        }

        public decimal RetirementAccountsTotal()
        {
            this.Info("Calculating RetirementAccountsTotal");
            decimal total = _AssetModelCommonCals.Total(AssetType.RetirementFund);

            return total;
        }

        public decimal StocksTotal()
        {
            this.Info("Calculating StocksTotal");
            decimal total = _AssetModelCommonCals.Total(AssetType.Stock);

            return total;
        }

        /// <summary>
        /// Gets the total usable of the following asset types:<para></para>
        /// SavingsAccount<para></para>
        /// PendingNetSaleProceedsFromRealEstateAssets<para></para>
        /// CertificateOfDepositTimeDeposit<para></para>
        /// MoneyMarketFund<para></para>
        /// GiftsTotal<para></para>
        /// GiftsNotDeposited<para></para>
        /// BridgeLoanNotDeposited<para></para>
        /// SecuredBorrowedFundsNotDeposited<para></para>
        /// LifeInsurance<para></para>
        /// </summary>
        public decimal LiquidAssetsUsable()
        {
            this.Info("Calculating LiquidAssetsUsable");
            return _AssetCalcs.LiquidAssetsUsable();
        }

        public decimal MutualFundsUsable()
        {
            this.Info("Calculating MutualFundsUsable");
            decimal total = _AssetModelCommonCals.TotalUsable(AssetType.MutualFund);

            return total;
        }

        public decimal RetirementAccountsUsable()
        {
            this.Info("Calculating RetirementAccountsUsable");
            decimal total = _AssetModelCommonCals.TotalUsable(AssetType.RetirementFund);

            return total;
        }

        public decimal StocksUsable()
        {
            this.Info("Calculating StocksUsable");
            decimal total = _AssetModelCommonCals.TotalUsable(AssetType.Stock);

            return total;
        }
    }
}
