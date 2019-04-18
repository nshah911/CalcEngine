using CalcEngine.Models.Configuration;
using CalcEngine.Models.Mismo;
using MismoCalcs.Implementation.Assets;
using MismoCalcs.Interface.Assets;
using System;
using System.Collections.Generic;
using Xunit;

namespace MismoCalcs.Implementation.Tests.Assets
{
    public class AssetModelCommonCalcsTest
    {
        [Fact]
        public void TotalUsable_for_AssetQualifier_Checking_should_be_100()
        {
            // arrange
            List<Asset> assets = new List<Asset>();
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.SavingsAccount, _CashOrMarketValueAmount = 50.00m });
            AssetModelCommonCalcs calc = new AssetModelCommonCalcs(assets, AssetQualifier_AssetConfig());

            // act
            decimal actual = calc.TotalUsable(AssetType.CheckingAccount);

            // assert
            decimal expected = 100.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        [Fact]
        public void TotalUsable_for_AssetQualifier_Stock_should_be_90()
        {
            // arrange
            List<Asset> assets = new List<Asset>();
            assets.Add(new Asset { _Type = AssetType.Stock, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.Stock, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            AssetModelCommonCalcs calc = new AssetModelCommonCalcs(assets, AssetQualifier_AssetConfig());

            // act
            decimal actual = calc.TotalUsable(AssetType.Stock);

            // assert
            decimal expected = 90.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        [Fact]
        public void TotalUsable_for_AssetQualifier_MutualFund_should_be_90()
        {
            // arrange
            List<Asset> assets = new List<Asset>();
            assets.Add(new Asset { _Type = AssetType.MutualFund, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.MutualFund, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            AssetModelCommonCalcs calc = new AssetModelCommonCalcs(assets, AssetQualifier_AssetConfig());

            // act
            decimal actual = calc.TotalUsable(AssetType.MutualFund);

            // assert
            decimal expected = 90.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        [Fact]
        public void TotalUsable_for_AssetQualifier_RetirementFund_should_be_80()
        {
            // arrange
            List<Asset> assets = new List<Asset>();
            assets.Add(new Asset { _Type = AssetType.RetirementFund, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.RetirementFund, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            AssetModelCommonCalcs calc = new AssetModelCommonCalcs(assets, AssetQualifier_AssetConfig());

            // act
            decimal actual = calc.TotalUsable(AssetType.RetirementFund);

            // assert
            decimal expected = 80.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        [Fact]
        public void Total_for_Stock_should_be_100()
        {
            // arrange
            List<Asset> assets = new List<Asset>();
            assets.Add(new Asset { _Type = AssetType.Stock, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.Stock, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            AssetModelCommonCalcs calc = new AssetModelCommonCalcs(assets, AssetQualifier_AssetConfig());

            // act
            decimal actual = calc.Total(AssetType.Stock);

            // assert
            decimal expected = 100.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        [Fact]
        public void TotalUsuable_for_AssetQaulifier_with_assetType_not_in_assetTypePercent_should_be_1500()
        {
            // arrange
            List<Asset> assets = new List<Asset>();
            assets.Add(new Asset { _Type = AssetType.Automobile, _CashOrMarketValueAmount = 1500.00m });
            assets.Add(new Asset { _Type = AssetType.Stock, _CashOrMarketValueAmount = 50.00m });
            assets.Add(new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m });
            AssetModelCommonCalcs calc = new AssetModelCommonCalcs(assets, AssetQualifier_AssetConfig());

            // act
            decimal actual = calc.TotalUsable(AssetType.Automobile);

            // assert
            decimal expected = 1500.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        private List<AssetTypePercentageConfiguration> AssetQualifier_AssetConfig()
        {
            List<AssetTypePercentageConfiguration> _AssetTypeConfigList = new List<AssetTypePercentageConfiguration>();

            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.CheckingAccount, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.SavingsAccount, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.PendingNetSaleProceedsFromRealEstateAssets, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.CertificateOfDepositTimeDeposit,UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.MoneyMarketFund, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.GiftsTotal, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.GiftsNotDeposited, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.BridgeLoanNotDeposited, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.SecuredBorrowedFundsNotDeposited, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.LifeInsurance, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.Stock, UsablePercent = 0.9M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.MutualFund, UsablePercent = 0.9M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.RetirementFund, UsablePercent = 0.8M });

            return _AssetTypeConfigList;
        }
    }
}
