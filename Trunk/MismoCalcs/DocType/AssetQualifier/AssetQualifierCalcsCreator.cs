using CalcEngine.Calcs.DocType;
using CalcEngine.Models;
using MismoCalcs.Interface.Assets;
using MismoCalcs.Implementation.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using CalcEngine.Calcs.DocType.Helper;
using MismoCalcs.Interface;
using CalcEngine.Calcs.Loan;
using CalcEngine.Calcs.TransactionDetail;
using MismoCalcs.Interface.Loan;
using MismoCalcs.Interface.TransactionDetail;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Implementation.ProposedHousing;
using CalcEngine.Calcs.Liabilities;
using MismoCalcs.Implementation.Liabilities;
using MismoCalcs.Interface.Liabilities;
using MismoCalcs.Interface.REOs;
using MismoCalcs.Implementation.REOs;
using CalcEngine.Calcs.Assets;
using CalcEngine.Calcs.Reo;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Implementation.Credit;
using CalcEngine.Calcs.Credit;
using MismoCalcs.Implementation.Loan;
using MismoCalcs.Implementation.TransactionDetail;
using CalcEngine.Calcs.ProposedHousing;
using Microsoft.Extensions.Logging;
using MismoCalcs.Implementation.Loan.BaseReserve;
using MismoCalcs.Implementation.Loan.LTV;
using MismoCalcs.Implementation.Loan.NetReservesAfterCashout;
using MismoCalcs.Implementation.Loan.AdditionalReservesRequired;
using MismoCalcs.Implementation.Loan.ProgramReservesRequired;
using System.Linq;
using MismoCalcs.Implementation.Loan.CashoutProceed;
using MismoCalcs.Implementation.Loan.NetQualifyingReserves;
using CalcEngine.Logging;

namespace CalcEngine.Calcs.DocType.AssetQualifier
{
    /// <summary>
    /// Each <<DocType>>CalcsCreator class will be responsible for creating an
    /// instance of a <<DocType>>Calcs object with the correct interfaces injected
    /// into the object's contructor.
    /// </summary>
    public class AssetQualifierCalcsCreator
    {
        private static DateTime GuideLineDt_20180621 = new DateTime(2018, 5, 31);
        private readonly DocTypeCalcResolver _resolver;
        private readonly IUSLoan _IUSLoan;

        public AssetQualifierCalcsCreator(DocTypeCalcResolver resolver, IUSLoan loan)
        {
            _resolver = resolver;
            _IUSLoan = loan;
            _IUSLoan.AllowReserve = false;
        }

        public Calculations Create()
        {
            this.Info("Creating AssetQualifierCalcsCreator");
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
                TransactionDetailCalculation transactionDetailCalcs = new TransactionDetailCalculation(
                    transactionDetailCommonCalcs, transactionDetailRefinanceCalcs);

                IProposedHousingModelCommonCalcs proposedHousingExpenseModelCommonCalcs =
                    new ProposedHousingCommonCalcs(_IUSLoan.MismoLoan.ProposedHousingExpenses);
                IProposedHousingCalcs proposedHousingExpenseCalcs = 
                    new ProposedHousingExpenseCalcs(proposedHousingExpenseModelCommonCalcs);
                IProposedHousingQualifyingTIA proposedHousingQualifyingTIA = new QualifyingTIACalcs(proposedHousingExpenseModelCommonCalcs);
                IProposedHousingMortgRelatedCalcs proposedHousingMortgRelatedExpenseCalcs =
                    new AssetQualifierProposedHousingCalcs(proposedHousingExpenseModelCommonCalcs);
                ProposedHousingExpenseCalculation proposedHousingCalcs =
                    new ProposedHousingExpenseCalculation(proposedHousingExpenseCalcs, proposedHousingQualifyingTIA, proposedHousingMortgRelatedExpenseCalcs);

                // Liabilities Calculation initilization
                ILiabilityModelCommonCalcs liabilitiesCommonCalcs =
                    new LiabilityModelCommonCalcs(_IUSLoan.MismoLoan.Liabilities);
                ICreditLiabilityCalcs creditLiabilityCalcs
                    = new CreditModelCommonCalcs(_IUSLoan.CreditReports);
                ILiabilityCalcs liabilityCalcs = new LiabilitiesCalcs(liabilitiesCommonCalcs, creditLiabilityCalcs);
                LiabilitiesCalculation liabilitiesCalcs = new LiabilitiesCalculation(liabilityCalcs, liabilitiesCommonCalcs,
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
                IReoReserves reoReserves = new ReoReserves(_IUSLoan.MismoLoan.MortgageTerms, ltvCalc, reoModelCommonCalcs, creditLiabilityCalcs,
                    _IUSLoan.MismoLoan.ReoProperties);
                ReoCalculation reoCalculation = new ReoCalculation(reoCalcs, reoModelCommonCalcs, reoReserves);

                ICashFromBorrower cashFromBorrowerCalcs = new CashFromBorrowerCalcs(transactionDetailCommonCalcs,
                    transactionDetailRefinanceCalcs, _IUSLoan.MismoLoan);

                ITotalResidualAssets totalResidualAssets = new TotalResidualAssetsAQ(assetCalcs, cashFromBorrowerCalcs, _IUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount,
                    _IUSLoan.MismoLoan.TransactionDetail.SubordinateLienAmount);

                AssetCalculation assetCalculation = new AssetCalculation(assetCalcs, assetModelCommonCalcs, totalResidualAssets);

                ICashoutProceed cashoutProceedCalcs = new CashoutProceedAQnBKPR(cashFromBorrowerCalcs, ltvCalc);

                IQualifyingPIPayment qualifyingPIPayment = new QualifyingPIPayment(_IUSLoan.MismoLoan.MortgageTerms,
                    _IUSLoan.ImpacProgramCode, _IUSLoan.MismoLoan.LoanProductData.RateAdjustment.FirstOrDefault());

                

                INetQualifyingReserves netQualifyingReserves = new NetQualifyingReservesAQCalcs(assetCalcs, _IUSLoan.MismoLoan.MortgageTerms, 
                    _IUSLoan.MismoLoan.TransactionDetail.SubordinateLienAmount, cashFromBorrowerCalcs, liabilityCalcs, reoCalcs, proposedHousingMortgRelatedExpenseCalcs);

                IBaseReserveCalc baseReserveCalc = new BaseReverseCalcAssetQualifier(
                    _IUSLoan.MismoLoan.MortgageTerms,
                    _IUSLoan.MismoLoan.LoanPurpose,
                    ltvCalc,
                    creditCalcs,
                    creditLiabilityCalcs,
                    proposedHousingExpenseCalcs,
                    presentTotalHousingExpenseCalc,
                    reoCalcs,
                    proposedHousingQualifyingTIA,
                    qualifyingPIPayment, _IUSLoan.ImpacProgramCode);

                IReservesForNonSubjRetainedPropertiesCalc reservesForNonSubjRetainedPropertiesCalc =
                    new ReservesForNonSubjRetainedPropertiesCalc(
                        _IUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount,
                        _IUSLoan.MismoLoan.ReoProperties, 
                        ltvCalc, 
                        creditLiabilityCalcs,
                        liabilityCalcs);
                INetReserveAfterCashOut netReserveAfterCashOut = new NetReserveAfterCashOutBKPR(ltvCalc, reoReserves, baseReserveCalc);

                IAdditionalReservesRequired additionalReservesRequired = new AdditionalReservesRequiredAQCalcs(liabilityCalcs, proposedHousingMortgRelatedExpenseCalcs, reoCalcs, cashFromBorrowerCalcs);
                
                IProgramReservesRequired programReservesRequired = new ProgramReservesRequiredCalcs(reservesForNonSubjRetainedPropertiesCalc, baseReserveCalc);


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

                calculations = new AssetQualifierCalc(assetCalculation,
                    creditCalculation,
                    liabilitiesCalcs,
                    loanCalcs,
                    proposedHousingCalcs,
                    reoCalculation,
                    transactionDetailCalcs);
            }
            else
            {
                calculations.Message = "Please select Guideline Publication Date on or after 6/21/2018 and rerun IUS.";
            }
            return calculations;
        }
    }
}
