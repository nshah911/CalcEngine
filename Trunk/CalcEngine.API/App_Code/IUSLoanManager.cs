using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs.DocType;
using CalcEngine.Models;
using CalcEngine.Models.BankStatementDTOs;
using CalcEngine.Models.BankStatements;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using CalcEngine.Models.Enums;
using CalcEngine.Models.Mapper.BankStatements;
using CalcEngine.Models.Mapper.Credit;
using CalcEngine.Models.Mapper.LiabilityMerge;
using CalcEngine.Models.Mapper.Mismo;
using CalcEngine.Models.Mapper.MortgageTermsMerge;
using CalcEngine.Models.Mapper.ReoPropertyMerge;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using CalcEngine.Models.Requests;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CalcEngine.API.App_Code
{
    public class IUSLoanManager : ILoanManager
    {
        private IUSLoan _IUSLoan = null;
        private DocTypeCalcResolver _docTypeCalcResolver = null;

        public void SetUp(CustomHeaderData customHeaderData, LoanCalcRequestModel loanCalcRequestModel)
        { 
            MismoContentModel mismoContentModel = loanCalcRequestModel.MismoContent;
            List<CreditContentModel> creditContents = loanCalcRequestModel.CreditContents;

            // Deserialize the MISMO xml to MISMO object
            LoanApplicationDTO mismoXmlLoan = null;
            using (TextReader reader = new StringReader(mismoContentModel.content))
            {
                mismoXmlLoan = (LoanApplicationDTO)new XmlSerializer(typeof(LoanApplicationDTO)).Deserialize(reader);
            }

            // Map MISMO object to IUSLoan.Loan object
            MismoMapManager loanMapper = new MismoMapManager(mismoXmlLoan);
            MismoLoan loan = new MismoLoan();
            loan = loanMapper.MapToLoan();

            // Deserialize each credit xml to Credit MISMO object
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            foreach (CreditContentModel creditContent in creditContents)
            {
                CREDITREPORTING_RESPONSE_GROUP_Type creditXml = null;
                using (TextReader reader = new StringReader(creditContent.content))
                {
                    creditXml = (CREDITREPORTING_RESPONSE_GROUP_Type)new XmlSerializer(typeof(CREDITREPORTING_RESPONSE_GROUP_Type)).Deserialize(reader);
                }

                // Map MISMO object to IUSLoan.Loan object
                CreditMapManager creditMapper = new CreditMapManager(creditXml);
                CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
                creditResponseGroup = creditMapper.MapToCredit();

                creditResponseGroups.Add(creditResponseGroup);
            }
            // Create IUSLoan object
            IUSLoan iUSLoan = new IUSLoan
            {
                DocType = customHeaderData.DocTypeEnum,
                GuideLineDt = customHeaderData.GuidelineDt,
                MismoLoan = loan,
                Source = customHeaderData.SourceType,
                ImpacProgramCode = customHeaderData.ImpacProgramCode,
                CreditReports = creditResponseGroups
            };
            _IUSLoan = iUSLoan;

            //Adding BankStatement if DocType is BankStatement or premier
            if(_IUSLoan.DocType == DocTypeEnum.BankStatements || _IUSLoan.DocType == DocTypeEnum.BankStatementsPremier)
            {
                List<BankStatement> bankStatements = new List<BankStatement>();
                List<BankStatementContentModel> bankStatementContents = loanCalcRequestModel.BankStatementContents;
                if (bankStatementContents != null)
                {
                    foreach(BankStatementContentModel BankStatModel in bankStatementContents)
                    {
                        object bankStatementDTO;
                        XmlSerializer serializer = new XmlSerializer(typeof(BankStatementDTO));
                        using (TextReader reader = new StringReader(BankStatModel.content))
                        {
                            bankStatementDTO = serializer.Deserialize(reader);
                        }
                        BankStatementMapManager BankStatementMapper = new BankStatementMapManager((BankStatementDTO)bankStatementDTO);
                        BankStatement bankStatement= new BankStatement();
                        bankStatement = BankStatementMapper.MapToBankStatement();
                        bankStatements.Add(bankStatement);

                    }
                    _IUSLoan.BankStatements = bankStatements;
                }
                else
                {
                    //_logger.LogError("BankStatement not found");
                }
            }




            if(loanCalcRequestModel.IUSLoanInfo != null)
            {
                // Merge MortgageTerms request info with loan's mortgage terms object
                if (loanCalcRequestModel.IUSLoanInfo.MortgageTermsInfo != null)
                {
                    MortgageTermsMergeManager mortgageTermsMergeMapper = new MortgageTermsMergeManager(loanCalcRequestModel.IUSLoanInfo.MortgageTermsInfo);
                    mortgageTermsMergeMapper.MergeWith(_IUSLoan.MismoLoan.MortgageTerms);
                }


                // Merge ReoProperties request info with loan object
                if (loanCalcRequestModel.IUSLoanInfo.ReoPropertiesInfo != null)
                {
                    ReoPropertyMergeManager reoPropertyMergeMapper = new ReoPropertyMergeManager(loanCalcRequestModel.IUSLoanInfo.ReoPropertiesInfo);
                    reoPropertyMergeMapper.MergeWith(_IUSLoan.MismoLoan.ReoProperties);
                }
            }
            


            // Use Liability Merge Manager to apply bussiness logic on merging liabilities.
            List<CreditLiability> allCreditLiabilities = new List<CreditLiability>();
            foreach (CreditResponseGroup creditReport in _IUSLoan.CreditReports)
            {
                allCreditLiabilities.AddRange(creditReport.Response.ResponseData.CreditResponse.CreditLiabilities);
            }

            LiabilityMergeManager liabilityMergeManager
                    = new LiabilityMergeManager(allCreditLiabilities);
            liabilityMergeManager.MergeWith(_IUSLoan.MismoLoan.Liabilities);

            DocTypeCalcResolver resolver = new DocTypeCalcResolver
            {
                DocType = customHeaderData.DocTypeEnum,
                GuideLineDt = customHeaderData.GuidelineDt,
                Source = customHeaderData.SourceType
            };
            _docTypeCalcResolver = resolver;
        }

        public void SetUp(CustomHeaderData customHeaderData, MismoContentModel mismoContentModel)
        {
            // Deserialize the MISMO xml to MISMO object
            LoanApplicationDTO mismoXmlLoan = null;
            using (TextReader reader = new StringReader(mismoContentModel.content))
            {
                mismoXmlLoan = (LoanApplicationDTO)new XmlSerializer(typeof(LoanApplicationDTO)).Deserialize(reader);
            }

            // Map MISMO object to IUSLoan.Loan object
            MismoMapManager loanMapper = new MismoMapManager(mismoXmlLoan);
            MismoLoan loan = new MismoLoan();
            loan = loanMapper.MapToLoan();

            // Create IUSLoan object
            IUSLoan iUSLoan = new IUSLoan
            {
                DocType = customHeaderData.DocTypeEnum,
                GuideLineDt = customHeaderData.GuidelineDt,
                MismoLoan = loan,
                Source = customHeaderData.SourceType,
                ImpacProgramCode = customHeaderData.ImpacProgramCode
            };
            _IUSLoan = iUSLoan;

            DocTypeCalcResolver resolver = new DocTypeCalcResolver
            {
                DocType = customHeaderData.DocTypeEnum,
                GuideLineDt = customHeaderData.GuidelineDt,
                Source = customHeaderData.SourceType
            };
            _docTypeCalcResolver = resolver;
        }

        public DocTypeCalcResolver GetDocTypeCalcResolver()
        {
            return _docTypeCalcResolver;
        }

        public IUSLoan GetIUSLoan()
        {
            return _IUSLoan;
        }
    }

    public interface ILoanManager
    {
        void SetUp(CustomHeaderData customHeaderData, MismoContentModel mismoContentModel);

        void SetUp(CustomHeaderData customHeaderData, LoanCalcRequestModel loanCalcRequestModel);

        IUSLoan GetIUSLoan();

        DocTypeCalcResolver GetDocTypeCalcResolver();
    }
}
