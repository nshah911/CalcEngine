using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.FileManager;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs;
using CalcEngine.Calcs.DocType;
using CalcEngine.Models;
using CalcEngine.Models.AssetQualifierWorksheet;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using CalcEngine.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace CalcEngine.API.Controllers.AssetQualifierWorksheet
{
    [Produces("application/json")]
    [Route("api/AssetQualifierWorksheet")]
    public class AssetQualifierWorksheetController : Controller
    {
        
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public AssetQualifierWorksheetController(
            IConfiguration configuration,
            ILoanManager IUSLoanManager,
            IGetContentRequest fileManagerApiClient)
        {
            _configuration = configuration;
            _IUSLoanManager = IUSLoanManager;
            _fileManagerApiClient = fileManagerApiClient;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{fileid}/CalculateWorksheet/")]
        public async Task<ActionResult> CalculateWorksheet(string fileId)
        {
            try
            {
                this.Info("Calculating Asset Qualifier worksheet");
                // Validate custom header values
                CustomHeaderData customHeaderData = new CustomHeaderData(HttpContext.Request.Headers);
                customHeaderData.ValidateCustomHeaders();

                // Get full request from disk
                LoanCalcRequestModel loanCalcRequestModel =
                    await _fileManagerApiClient.GetLoanCalcRequestAsync(fileId, $"{_configuration["FileManagerApi:ApiUrl"]}");

                MismoContentModel mismoContentModel = loanCalcRequestModel.MismoContent;
                List<CreditContentModel> creditContents = loanCalcRequestModel.CreditContents;

                // Use custom header information and mismo content to create IUSLoan & DocTypeCalcResolver
                _IUSLoanManager.SetUp(customHeaderData, loanCalcRequestModel);
                DocTypeCalcResolver docTypeCalcResolver = _IUSLoanManager.GetDocTypeCalcResolver();
                IUSLoan iusLoan = _IUSLoanManager.GetIUSLoan();

                // Use Factory to create Calculations Interface
                Calculations calcs = DocTypeCalcFactory.Create(docTypeCalcResolver, iusLoan);

                // Create worksheet model
                Worksheet worksheet = new Worksheet();

                if (calcs.Message == null)
                {
                    // Calculate Assets section C7, C8, C9, C10, E11
                    worksheet.Assets = GetWorksheetAssets(calcs);

                    //LoanAmount, Subordinate financing loan amount, Cash from borrower E12, E13, E14
                    worksheet.ResidualAssets = GetWorksheetResidualAssets(calcs, iusLoan);

                    //Total Residual Assets E15
                    worksheet.ResidualAssets.TotalResidualAsset = calcs.AssetCalculation.TotalResidualAssets();

                    // Total Monthly Liabilites, mortgage related expense, PITIA, Total Net Rental D16, D17, D18, D19, D20, E21
                    worksheet.MonthlyLiabilities = GetMonthlyLiabilities(calcs, iusLoan);

                    //Total 60 months Required Reserves E22
                    worksheet.SixtyMonthsRequiredReserves = GetSixtyMonthRequiredReserves(worksheet.MonthlyLiabilities);

                    // Calculate Qualifying P&I Payment E23
                    worksheet.QualifyingPI = iusLoan.ImpacProgramCode.Contains("IO") ? GetQualifyingPI(calcs).ToString()
                        : calcs.ProposedHousingCalcs.FirstMortgagePIAmount().ToString();

                    // Calculate BaseReserve E24
                    worksheet.BaseReserves = GetBaseReserves(calcs);

                    //Calculate Additional Reserves Required E25
                    worksheet.AdditionalReservesRequired = GetReservesForNonSubjRetainedProperties(calcs);

                    //Additional Reserves Required
                    //worksheet.AdditionalReservesRequired = calcs.LoanCalcs.AdditionalReservesRequired();

                    //Net Reserves Need after Cashout E28
                    worksheet.NetReservesNeededAfterCashOut = worksheet.BaseReserves + worksheet.AdditionalReservesRequired;

                    //Total Additional Still Required E29
                    worksheet.TotalAdditionalReservesStillRequired = worksheet.NetReservesNeededAfterCashOut > 0 ? worksheet.NetReservesNeededAfterCashOut : 0;

                    //Total Required Assets For Transaction E30
                    worksheet.TotalRequiredAssetsForTransaction = worksheet.ResidualAssets.TotalResidualAsset +
                        worksheet.SixtyMonthsRequiredReserves.Total + worksheet.TotalAdditionalReservesStillRequired;

                    //Loan Qualify D31
                    worksheet.LoanQualify = worksheet.ResidualAssets.TotalResidualAsset < worksheet.TotalRequiredAssetsForTransaction ? false : true;

                    //Amount of shortage or funds Remains E32
                    worksheet.FundRemain = worksheet.ResidualAssets.TotalResidualAsset - worksheet.TotalRequiredAssetsForTransaction;

                    //Toal Monthly Income C39
                    worksheet.TotalMonthlyIncome = worksheet.ResidualAssets.TotalResidualAsset > 0 ? worksheet.ResidualAssets.TotalResidualAsset / 60 : 0;

                    //Total Monthly Residual Income E39
                    worksheet.TotalMonthlyResidualIncome = worksheet.TotalMonthlyIncome - worksheet.MonthlyLiabilities.Total;

                    worksheet.Date = DateTime.Now.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    worksheet.Message = calcs.Message;
                }

                return Ok(worksheet);
            }
            catch (Exception ex)
            {
                this.Error("Error in Asset Qualifier worksheet", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private decimal GetAddOneMonthReserves(Calculations Calcs)
        {
            return Calcs.ReoCalcs.AddReserveForOtherFinancedReo();
        }

        private decimal GetReservesForNonSubjRetainedProperties(Calculations Calcs)
        {
            return Calcs.LoanCalcs.ReservesForNonSubjRetainedProperties();
        }

        private decimal GetBaseReserves(Calculations Calcs)
        {
            return Calcs.LoanCalcs.BaseReserve();
        }

        private decimal GetQualifyingPI(Calculations Calcs)
        {
            return Calcs.LoanCalcs.QualifyingPIAmount() ;
        }

        private SixtyMonthsRequiredReserves GetSixtyMonthRequiredReserves(MonthlyLiabilities monthlyLiabilities)
        {
            SixtyMonthsRequiredReserves sixtyMonthsRequiredReserves = new SixtyMonthsRequiredReserves
            {
                Total = monthlyLiabilities.Total * 60
            };
            return sixtyMonthsRequiredReserves;
        }

        private Assets GetWorksheetAssets(Calculations calcs)
        {
            Assets assets = new Assets
            {
                LiquidAssets = new LiquidAssets
                {
                    OriginalAmt = calcs.AssetCalculation.LiquidAssetsTotal(),
                    UsableAmt = calcs.AssetCalculation.LiquidAssetsUsable()
                },
                Stocks = new Stocks
                {
                    OriginalAmt = calcs.AssetCalculation.StocksTotal(),
                    UsableAmt = calcs.AssetCalculation.StocksUsable()
                },
                MutualFunds = new MutualFunds
                {
                    OriginalAmt = calcs.AssetCalculation.MutualFundsTotal(),
                    UsableAmt = calcs.AssetCalculation.MutualFundsUsable()
                },
                Retirement = new Retirement
                {
                    OriginalAmt = calcs.AssetCalculation.RetirementAccountsTotal(),
                    UsableAmt = calcs.AssetCalculation.RetirementAccountsUsable()
                }
            };

            assets.TotalUsable = assets.LiquidAssets.UsableAmt
                + assets.Stocks.UsableAmt
                + assets.MutualFunds.UsableAmt
                + assets.Retirement.UsableAmt;

            assets.TotalVerifiedAmount = assets.LiquidAssets.OriginalAmt
                + assets.MutualFunds.OriginalAmt
                + assets.Retirement.OriginalAmt
                + assets.Stocks.OriginalAmt;

            return assets;
        }

        private ResidualAssets GetWorksheetResidualAssets(Calculations calcs, IUSLoan iUSLoan)
        {
            //decimal totalCredits = calcs.LoanCalcs.TotalCredit();
            //if (iUSLoan.MismoLoan.LoanPurpose._Type == LoanPurposeType.Purchase)
            //{
            //    totalCredits += iUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount;
            //}

            ResidualAssets residualAssets = new ResidualAssets
            {
                LoanAmount = iUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount,
                SubordinateLienAmount = iUSLoan.MismoLoan.TransactionDetail.SubordinateLienAmount,
                CashFromBorrower = new CashFromBorrower
                {
                    TotalCosts = calcs.LoanCalcs.TotalCost(),
                    TotalCredits = calcs.LoanCalcs.TotalCredit(),
                    CashFromBorrowerTotal = calcs.LoanCalcs.CashFromBorrower()
                }
            };
            
            return residualAssets;
        }

        private MonthlyLiabilities GetMonthlyLiabilities(Calculations calcs, IUSLoan iUSLoan)
        {
            MonthlyLiabilities monthlyLiabilities = new MonthlyLiabilities
            {
                TotalMonthlyLiabilities = calcs.LiabilitiesCalcs.TotalMonthlyLiabilites(10),
                SubjectPropertyTaxInsAssc = calcs.ProposedHousingCalcs.MortgageRelatedExpenses(),
                PITIAforPrimaryResidence = calcs.ReoCalcs.PITIAforPrimaryResidence(),
                PITIAforSecondHome = calcs.ReoCalcs.PITIAforSecondHome(),
                TotalNetRental = calcs.ReoCalcs.TotalNetRental()
            };
            monthlyLiabilities.Total = monthlyLiabilities.TotalMonthlyLiabilities
                    + monthlyLiabilities.SubjectPropertyTaxInsAssc
                    + monthlyLiabilities.PITIAforPrimaryResidence
                    + monthlyLiabilities.PITIAforSecondHome
                    + monthlyLiabilities.TotalNetRental;
            return monthlyLiabilities;
        }
    }
}