using CalcEngine.Calcs.Assets;
using CalcEngine.Calcs.Credit;
using CalcEngine.Calcs.DocType.Helper;
using CalcEngine.Calcs.Liabilities;
using CalcEngine.Calcs.Loan;
using CalcEngine.Calcs.ProposedHousing;
using CalcEngine.Calcs.Reo;
using CalcEngine.Calcs.TransactionDetail;
using CalcEngine.Logging;
using CalcEngine.Models;
using Microsoft.Extensions.Logging;
using MismoCalcs.Implementation.Assets;
using MismoCalcs.Implementation.Credit;
using MismoCalcs.Implementation.Incomes;
using MismoCalcs.Implementation.Liabilities;
using MismoCalcs.Implementation.Loan;
using MismoCalcs.Implementation.Loan.AdditionalReservesRequired;
using MismoCalcs.Implementation.Loan.BaseReserve;
using MismoCalcs.Implementation.Loan.CashoutProceed;
using MismoCalcs.Implementation.Loan.DTI;
using MismoCalcs.Implementation.Loan.LTV;
using MismoCalcs.Implementation.Loan.NetQualifyingReserves;
using MismoCalcs.Implementation.Loan.NetReservesAfterCashout;
using MismoCalcs.Implementation.Loan.ProgramReservesRequired;
using MismoCalcs.Implementation.ProposedHousing;
using MismoCalcs.Implementation.REOs;
using MismoCalcs.Implementation.TransactionDetail;
using MismoCalcs.Interface.Assets;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.Incomes;
using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Interface.REOs;
using MismoCalcs.Interface.TransactionDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcEngine.Calcs.DocType.AgencyPlus
{
    public class AgencyPlusCalcsCreator
    {
        private static DateTime GuideLineDt_20180621 = new DateTime(2018, 5, 31);
        private readonly DocTypeCalcResolver _resolver;
        private readonly IUSLoan _IUSLoan;        

        public AgencyPlusCalcsCreator(DocTypeCalcResolver resolver, IUSLoan loan)
        {
            _resolver = resolver;
            _IUSLoan = loan;
            _IUSLoan.AllowReserve = false;
        }

        public Calculations Create()
        {
            this.Info("Creating AgencyPlusCalcsCreator");
            Calculations calculations = new Calculations();

            if (_resolver.GuideLineDt >= GuideLineDt_20180621)
            {
                ILTVCalc ltvCalc = new LTVCalc(_IUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount,
                    _IUSLoan.MismoLoan.TransactionDetail.PurchasePriceAmount,
                    _IUSLoan.MismoLoan.AdditionalCaseData.TransmittalData.PropertyAppraisedValueAmount);

                AssetTypePercentConfigHelper assetTypePercentConfigHelper = new AssetTypePercentConfigHelper(_resolver, ltvCalc.LTV());
                //Assets initilization of common and calcs
                IAssetModelCommonCalcs assetModelCommonCalcs = new AssetModelCommonCalcs(_IUSLoan.MismoLoan.Assets,
                    assetTypePercentConfigHelper.ListOfAssetTypePercentConfiguration);
                IAssetCalcs assetCalcs = new AssetCalcs(assetModelCommonCalcs);
                

                //Transaction Details inilization for AQ
                ITransactionDetailCommonCalcs transactionDetailCommonCalcs =
                    new TransactionDetailCommonCalcs(_IUSLoan.MismoLoan.TransactionDetail);
                ITransactionDetailRefinanceCalcs transactionDetailRefinanceCalcs =
                    new TransactionDetailRefinanceCalcs(_IUSLoan.MismoLoan.TransactionDetail,
                    _IUSLoan.MismoLoan.LoanPurpose._Type);
                TransactionDetailCalculation transactionDetailCalculation = new TransactionDetailCalculation(
                    transactionDetailCommonCalcs);

                IProposedHousingModelCommonCalcs proposedHousingExpenseModelCommonCalcs =
                    new ProposedHousingCommonCalcs(_IUSLoan.MismoLoan.ProposedHousingExpenses);
                IProposedHousingCalcs proposedHousingExpenseCalcs =
                    new ProposedHousingExpenseCalcs(proposedHousingExpenseModelCommonCalcs);
                IProposedHousingQualifyingTIA proposedHousingQualifyingTIA = new QualifyingTIACalcs(proposedHousingExpenseModelCommonCalcs);
                ProposedHousingExpenseCalculation proposedHousingCalculation =
                   new ProposedHousingExpenseCalculation(proposedHousingExpenseCalcs, proposedHousingQualifyingTIA);


                // Liabilities Calculation initilization
                ILiabilityModelCommonCalcs liabilitiesCommonCalcs =
                    new LiabilityModelCommonCalcs(_IUSLoan.MismoLoan.Liabilities);
                ICreditLiabilityCalcs creditLiabilityCalcs
                    = new CreditModelCommonCalcs(_IUSLoan.CreditReports);
                ILiabilityCalcs liabilityCalcs = new LiabilitiesCalcs(liabilitiesCommonCalcs, creditLiabilityCalcs);
                LiabilitiesCalculation liabilitiesCalculation = new LiabilitiesCalculation(liabilityCalcs, liabilitiesCommonCalcs,
                    creditLiabilityCalcs);

                //Credit Calculation
                ICreditCalcs creditCommonCalcs = new CreditModelCommonCalcs(_IUSLoan.CreditReports);
                ICreditCalcs creditCalcs = new CreditCalcs(creditCommonCalcs);
                CreditCalculation creditCalculation = new CreditCalculation(creditCommonCalcs);

                IPresentTotalHousingExpenseCalc presentTotalHousingExpenseCalc =
                    new PresentTotalHousingExpenseCalc(_IUSLoan.MismoLoan.ReoProperties,
                    _IUSLoan.MismoLoan.LoanPurpose,
                    _IUSLoan.MismoLoan.Borrowers);


                IReoModelCommonCalcs reoModelCommonCalcs =
                        new REOModelCommonCalcs(_IUSLoan.MismoLoan.ReoProperties);
                ReoCalcs reoCalcs = new ReoCalcs(reoModelCommonCalcs);
                IReoReserves reoReserves = new ReoReserves(_IUSLoan.MismoLoan.MortgageTerms,
                    ltvCalc, reoModelCommonCalcs, creditLiabilityCalcs, _IUSLoan.MismoLoan.ReoProperties);
                ReoCalculation reoCalculation = new ReoCalculation(reoCalcs, reoModelCommonCalcs, reoReserves);


                ICashFromBorrower cashFromBorrowerCalcs = new CashFromBorrowerCalcs(transactionDetailCommonCalcs,
                    transactionDetailRefinanceCalcs, _IUSLoan.MismoLoan);


                ITotalResidualAssets totalResidualAssets = new TotalResidualAssets(assetCalcs, cashFromBorrowerCalcs, _IUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount, 
                    _IUSLoan.MismoLoan.TransactionDetail.SubordinateLienAmount);

                AssetCalculation assetCalculation = new AssetCalculation(assetCalcs, assetModelCommonCalcs, totalResidualAssets);

                ICashoutProceed cashoutProceedCalcs = new CashoutProceedAPnBK(cashFromBorrowerCalcs, ltvCalc);

                IIncomeModelCommonCalcs incomeModelCommon = new IncomeModelCommonCalcs(_IUSLoan.MismoLoan.Borrowers);
                IIncomeCalcs incomeCalcs = new IncomeCalcs(incomeModelCommon);

                IReservesForNonSubjRetainedPropertiesCalc reservesForNonSubjRetainedPropertiesCalc =
                    new ReservesForNonSubjRetainedPropertiesCalc(
                        _IUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount,
                        _IUSLoan.MismoLoan.ReoProperties,
                        ltvCalc,
                        creditLiabilityCalcs,
                        liabilityCalcs);

                IQualifyingPIPayment qualifyingPIPayment = new QualifyingPIPayment(_IUSLoan.MismoLoan.MortgageTerms, 
                    _IUSLoan.ImpacProgramCode, _IUSLoan.MismoLoan.LoanProductData.RateAdjustment.FirstOrDefault());

                

                IDTICalc dTICalc = new DTIBackRatioAgencyPlus(reoCalcs, incomeCalcs,
                    presentTotalHousingExpenseCalc, liabilityCalcs, _IUSLoan.MismoLoan.LoanPurpose,
                    proposedHousingExpenseCalcs);


                INetQualifyingReserves netQualifyingReserves = new NetQualifyingReservesCalcs(assetCalcs, _IUSLoan.MismoLoan.MortgageTerms,
                    _IUSLoan.MismoLoan.TransactionDetail.SubordinateLienAmount, cashFromBorrowerCalcs, liabilityCalcs,reoCalcs, null);

                IBaseReserveCalc baseReserveCalc = new BaseReverseCalc(
                    _IUSLoan.MismoLoan.LoanProductData,
                    _IUSLoan.MismoLoan.MortgageTerms,
                    _IUSLoan.MismoLoan.LoanPurpose,
                    ltvCalc,
                    creditCalcs,
                    creditLiabilityCalcs,
                    proposedHousingExpenseCalcs,
                    presentTotalHousingExpenseCalc,
                    reoCalcs,
                    dTICalc,
                    proposedHousingQualifyingTIA, qualifyingPIPayment, _IUSLoan.ImpacProgramCode);

                IAdditionalReservesRequired additionalReservesRequired = new AdditionalReservesRequiredCalcs(reservesForNonSubjRetainedPropertiesCalc, baseReserveCalc);

                IProgramReservesRequired programReservesRequired = new ProgramReservesRequiredCalcs(reservesForNonSubjRetainedPropertiesCalc, baseReserveCalc);

                INetReserveAfterCashOut netReserveAfterCashOut = new NetReserveAfterCashOut(_IUSLoan,reoReserves, cashFromBorrowerCalcs, baseReserveCalc);

                LoanCalculation loanCalcs = new LoanCalculation(
                   presentTotalHousingExpenseCalc,
                   baseReserveCalc,
                   ltvCalc,
                   cashFromBorrowerCalcs,
                   reservesForNonSubjRetainedPropertiesCalc,
                   programReservesRequired,
                   additionalReservesRequired,
                   netReserveAfterCashOut,
                   qualifyingPIPayment,
                   cashoutProceedCalcs,
                   netQualifyingReserves);

                calculations = new AgencyPlusCalc(
                    assetCalculation,
                    creditCalculation,
                    liabilitiesCalculation,
                    proposedHousingCalculation,
                    loanCalcs,
                    reoCalculation,
                    transactionDetailCalculation);
            }
            else
            {
                calculations.Message = "Please select Guideline Publication Date on or after 6/21/2018 and rerun IUS.";
            }
            return calculations;
        }
    }
}
