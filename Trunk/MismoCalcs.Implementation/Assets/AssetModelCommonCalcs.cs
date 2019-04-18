using MismoCalcs.Interface.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Configuration;
using Microsoft.Extensions.Logging;

namespace MismoCalcs.Implementation.Assets
{
    public class AssetModelCommonCalcs : IAssetModelCommonCalcs
    {
        public readonly IEnumerable<Asset> _assets = null;
        public readonly List<AssetTypePercentageConfiguration> _configList = null;        

        public AssetModelCommonCalcs(IEnumerable<Asset> assets, List<AssetTypePercentageConfiguration> configList)
        {
            _assets = assets;
            _configList = configList;
        }

        public decimal Total(AssetType type)
        {
            decimal total = 0.00m;

            foreach (Asset asset in _assets)
            {
                if (asset._Type == type)
                    total += asset._CashOrMarketValueAmount;
            }

            return total;
        }

        public decimal TotalUsable(AssetType type)
        {
            decimal total = 0.00m;

            foreach (Asset asset in _assets)
            {
                AssetTypePercentageConfiguration assetConfig = _configList.Where(a => a.AssetTypeId == asset._Type).FirstOrDefault();
                if (assetConfig != null)
                {
                    if (asset._Type == type)
                        total += asset._CashOrMarketValueAmount * assetConfig.UsablePercent;
                }
                else
                {
                    if (asset._Type == type)
                        total += asset._CashOrMarketValueAmount;
                }
            }
            return total;
        }
    }
}
