using CalcEngine.Calcs.DocType.AgencyPlus;
using CalcEngine.Calcs.DocType.AssetQualifier;
using CalcEngine.Calcs.DocType.BankStatements;
using CalcEngine.Calcs.DocType.BankStatementsPR;
using CalcEngine.Calcs.DocType.Investor;
using CalcEngine.Models;
using CalcEngine.Models.Enums;
using Microsoft.Extensions.Logging;

namespace CalcEngine.Calcs.DocType
{
    public static class DocTypeCalcFactory
    {
        /// <summary>
        /// This Factory is responsible for creating a <see cref="ICalculations"/> object 
        /// based on the properties in the <see cref="DocTypeCalcResolver"/> object.
        /// </summary>
        public static Calculations Create(DocTypeCalcResolver resolver, IUSLoan loan)
        {
            Calculations calculations = null;

            switch (resolver.DocType)
            {
                case DocTypeEnum.AssetQualifier:
                    AssetQualifierCalcsCreator creator = new AssetQualifierCalcsCreator(resolver, loan);
                    calculations = creator.Create();
                    break;
                case DocTypeEnum.BankStatements:
                    BankStatementCalcsCreator BkStcreator = new BankStatementCalcsCreator(resolver, loan);
                    calculations = BkStcreator.Create();
                    break;
                case DocTypeEnum.BankStatementsPremier:
                    BankStatementPRCalcsCreator BkStPkcreator = new BankStatementPRCalcsCreator(resolver, loan);
                    calculations = BkStPkcreator.Create();
                    break;
                case DocTypeEnum.FullDocumentation:
                    AgencyPlusCalcsCreator APCreator = new AgencyPlusCalcsCreator(resolver, loan);
                    calculations = APCreator.Create();
                    break;
                case DocTypeEnum.DebtCovgRatio:
                    InvestorCalcsCreator InvCreator = new InvestorCalcsCreator(resolver, loan);
                    calculations = InvCreator.Create();
                    break;
                default:
                    break;
            }

            return calculations;
        }
    }
}
