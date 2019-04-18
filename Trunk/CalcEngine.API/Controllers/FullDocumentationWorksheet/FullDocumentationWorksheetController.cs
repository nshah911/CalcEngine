using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.FileManager;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs;
using CalcEngine.Calcs.DocType;
using CalcEngine.Logging;
using CalcEngine.Models;
using CalcEngine.Models.AssetQualifierWorksheet;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalcEngine.API.Controllers.FullDocumentationWorksheet
{
    [Produces("application/json")]
    [Route("api/FullDocumentationWorksheet")]
    public class FullDocumentationWorksheetController : Controller
    {
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public FullDocumentationWorksheetController(
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
                this.Info("Calculating FullDocumantation worksheet");
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
                    // Calculate Assets section
                    worksheet.Assets = GetWorksheetAssets(calcs);

                    // Calculate Residual Assets section
                    worksheet.ResidualAssets = GetWorksheetResidualAssets(calcs, iusLoan);
                    worksheet.ResidualAssets.TotalResidualAsset = calcs.AssetCalculation.TotalResidualAssets();

                    // Calculate 60 months Required Reserves
                    //worksheet.MonthlyLiabilities = GetMonthlyLiabilities(calcs, iusLoan);
                    //worksheet.SixtyMonthsRequiredReserves = GetSixtyMonthRequiredReserves(worksheet.MonthlyLiabilities);

                    //worksheet.ProgramReservesRequired = worksheet.SixtyMonthsRequiredReserves.Total +
                    //    (worksheet.ResidualAssets.CashFromBorrower.CashFromBorrowerTotal > 0
                    //        ? worksheet.ResidualAssets.CashFromBorrower.CashFromBorrowerTotal : 0);

                    // Calculate Qualifying P&I Payment
                    worksheet.QualifyingPI = GetQualifyingPI(calcs).ToString();

                    // Calculate BaseReserve
                    worksheet.BaseReserves = GetBaseReserves(calcs);

                    // Calculate Reo Reserves ForNonSubj Retained Properties
                    worksheet.AdditionalReservesRequired = GetReservesForNonSubjRetainedProperties(calcs);

                    worksheet.TotalRequiredReserves = worksheet.BaseReserves + GetAddOneMonthReserves(calcs);

                    worksheet.UsableCashoutProceeds = GetUsableCashOut(worksheet.ResidualAssets.CashFromBorrower.CashFromBorrowerTotal, calcs.LoanCalcs.LTV());

                    worksheet.NetReservesNeededAfterCashOut = GetNetReserves(worksheet.BaseReserves, worksheet.AdditionalReservesRequired,
                        worksheet.ResidualAssets.CashFromBorrower.CashFromBorrowerTotal, calcs.LoanCalcs.LTV());

                    worksheet.TotalAdditionalReservesStillRequired =
                        worksheet.ResidualAssets.TotalResidualAsset - worksheet.NetReservesNeededAfterCashOut < 0 ?
                        worksheet.ResidualAssets.TotalResidualAsset - worksheet.NetReservesNeededAfterCashOut : 0;


                    worksheet.TotalRequiredAssetsForTransaction = worksheet.ResidualAssets.CashFromBorrower.CashFromBorrowerTotal
                        + worksheet.TotalAdditionalReservesStillRequired;
                }
                else
                {

                }
                return Ok(worksheet);
            }
            catch (Exception ex)
            {
                this.Error("Error in Full Documentation workbook", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private decimal GetNetReserves(decimal baseReserves, decimal additionalReserves, decimal cashFromBorrower, decimal ltv)
        {
            decimal result = 0;
            if(cashFromBorrower <= 0 && ltv <= 80)
            {
                result = baseReserves + additionalReserves - Math.Abs(cashFromBorrower);
            }
            else
            {
                result = baseReserves + additionalReserves;
            }
            return result;
        }

        private decimal GetUsableCashOut(decimal cashFromBorrowerTotal, decimal ltv)
        {
            decimal result = 0;
            if(cashFromBorrowerTotal < 0 && ltv <= 80)
            {
                result = Math.Abs(cashFromBorrowerTotal);
            }
            else
            {
                result = 0;
            }
            return result; ;
            
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
            return Calcs.LoanCalcs.QualifyingPIAmount();
        }

        //private SixtyMonthsRequiredReserves GetSixtyMonthRequiredReserves(MonthlyLiabilities monthlyLiabilities)
        //{
        //    SixtyMonthsRequiredReserves sixtyMonthsRequiredReserves = new SixtyMonthsRequiredReserves
        //    {
        //        Total = monthlyLiabilities.Total * 60
        //    };
        //    return sixtyMonthsRequiredReserves;
        //}

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
            decimal totalCredits = calcs.LoanCalcs.TotalCredit();
            if (iUSLoan.MismoLoan.LoanPurpose._Type == LoanPurposeType.Purchase)
            {
                totalCredits += iUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount;
            }

            ResidualAssets residualAssets = new ResidualAssets
            {
                LoanAmount = iUSLoan.MismoLoan.MortgageTerms.BaseLoanAmount,
                SubordinateLienAmount = iUSLoan.MismoLoan.TransactionDetail.SubordinateLienAmount,
                CashFromBorrower = new CashFromBorrower
                {
                    TotalCosts = calcs.LoanCalcs.TotalCost(),
                    TotalCredits = totalCredits,
                    CashFromBorrowerTotal = calcs.LoanCalcs.CashFromBorrower()
                }
            };

            return residualAssets;
        }

        //private MonthlyLiabilities GetMonthlyLiabilities(Calculations calcs, IUSLoan iUSLoan)
        //{
        //    MonthlyLiabilities monthlyLiabilities = new MonthlyLiabilities
        //    {
        //        TotalMonthlyLiabilities = calcs.LiabilitiesCalcs.TotalMonthlyLiabilites(10),
        //        SubjectPropertyTaxInsAssc = calcs.ProposedHousingCalcs.MortgageRelatedExpenses(),
        //        PITIAforPrimaryResidence = calcs.ReoCalcs.PITIAforPrimaryResidence(),
        //        PITIAforSecondHome = calcs.ReoCalcs.PITIAforSecondHome(),
        //        TotalNetRental = calcs.ReoCalcs.TotalNetRental()
        //    };
        //    monthlyLiabilities.Total = monthlyLiabilities.TotalMonthlyLiabilities
        //            + monthlyLiabilities.SubjectPropertyTaxInsAssc
        //            + monthlyLiabilities.PITIAforPrimaryResidence
        //            + monthlyLiabilities.PITIAforSecondHome
        //            + monthlyLiabilities.TotalNetRental;
        //    return monthlyLiabilities;
        //}
    }
}