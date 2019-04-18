using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs;
using CalcEngine.Calcs.DocType;
using CalcEngine.Logging;
using CalcEngine.Models;
using CalcEngine.Models.AssetQualifierWorksheet;
using CalcEngine.Models.Credit;
using CalcEngine.Models.EncompassUWLaunchPad;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CalcEngine.API.Controllers.EncompassUWLaunchPadWorksheet
{
    [Produces("application/json")]
    [Route("api/EncompassUWLaunchPadWorksheet")]
    public class EncompassUWLaunchPadWorksheetController : Controller
    {
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public EncompassUWLaunchPadWorksheetController(
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
                this.Info("Calculating Encompass UW Launchpad worksheet");
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
                UWLaunchPadWorksheet worksheet = new UWLaunchPadWorksheet();

                if (calcs.Message == null)
                {
                    worksheet.ProgramReservesRequired = calcs.LoanCalcs.ProgramReservesRequired();

                    worksheet.Source = "IUS";

                    worksheet.AdditionalReservesRequired = calcs.LoanCalcs.AdditionalReservesRequired();

                    worksheet.TotalReservesRequired = worksheet.ProgramReservesRequired + worksheet.AdditionalReservesRequired;

                    worksheet.TotalReservesRequiredMonth = Math.Round(worksheet.TotalReservesRequired / GetPITIA(iusLoan, calcs), 2);

                    worksheet.Assets = GetWorksheetAssets(calcs);

                    worksheet.VerifiedAssetsMonth = Math.Round(worksheet.Assets.TotalVerifiedAmount / GetPITIA(iusLoan, calcs), 2);

                    worksheet.FundsAvailableForReserves = calcs.AssetCalculation.TotalResidualAssets();

                    worksheet.UsableCashOutProceed = calcs.LoanCalcs.CashoutProceed();

                    worksheet.UsableCashOutProceedMonth = Math.Round(worksheet.UsableCashOutProceed / GetPITIA(iusLoan, calcs), 2);

                    worksheet.RequiredFundsToClose = calcs.LoanCalcs.CashFromBorrower();

                    worksheet.TotalReservesAfterClose = worksheet.Assets.TotalVerifiedAmount - worksheet.RequiredFundsToClose;

                    worksheet.TotalReservesAfterCloseMonth = Math.Round(worksheet.TotalReservesAfterClose / GetPITIA(iusLoan, calcs), 2);

                    worksheet.TotalReservesWithCashoutProceed = worksheet.Assets.TotalVerifiedAmount
                        + worksheet.UsableCashOutProceed - worksheet.RequiredFundsToClose;

                    worksheet.TotalReservesWithCashoutProceedMonth = Math.Round(worksheet.TotalReservesWithCashoutProceed / GetPITIA(iusLoan, calcs), 2);

                    worksheet.TotalNetQualifyingReserves = calcs.LoanCalcs.NetQualifyingReserves();

                    worksheet.TotalNetCashReserveAmount = worksheet.Assets.TotalUsable - calcs.LoanCalcs.CashFromBorrower()
                        - worksheet.TotalReservesRequired;
                }
                else
                {
                    worksheet.Message = calcs.Message;
                }
                return Ok(worksheet);
            }
            catch(Exception ex)
            {
                this.Error("Error in Encompass UW LaunchPad", ex.Message);
                return BadRequest(ex.Message);
            }

            }


        private decimal GetPITIA(IUSLoan iUSLoan, Calculations calcs)
        {
            decimal result = 0;
            if (iUSLoan.ImpacProgramCode.Contains("IO"))
            {
                result = calcs.LoanCalcs.QualifyingPIAmount() + calcs.ProposedHousingCalcs.ProposedHousingQualifyingTIA();
            }
            else
            {
                result = calcs.ProposedHousingCalcs.ProposedTotalHousingPayment();
            }
            return result;
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
    }
}