using CalcEngine.Models.Configuration;
using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs.DocType.Helper
{
    public class AssetTypePercentConfigHelper
    {
        private readonly DocTypeCalcResolver _docTypeResolver;
        private List<AssetTypePercentageConfiguration> listAssetTypePercentage = new List<AssetTypePercentageConfiguration>();
        private decimal _Ltv;
        public AssetTypePercentConfigHelper(DocTypeCalcResolver docTypeCalcResolver, decimal Ltv)
        {
            _docTypeResolver = docTypeCalcResolver;
            _Ltv = Ltv;
        }

        public List<AssetTypePercentageConfiguration> ListOfAssetTypePercentConfiguration
        {
            get
            {
                switch (_docTypeResolver.DocType)
                {
                    case Models.Enums.DocTypeEnum.AssetQualifier:
                        listAssetTypePercentage = GetListOfAssetQualifierAssetTypePercentage();
                        break;
                    case Models.Enums.DocTypeEnum.BankStatements:
                        listAssetTypePercentage = GetListOfCommonDocTypeAssetTypePercentage();
                        break;
                    case Models.Enums.DocTypeEnum.BankStatementsPremier:
                        listAssetTypePercentage = GetListOfCommonDocTypeAssetTypePercentage();
                        break;
                    case Models.Enums.DocTypeEnum.DebtCovgRatio:
                        listAssetTypePercentage = GetListOfCommonDocTypeAssetTypePercentage();
                        break;
                    case Models.Enums.DocTypeEnum.FullDocumentation:
                        listAssetTypePercentage = GetListOfCommonDocTypeAssetTypePercentage();
                        break;
                    default:
                        break;
                }
                return listAssetTypePercentage;
            }
        }

        private List<AssetTypePercentageConfiguration> GetListOfCommonDocTypeAssetTypePercentage()
        {
            List<AssetTypePercentageConfiguration> _AssetTypeConfigList = new List<AssetTypePercentageConfiguration>();

            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.CheckingAccount, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.SavingsAccount, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.PendingNetSaleProceedsFromRealEstateAssets, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.CertificateOfDepositTimeDeposit, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.MoneyMarketFund, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.GiftsTotal, UsablePercent = _Ltv <= 75 ? 1.0M : 0 });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.GiftsNotDeposited, UsablePercent = _Ltv <= 75 ? 1.0M : 0 });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.BridgeLoanNotDeposited, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.SecuredBorrowedFundsNotDeposited, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.LifeInsurance, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.Stock, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.MutualFund, UsablePercent = 1.0M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.RetirementFund, UsablePercent = 1.0M });
            return _AssetTypeConfigList;
        }

        private List<AssetTypePercentageConfiguration> GetListOfAssetQualifierAssetTypePercentage()
        {
            List<AssetTypePercentageConfiguration> _AssetTypeConfigList = new List<AssetTypePercentageConfiguration>();

            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.CheckingAccount, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.SavingsAccount, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.PendingNetSaleProceedsFromRealEstateAssets, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.CertificateOfDepositTimeDeposit, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.MoneyMarketFund, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.GiftsTotal, UsablePercent = 0 });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.GiftsNotDeposited, UsablePercent = 0 });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.BridgeLoanNotDeposited, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.SecuredBorrowedFundsNotDeposited, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.LifeInsurance, UsablePercent = 1.0M});
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.Stock, UsablePercent = 0.9M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.MutualFund, UsablePercent = 0.9M });
            _AssetTypeConfigList.Add(new AssetTypePercentageConfiguration { AssetTypeId = AssetType.RetirementFund, UsablePercent = 0.8M });
            return _AssetTypeConfigList;
        }
    }
}
