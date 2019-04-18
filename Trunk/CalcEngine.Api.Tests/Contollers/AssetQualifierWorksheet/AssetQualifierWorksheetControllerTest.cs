using System;
using Xunit;
using Moq;
using CalcEngine.API.App_Code;
using CalcEngine.Models.Mismo;
using System.Threading.Tasks;
using System.Collections.Generic;
using CalcEngine.API.Controllers.AssetQualifierWorksheet;
using CalcEngine.API.App_Code.Helper;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CalcEngine.Models;
using CalcEngine.Models.AssetQualifierWorksheet;
using CalcEngine.Calcs.DocType;

namespace CalcEngine.Api.Tests.Contollers.AssetQualifierWorksheet
{
    public class AssetQualifierWorksheetControllerTest
    {
        [Fact]
        public void CalculateWorksheet_AssetQulifier_TotalUsableAssets()
        {
            // Arrange
            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger<AssetQualifierWorksheetController>>();

            var mockGetContentRequest = new Mock<IGetContentRequest>();
            mockGetContentRequest.Setup(x => x.GetMismoContentAsync("testfileId", ""))
                .Returns(Task.FromResult<MismoContentModel>(GetMismoContentSample()));

            var mockLoanManager = new Mock<ILoanManager>();
            mockLoanManager.Setup(x => x.GetIUSLoan())
                .Returns(GetIUSLoan_Sample1());
            mockLoanManager.Setup(x => x.GetDocTypeCalcResolver())
                .Returns(GetDocTypeCalcResolver());

            var controller = new AssetQualifierWorksheetController(
                mockConfig.Object,
                mockLoanManager.Object,
                mockGetContentRequest.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = GetDefaultHttpContextWithHeaders();

            // Act
            Task<ActionResult> taskActionResult = controller.CalculateWorksheet("testfileId");
            OkObjectResult actual = (OkObjectResult)taskActionResult.Result;

            // Assert
            Assert.Equal<int>(200, actual.StatusCode.Value);
            Worksheet worksheet = (Worksheet)actual.Value;
            Assert.Equal<decimal>(270, worksheet.Assets.TotalUsable);
        }

        private MismoContentModel GetMismoContentSample()
        {
            MismoContentModel mismoModel = null;
            mismoModel = new MismoContentModel
            {
                content = "<LOAN_APPLICATION></LOAN_APPLICATION>"
            };
            return mismoModel;
        }

        private DefaultHttpContext GetDefaultHttpContextWithHeaders()
        {
            var defaultContext = new DefaultHttpContext();
            defaultContext.Request.Headers.Add("x-guidelinedt", "2018-06-21");
            defaultContext.Request.Headers.Add("x-clientsource", "encompass");
            defaultContext.Request.Headers.Add("x-impacprogramcode", "I30FAD");
            defaultContext.Request.Headers.Add("x-docType", "assetqualifier");
            return defaultContext;
        }

        private IUSLoan GetIUSLoan_Sample1()
        {
            return new IUSLoan
            {
                MismoLoan = new MismoLoan
                {
                    Assets = new List<Asset>
                    {
                        new Asset { _Type = AssetType.SavingsAccount, _CashOrMarketValueAmount = 50.00m },
                        new Asset { _Type = AssetType.CheckingAccount, _CashOrMarketValueAmount = 50.00m },
                        new Asset { _Type = AssetType.MutualFund, _CashOrMarketValueAmount = 100.00m },
                        new Asset { _Type = AssetType.RetirementFund, _CashOrMarketValueAmount = 100.00m }
                    },
                    Liabilities = new List<Liability>
                    {
                    },
                    LoanPurpose = new LoanPurpose
                    {
                        _Type = LoanPurposeType.Unknown
                    },
                    MortgageTerms = new MortgageTerms
                    {
                        BaseLoanAmount = 500000.00m
                    },
                    ProposedHousingExpenses = new List<ProposedHousingExpense>
                    {
                    },
                    ReoProperties = new List<ReoProperty>
                    {
                    },
                    TransactionDetail = new TransactionDetail
                    {
                    }
                }
            };
        }

        private DocTypeCalcResolver GetDocTypeCalcResolver()
        {
            DocTypeCalcResolver resolver = new DocTypeCalcResolver
            {
                DocType = Models.Enums.DocTypeEnum.AssetQulifier,
                GuideLineDt = new DateTime(2018, 6, 21),
                Source = Models.Enums.SourceType.Encompass
            };
            return resolver;
        }
    }
}
