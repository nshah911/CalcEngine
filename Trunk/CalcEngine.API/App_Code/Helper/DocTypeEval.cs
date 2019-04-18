using CalcEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalcEngine.API.App_Code.Helper
{
    public static class DocTypeEval
    {
        public static DocTypeEnum Get(string docType)
        {
            DocTypeEnum docTypeEnum = DocTypeEnum.AssetQualifier;
            docType = docType.ToLower().Trim();

            switch (docType)
            {
                case "assetqualifier":
                case "assetqualifierpremier":
                    docTypeEnum = DocTypeEnum.AssetQualifier;
                    break;
                case "bankstatements":
                    docTypeEnum = DocTypeEnum.BankStatements;
                    break;
                case "debtcovgratio":
                case "debtcovgratiopremier":
                    docTypeEnum = DocTypeEnum.DebtCovgRatio;
                    break;
                case "bankstatementspremier":
                    docTypeEnum = DocTypeEnum.BankStatementsPremier;
                    break;
                case "fulldocumentation":
                case "agencypluspremier":
                    docTypeEnum = DocTypeEnum.FullDocumentation;
                    break;
                default:
                    docTypeEnum = DocTypeEnum.Unknown;
                    break;
            }

            return docTypeEnum;
        }
    }
}
