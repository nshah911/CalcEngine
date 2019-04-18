using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Assets
{
    public class AssetCalcs : IAssetCalcs
    {
        private readonly IAssetModelCommonCalcs _AssetModelCommonCals = null;

        public AssetCalcs(IAssetModelCommonCalcs assetModelCommonCalcs)
        {
            _AssetModelCommonCals = assetModelCommonCalcs;
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
            decimal total = _AssetModelCommonCals.Total(AssetType.CheckingAccount)
                + _AssetModelCommonCals.Total(AssetType.SavingsAccount)
                + _AssetModelCommonCals.Total(AssetType.PendingNetSaleProceedsFromRealEstateAssets)
                + _AssetModelCommonCals.Total(AssetType.CertificateOfDepositTimeDeposit)
                + _AssetModelCommonCals.Total(AssetType.MoneyMarketFund)
                + _AssetModelCommonCals.Total(AssetType.GiftsTotal)
                + _AssetModelCommonCals.Total(AssetType.GiftsNotDeposited)
                + _AssetModelCommonCals.Total(AssetType.BridgeLoanNotDeposited)
                + _AssetModelCommonCals.Total(AssetType.SecuredBorrowedFundsNotDeposited)
                + _AssetModelCommonCals.Total(AssetType.LifeInsurance);

            return total;
        }


        public decimal MutualFundsTotal()
        {
            decimal total = _AssetModelCommonCals.Total(AssetType.MutualFund);

            return total;
        }

        public decimal RetirementAccountsTotal()
        {
            decimal total = _AssetModelCommonCals.Total(AssetType.RetirementFund);

            return total;
        }

        public decimal StocksTotal()
        {
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
            decimal total = _AssetModelCommonCals.Total(AssetType.CheckingAccount)
                + _AssetModelCommonCals.TotalUsable(AssetType.SavingsAccount)
                + _AssetModelCommonCals.TotalUsable(AssetType.PendingNetSaleProceedsFromRealEstateAssets)
                + _AssetModelCommonCals.TotalUsable(AssetType.CertificateOfDepositTimeDeposit)
                + _AssetModelCommonCals.TotalUsable(AssetType.MoneyMarketFund)
                + _AssetModelCommonCals.TotalUsable(AssetType.GiftsTotal)
                + _AssetModelCommonCals.TotalUsable(AssetType.GiftsNotDeposited)
                + _AssetModelCommonCals.TotalUsable(AssetType.BridgeLoanNotDeposited)
                + _AssetModelCommonCals.TotalUsable(AssetType.SecuredBorrowedFundsNotDeposited)
                + _AssetModelCommonCals.TotalUsable(AssetType.LifeInsurance);

            return total;
        }

        public decimal MutualFundsUsable()
        {
            decimal total = _AssetModelCommonCals.TotalUsable(AssetType.MutualFund);

            return total;
        }

        public decimal RetirementAccountsUsable()
        {
            decimal total = _AssetModelCommonCals.TotalUsable(AssetType.RetirementFund);

            return total;
        }

        public decimal StocksUsable()
        {
            decimal total = _AssetModelCommonCals.TotalUsable(AssetType.Stock);

            return total;
        }

    }
}
