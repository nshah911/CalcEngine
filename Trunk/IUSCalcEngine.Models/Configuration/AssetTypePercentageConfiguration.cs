using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Configuration
{
    public class AssetTypePercentageConfiguration
    {
        public AssetType AssetTypeId { get; set; }

        public decimal UsablePercent { get; set; }
    }
}
