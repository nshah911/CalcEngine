using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Assets
{
    /// <summary>
    /// Following interface is to expose the methods to be performed on the <ASSET></ASSET> nodes in MISMO.
    /// This interface will be injected into the respective DocType class by the DocType factory.
    /// </summary>
    public interface IAssetCalcs
    {
        decimal LiquidAssetsTotal();

        decimal MutualFundsTotal();

        decimal StocksTotal();

        decimal RetirementAccountsTotal();

        decimal LiquidAssetsUsable();

        decimal MutualFundsUsable();

        decimal StocksUsable();

        decimal RetirementAccountsUsable();
    }
}
