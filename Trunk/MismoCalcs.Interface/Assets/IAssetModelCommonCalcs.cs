using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Assets
{
    /// <summary>
    /// This class ending with the name '...CommonCalcs' naming convention is used to perform calculations on nodes that are a
    /// list in the MISMO xml. Below example is for the <ASSET></ASSET> node.
    /// </summary>
    public interface IAssetModelCommonCalcs
    {
        decimal Total(AssetType type);

        decimal TotalUsable(AssetType type);
    }
}
