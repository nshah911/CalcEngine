using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.FileManager;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs;
using CalcEngine.Calcs.DocType;
using CalcEngine.Logging;
using CalcEngine.Models;
using CalcEngine.Models.Mismo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface;
using System;
using System.Threading.Tasks;

namespace CalcEngine.API.Controllers.Mismo
{
    [Produces("application/json")]
    [Route("api/mismo/Assets")]
    public class AssetsController : Controller
    {
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public AssetsController(
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
        [Route("{fileid}/LiquidAssetsTotal/")]
        public async Task<ActionResult> LiquidAssetsTotal(string fileId)
        {
            try
            {
                this.Info("Calculating Liquid Assets total");
                // Validate custom header values
                CustomHeaderData customHeaderData = new CustomHeaderData(HttpContext.Request.Headers);
                customHeaderData.ValidateCustomHeaders();

                // Get the file from disk
                MismoContentModel mismoContentModel =
                    await _fileManagerApiClient.GetMismoContentAsync(fileId, $"{_configuration["FileManagerApi:ApiUrl"]}");

                // Use custom header information and mismo content to create IUSLoan & DocTypeCalcResolver
                _IUSLoanManager.SetUp(customHeaderData, mismoContentModel);

                DocTypeCalcResolver docTypeCalcResolver = _IUSLoanManager.GetDocTypeCalcResolver();
                IUSLoan iusLoan = _IUSLoanManager.GetIUSLoan();

                // Use Factory to create Calculations Interface
                Calculations calcs = DocTypeCalcFactory.Create(docTypeCalcResolver, iusLoan);

                decimal total = calcs.AssetCalculation.LiquidAssetsTotal();
                return Ok(total);
            }
            catch (Exception ex)
            {
                this.Error("Calculating Liquid Assets total", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{fileid}/LiquidAssetsUsable/")]
        public async Task<ActionResult> LiquidAssetsUsable(string fileId)
        {
            try
            {
                this.Info("Calculating Liquid Assets Usable");
                // Validate custom header values
                CustomHeaderData customHeaderData = new CustomHeaderData(HttpContext.Request.Headers);
                customHeaderData.ValidateCustomHeaders();

                // Get the file from disk
                MismoContentModel mismoContentModel =
                    await _fileManagerApiClient.GetMismoContentAsync(fileId, $"{_configuration["FileManagerApi:ApiUrl"]}");

                // Use custom header information and mismo content to create IUSLoan & DocTypeCalcResolver
                _IUSLoanManager.SetUp(customHeaderData, mismoContentModel);

                DocTypeCalcResolver docTypeCalcResolver = _IUSLoanManager.GetDocTypeCalcResolver();
                IUSLoan iusLoan = _IUSLoanManager.GetIUSLoan();

                // Use Factory to create Calculations Interface
                Calculations calcs = DocTypeCalcFactory.Create(docTypeCalcResolver, iusLoan);

                decimal total = calcs.AssetCalculation.LiquidAssetsUsable();
                return Ok(total);
            }
            catch (Exception ex)
            {
                this.Error("Error in calculating Liquid Assets Usable", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}