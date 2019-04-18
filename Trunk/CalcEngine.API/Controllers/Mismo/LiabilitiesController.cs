using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.FileManager;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs.DocType;
using CalcEngine.Models;
using MismoCalcs.Interface;
using Microsoft.Extensions.Logging;
using CalcEngine.Calcs;
using CalcEngine.Logging;

namespace CalcEngine.API.Controllers.Mismo
{
    [Produces("application/json")]
    [Route("api/mismo/Liabilities")]
    public class LiabilitiesController : Controller
    {
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public LiabilitiesController(
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
        [Route("{fileid}/LiabilitiesData")]
        public async Task<ActionResult> LiabilitiesDataAsync(string fileId)
        {
            try
            {
                this.Info("Calculating Liabilities Data");
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

                if (iusLoan.MismoLoan.Liabilities != null)
                    return Ok(iusLoan.MismoLoan.Liabilities);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                this.Error("Error in calculating Liabilities Data", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
