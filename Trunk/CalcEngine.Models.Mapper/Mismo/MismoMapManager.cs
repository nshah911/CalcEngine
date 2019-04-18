using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class MismoMapManager
    {
        private readonly LoanApplicationDTO _mismo_v231 = null;

        public MismoMapManager(LoanApplicationDTO mismo_v231)
        {
            _mismo_v231 = mismo_v231;
        }

        public MismoLoan MapToLoan()
        {
            MismoLoan loan = new MismoLoan();

            if (_mismo_v231 != null)
            {
                loan.Borrowers = Borrowers();
                loan.Assets = Assets();
                loan.AdditionalCaseData = AdditionalCaseData();
                loan.Liabilities = Liability();
                loan.MortgageTerms = MortgageTerms();
                loan.ProposedHousingExpenses = ProposedHousingExpense();
                loan.ReoProperties = ReoProperty();
                loan.TransactionDetail = TransactionDetail();
                loan.LoanPurpose = LoanPurpose();
                loan.LoanProductData = LoanProductData();
            }

            return loan;
        }

        private List<Borrower> Borrowers()
        {
            List<Borrower> borrower = new List<Borrower>();
            if (_mismo_v231 != null
                && _mismo_v231.BORROWER != null)
            {
                BorrowerMapper map = new BorrowerMapper(_mismo_v231.BORROWER);
                borrower = map.Convert();
            }

            return borrower;
        }

        private LoanProductData LoanProductData()
        {
            LoanProductData loanProductData = null;
            if (_mismo_v231 != null
                && _mismo_v231.LOAN_PRODUCT_DATA != null)
            {
                LoanProductDataMapper map = new LoanProductDataMapper(_mismo_v231.LOAN_PRODUCT_DATA);
                loanProductData = map.Convert();
            }
            return loanProductData;
        }

        private AdditionalCaseData AdditionalCaseData()
        {
            AdditionalCaseData additionalCaseData = new AdditionalCaseData();
            if (_mismo_v231 != null
                && _mismo_v231.ADDITIONAL_CASE_DATA != null)
            {
                AdditionalCaseDataMapper map = new AdditionalCaseDataMapper(_mismo_v231.ADDITIONAL_CASE_DATA);
                additionalCaseData = map.Convert();
            }
            return additionalCaseData;
        }

        private List<Asset> Assets()
        {
            List<Asset> list = new List<Asset>();
            Asset dto = null;
            if (_mismo_v231 != null
                && _mismo_v231.ASSET != null)
            {
                for (int i = 0; i <= _mismo_v231.ASSET.Count - 1; i++)
                {
                    AssetMapper map = new AssetMapper(_mismo_v231.ASSET[i]);
                    dto = map.Convert();
                    list.Add(dto);
                }
            }

            return list;
        }
        private List<Liability> Liability()
        {
            List<Liability> list = new List<Liability>();
            Liability dto = null;
            if (_mismo_v231 != null
                && _mismo_v231.LIABILITY != null)
            {
                for (int i = 0; i <= _mismo_v231.LIABILITY.Count - 1; i++)
                {
                    LiabiltyMapper map = new LiabiltyMapper(_mismo_v231.LIABILITY[i]);
                    dto = map.Convert();
                    list.Add(dto);
                }
            }

            return list;
        }
        private MortgageTerms MortgageTerms()
        {
            MortgageTerms mortgageTerms = new MortgageTerms();
            if (_mismo_v231 != null
                && _mismo_v231.MORTGAGE_TERMS != null)
            {
                MortgageTermsMapper map = new MortgageTermsMapper(_mismo_v231.MORTGAGE_TERMS);
                mortgageTerms = map.Convert();
            }
            return mortgageTerms;
        }

        private List<ProposedHousingExpense> ProposedHousingExpense()
        {
            List<ProposedHousingExpense> list = new List<ProposedHousingExpense>();
            ProposedHousingExpense dto = null;
            if (_mismo_v231 != null
                && _mismo_v231.PROPOSED_HOUSING_EXPENSE != null)
            {
                for (int i = 0; i <= _mismo_v231.PROPOSED_HOUSING_EXPENSE.Count - 1; i++)
                {
                    ProposedHousingExpenseMapper map = new ProposedHousingExpenseMapper(_mismo_v231.PROPOSED_HOUSING_EXPENSE[i]);
                    dto = map.Convert();
                    list.Add(dto);
                }
            }

            return list;
        }

        //private List<PresentHousingExpense> PresentHousingExpense()
        //{
        //    List<PresentHousingExpense> list = new List<PresentHousingExpense>();
        //    PresentHousingExpense dto = null;
        //    if (_mismo_v231 != null
        //        && _mismo_v231.BORROWER != null
        //        && _mismo_v231.BORROWER.FindIndex(b => b._PrintPositionType.Trim() == "Borrower") > 0)
        //    {
        //        BorrowerDTO borrowerDTO = _mismo_v231.BORROWER.Find(b => b._PrintPositionType.Trim() == "Borrower");
        //        if (borrowerDTO != null 
        //            && borrowerDTO.PRESENT_HOUSING_EXPENSE != null 
        //            && borrowerDTO.PRESENT_HOUSING_EXPENSE.Count > 0)
        //        {
        //            for (int i = 0; i <= borrowerDTO.PRESENT_HOUSING_EXPENSE.Count - 1; i++)
        //            {
        //                PresentHousingExpenseMapper map = new PresentHousingExpenseMapper(borrowerDTO.PRESENT_HOUSING_EXPENSE[i]);
        //                dto = map.Convert();
        //                list.Add(dto);
        //            }
        //        }
        //    }

        //    return list;
        //}

        private List<ReoProperty> ReoProperty()
        {
            List<ReoProperty> list = new List<ReoProperty>();
            ReoProperty dto = null;
            if (_mismo_v231 != null
                && _mismo_v231.REO_PROPERTY != null)
            {
                for (int i = 0; i <= _mismo_v231.REO_PROPERTY.Count - 1; i++)
                {
                    ReoPropertyMapper map = new ReoPropertyMapper(_mismo_v231.REO_PROPERTY[i], Liability());
                    dto = map.Convert();
                    list.Add(dto);
                }
            }
            return list;
        }

        private TransactionDetail TransactionDetail()
        {
            TransactionDetail TransactionDetail = new TransactionDetail();
            if (_mismo_v231 != null
                && _mismo_v231.TRANSACTION_DETAIL != null)
            {
                TransactionDetailMapper map = new TransactionDetailMapper(_mismo_v231.TRANSACTION_DETAIL);
                TransactionDetail = map.Convert();
            }
            return TransactionDetail;
        }

        private LoanPurpose LoanPurpose()
        {
            LoanPurpose LoanPurpose = new LoanPurpose();
            if (_mismo_v231 != null
                && _mismo_v231.LOAN_PURPOSE != null)
            {
                LoanPurposeMapper map = new LoanPurposeMapper(_mismo_v231.LOAN_PURPOSE);
                LoanPurpose = map.Convert();
            }
            return LoanPurpose;
        }
    }
}
